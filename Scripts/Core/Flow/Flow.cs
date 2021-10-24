using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    public sealed class Flow : AbstractAnimation,
        IUpdateController,
        IFluentFlowOptionable<Flow>
    {
        private const string ErrorAnimationAlreadyStarted = "Cannot add animation that has already started.";
        private class UpdatableWrapper
        {
            public UpdatableWrapper(object updatableObject, int index, float? timeIndex = null)
            {
                this.updatableObject = updatableObject;
                this.index = index;
                this.timeIndex = timeIndex;
            }

            private readonly object updatableObject;
            public int index;
            public float? timeIndex;
            public UpdatableWrapper next;

            public AbstractUpdatable GetUpdatable()
            {
                switch (updatableObject)
                {
                    case AbstractUpdatable abstractUpdatable:
                        return abstractUpdatable;
                    case Func<AbstractUpdatable> getAbstractUpdatable:
                        return getAbstractUpdatable.Invoke();
                    default:
                        throw new ArgumentException("Unknown updatable type found.");
                }
            }
        }

        public Flow(FlowOptions options) : base(options.AutoStart)
        {
            CopyOptions(options);
        }

        public Flow(bool autoStart = false) : base(autoStart)
        {
        }

        #region Internal Members

        private readonly FastList<AbstractUpdatable, UpdatableAnchor> updatables = new FastList<AbstractUpdatable, UpdatableAnchor>();
        private readonly List<UpdatableWrapper> updatableWrappersQueue = new List<UpdatableWrapper>(2);
        private UpdatableWrapper lastQueuedUpdatableWrapper;
        private UpdatableWrapper[] updatableWrappersOrderedByTimeIndexed;
        private int nextTimeIndexedUpdatableWrapperIndex;
        private UpdatableWrapper nextTimeIndexedUpdatableWrapper;
        private readonly Dictionary<ulong, UpdatableWrapper> runningUpdatableWrappers = new Dictionary<ulong, UpdatableWrapper>(2);
        private int runningUpdatableWrappersCount;

        private float time;
        private int? remainingLoops;

        #endregion

        #region IUpdateController

        void IUpdateController.SubscribeToUpdate(AbstractUpdatable updatable)
        {
            updatables.Add(updatable);
        }

        void IUpdateController.UnsubscribeFromUpdate(AbstractUpdatable updatable)
        {
            updatables.Remove(updatable);
        }

        #endregion

        #region Lifecycle

        public Flow Start()
        {
            if (playState != PlayState.Building)
            {
                throw new FlowEntException("Flow already started.");
            }

            if (autoStartHelper != null)
            {
                CancelAutoStart();
            }
            StartInternal();
            return this;
        }

        public async Task<Flow> StartAsync()
        {
            if (playState != PlayState.Building)
            {
                throw new FlowEntException("Flow already started.");
            }

            if (autoStartHelper != null)
            {
                CancelAutoStart();
            }
            StartInternal();
            await new AwaitableAnimation(this);
            return this;
        }

        internal override void StartInternal(float deltaTime = 0)
        {
            playState = PlayState.Waiting;

            if (skipFrames > 0)
            {
                StartSkipFrames();
                return;
            }

            if (delay > 0f)
            {
                StartDelay();
                return;
            }

            remainingLoops = loopCount;

            Init();

            updateController.SubscribeToUpdate(this);
            playState = PlayState.Playing;
            onStarted?.Invoke();

            UpdateInternal(deltaTime);
        }

        private void Init()
        {
            time = 0;

            if (updatableWrappersOrderedByTimeIndexed == null)
            {
                updatableWrappersOrderedByTimeIndexed = updatableWrappersQueue.ToArray();
                //TODO do we really need to apply quick sort?
                QuickSortByTimeIndex(updatableWrappersOrderedByTimeIndexed, 0, updatableWrappersOrderedByTimeIndexed.Length - 1);
            }

            nextTimeIndexedUpdatableWrapperIndex = 0;
            nextTimeIndexedUpdatableWrapper = updatableWrappersOrderedByTimeIndexed[nextTimeIndexedUpdatableWrapperIndex++];
        }

        internal void CompleteUpdatable(AbstractUpdatable updatable)
        {
            float overdraft = 0;
            if (updatable is AbstractAnimation animation)
            {
                overdraft = animation.OverDraft.Value;
                animation.OverDraft = null;
            }

            UpdatableWrapper nextAnimationWrapper = runningUpdatableWrappers[updatable.Id].next;
            runningUpdatableWrappers.Remove(updatable.Id);
            if (nextAnimationWrapper == null)
            {
                --runningUpdatableWrappersCount;
                if (runningUpdatableWrappersCount == 0 && nextTimeIndexedUpdatableWrapper == null)
                {
                    this.overdraft = overdraft;
                    CompleteLoop();
                }
                return;
            }

            updatable = nextAnimationWrapper.GetUpdatable();
            runningUpdatableWrappers.Add(updatable.Id, nextAnimationWrapper);
            updatable.StartInternal(overdraft);
        }

        internal override void UpdateInternal(float deltaTime)
        {
            float scaledDeltaTime = deltaTime * timeScale;
            time += scaledDeltaTime;

            #region Updating animations

            AbstractUpdatable index = updatables.anchor.next;

            while (index != null)
            {
                index.UpdateInternal(scaledDeltaTime);
                index = index.next;
            }

            #endregion

            #region TimeBased start

            while (nextTimeIndexedUpdatableWrapper != null && time >= nextTimeIndexedUpdatableWrapper.timeIndex)
            {
                ++runningUpdatableWrappersCount;
                AbstractUpdatable updatable = nextTimeIndexedUpdatableWrapper.GetUpdatable();
                runningUpdatableWrappers.Add(updatable.Id, nextTimeIndexedUpdatableWrapper);
                updatable.StartInternal(time - nextTimeIndexedUpdatableWrapper.timeIndex.Value);

                if (nextTimeIndexedUpdatableWrapperIndex < updatableWrappersOrderedByTimeIndexed.Length)
                {
                    nextTimeIndexedUpdatableWrapper = updatableWrappersOrderedByTimeIndexed[nextTimeIndexedUpdatableWrapperIndex++];
                }
                else
                {
                    nextTimeIndexedUpdatableWrapper = null;
                }
            }

            #endregion

            onUpdated?.Invoke(scaledDeltaTime);
        }

        private void CompleteLoop()
        {
            remainingLoops--;

            if (!(remainingLoops <= 0))
            {
                onLoopCompleted?.Invoke(remainingLoops);
                Init();
                UpdateInternal(overdraft.Value);
                return;
            }

            if (remainingLoops == 0)
            {
                onLoopCompleted?.Invoke(remainingLoops);
            }

            updateController.UnsubscribeFromUpdate(this);

            playState = PlayState.Finished;

            onCompleted?.Invoke();

            if (updateController is Flow parentFlow)
            {
                parentFlow.CompleteUpdatable(this);
            }
        }

        #endregion

        #region Setters

        #region Threads

        #region Utils

        private void InitAnimation(AbstractAnimation animation)
        {
            if (animation.PlayState != PlayState.Building)
            {
                throw new FlowEntException(ErrorAnimationAlreadyStarted);
            }

            if (animation.HasAutoStart)
            {
                animation.CancelAutoStart();
            }
            animation.updateController = this;
        }

        private void AddOrQueue(object updatableObject)
        {
            if (lastQueuedUpdatableWrapper == null)
            {
                lastQueuedUpdatableWrapper = new UpdatableWrapper(updatableObject, updatableWrappersQueue.Count, 0);
                updatableWrappersQueue.Add(lastQueuedUpdatableWrapper);
            }
            else
            {
                UpdatableWrapper animationWrapper = new UpdatableWrapper(updatableObject, lastQueuedUpdatableWrapper.index);
                lastQueuedUpdatableWrapper.next = animationWrapper;
                lastQueuedUpdatableWrapper = animationWrapper;
            }
        }


        #endregion

        #region Queue

        public Flow Queue(AbstractAnimation animation)
        {
            InitAnimation(animation);

            AddOrQueue(animation);

            return this;
        }

        public Flow Queue(Func<Tween, Tween> tweenBuilder)
            => Queue(tweenBuilder(new Tween()));

        public Flow Queue(Func<Flow, Flow> flowBuilder)
            => Queue(flowBuilder(new Flow()));

        public Flow QueueDelay(float delay)
            => Queue(new Tween(delay));

        public Flow QueueAwaiter(AbstractFlowAwaiter flowAwaiter)
        {
            flowAwaiter.updateController = this;

            AddOrQueue(flowAwaiter);

            return this;
        }

        public Flow QueueAwaiter(Func<bool> waitCondition)
            => QueueAwaiter(new CallbackFlowAwaiter(waitCondition));

        public Flow QueueAwaiter(Task task)
            => QueueAwaiter(new TaskFlowAwaiter(task));

        #endregion

        #region QueueDeferred

        public Flow QueueDeferred(Func<AbstractAnimation> animationBuilder)
        {
            AbstractAnimation createAnimation()
            {
                AbstractAnimation animation = animationBuilder();

                InitAnimation(animation);

                return animation;
            }

            AddOrQueue((Func<AbstractAnimation>)createAnimation);

            return this;
        }

        public Flow QueueDeferred(Func<Tween, Tween> tweenBuilder)
            => QueueDeferred(() => tweenBuilder(new Tween()));

        public Flow QueueDeferred(Func<Flow, Flow> flowBuilder)
            => QueueDeferred(() => flowBuilder(new Flow()));

        #endregion

        #region At

        public Flow At(float timeIndex, AbstractAnimation animation)
        {
            if (timeIndex < 0)
            {
                throw new ArgumentException($"Time index cannot be negative. Value: {timeIndex}");
            }

            InitAnimation(animation);

            lastQueuedUpdatableWrapper = new UpdatableWrapper(animation, updatableWrappersQueue.Count, timeIndex);
            updatableWrappersQueue.Add(lastQueuedUpdatableWrapper);

            return this;
        }

        public Flow At(float timeIndex, Func<Tween, Tween> tweenBuilder)
            => At(timeIndex, tweenBuilder(new Tween(new TweenOptions())));

        public Flow At(float timeIndex, Func<Flow, Flow> flowBuilder)
            => At(timeIndex, flowBuilder(new Flow()));

        #endregion

        #region AtDeferred

        public Flow AtDeferred(float timeIndex, Func<AbstractAnimation> animationBuilder)
        {
            AbstractAnimation createAnimation()
            {
                AbstractAnimation animation = animationBuilder();

                if (timeIndex < 0)
                {
                    throw new ArgumentException($"Time index cannot be negative. Value: {timeIndex}");
                }

                InitAnimation(animation);

                return animation;
            }

            lastQueuedUpdatableWrapper = new UpdatableWrapper((Func<AbstractAnimation>)createAnimation, updatableWrappersQueue.Count, timeIndex);
            updatableWrappersQueue.Add(lastQueuedUpdatableWrapper);

            return this;
        }

        public Flow AtDeferred(float timeIndex, Func<Tween, Tween> tweenBuilder)
            => AtDeferred(timeIndex, () => tweenBuilder(new Tween()));

        public Flow AtDeferred(float timeIndex, Func<Flow, Flow> flowBuilder)
            => AtDeferred(timeIndex, () => flowBuilder(new Flow()));

        #endregion

        #endregion

        #region Events

        public Flow OnStarted(Action callback)
        {
            onStarted += callback;
            return this;
        }

        public Flow OnUpdated(Action<float> callback)
        {
            onUpdated += callback;
            return this;
        }

        public Flow OnCompleted(Action callback)
        {
            onCompleted += callback;
            return this;
        }

        public Flow OnLoopCompleted(Action<int?> callback)
        {
            onLoopCompleted += callback;
            return this;
        }

        #endregion

        #region Options

        public Flow SetOptions(FlowOptions options)
        {
            CopyOptions(options);
            return this;
        }

        public Flow SetOptions(Func<FlowOptions, FlowOptions> optionsBuilder)
        {
            CopyOptions(optionsBuilder(new FlowOptions()));
            return this;
        }

        public Flow SetSkipFrames(int frames)
        {
            this.skipFrames = frames;
            return this;
        }

        public Flow SetDelay(float time)
        {
            this.delay = time;
            return this;
        }

        public Flow SetLoopCount(int? loopCount)
        {
            if (loopCount <= 0)
            {
                throw new ArgumentException(AbstractAnimationOptions.ErrorLoopCountNegative);
            }
            this.loopCount = loopCount;
            return this;
        }

        public Flow SetTimeScale(float timeScale)
        {
            if (timeScale < 0)
            {
                throw new ArgumentException(AbstractAnimationOptions.ErrorTimeScaleNegative);
            }
            this.timeScale = timeScale;
            return this;
        }

        private void CopyOptions(FlowOptions options)
        {
            skipFrames = options.SkipFrames;
            delay = options.Delay;
            loopCount = options.LoopCount;
            timeScale = options.TimeScale;
        }

        #endregion

        #endregion

        #region Private

        #region QuickSort TimeIndex

        private void QuickSortByTimeIndex(UpdatableWrapper[] arr, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            int i = Partition(arr, start, end);
            QuickSortByTimeIndex(arr, start, i - 1);
            QuickSortByTimeIndex(arr, i + 1, end);
        }

        private int Partition(UpdatableWrapper[] arr, int start, int end)
        {
            UpdatableWrapper temp;
            float p = arr[end].timeIndex.Value;
            int i = start - 1;

            for (int j = start; j <= end - 1; j++)
            {
                if (arr[j].timeIndex < p)
                {
                    i++;
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            temp = arr[i + 1];
            arr[i + 1] = arr[end];
            arr[end] = temp;
            return i + 1;
        }

        #endregion

        #endregion

    }
}