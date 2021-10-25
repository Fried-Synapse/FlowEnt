using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides functionality to create a sequence or multiple sequences of animations.
    /// For more information please go to https://flowent.friedsynapse.com/flow
    /// </summary>
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

        /// <summary>
        /// Creates a new flow using the options provided.
        /// </summary>
        /// <param name="options"></param>
        public Flow(FlowOptions options)
        {
            CopyOptions(options);
        }

        /// <summary>
        /// Creates a new flow.
        /// </summary>
        /// <param name="autoStart">Weather the flow should start automatically or not.</param>
        public Flow(bool autoStart = false)
        {
            SetAutoStart(autoStart);
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

        /// <summary>
        /// Starts the flow.
        /// </summary>
        /// <exception cref="FlowEntException">If the flow has already started.</exception>
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

        /// <summary>
        /// Starts the flow async(you can await this till the flow finishes).
        /// </summary>
        /// <exception cref="FlowEntException">If the flow has already started.</exception>
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

        /// <summary>
        /// Queues an animation in the current sequence.
        /// </summary>
        /// <param name="animation"></param>
        public Flow Queue(AbstractAnimation animation)
        {
            InitAnimation(animation);

            AddOrQueue(animation);

            return this;
        }

        /// <summary>
        /// Creates a tween and provides a context to build it and then queues the built animation in the current sequence.
        /// </summary>
        /// <param name="tweenBuilder"></param>
        public Flow Queue(Func<Tween, Tween> tweenBuilder)
            => Queue(tweenBuilder(new Tween()));

        /// <summary>
        /// Creates a flow and provides a context to build it and then queues the built animation in the current sequence.
        /// </summary>
        /// <param name="flowBuilder"></param>
        public Flow Queue(Func<Flow, Flow> flowBuilder)
            => Queue(flowBuilder(new Flow()));

        /// <summary>
        /// Queues a delay in the current sequence.
        /// </summary>
        /// <param name="delay"></param>
        public Flow QueueDelay(float delay)
            => Queue(new Tween(delay));

        /// <summary>
        /// Queues an awaiter in the current sequence.
        /// </summary>
        /// <param name="flowAwaiter"></param>
        public Flow QueueAwaiter(AbstractFlowAwaiter flowAwaiter)
        {
            flowAwaiter.updateController = this;

            AddOrQueue(flowAwaiter);

            return this;
        }

        /// <summary>
        /// Queues a callback as an awaiter in the current sequence.
        /// </summary>
        /// <param name="waitCondition"></param>
        public Flow QueueAwaiter(Func<bool> waitCondition)
            => QueueAwaiter(new CallbackFlowAwaiter(waitCondition));

        /// <summary>
        /// Queues a task as an awaiter in the current sequence.
        /// </summary>
        /// <param name="task"></param>
        public Flow QueueAwaiter(Task task)
            => QueueAwaiter(new TaskFlowAwaiter(task));

        #endregion

        #region QueueDeferred

        /// <summary>
        /// Queues a callback for the animation builder in the current sequence.
        /// This is useful when you need to create an animation after the current flow has started.
        /// </summary>
        /// <param name="animationBuilder"></param>
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

        /// <summary>
        /// Queues a callback, that creates a tween and provides a context to build it, in the current sequence.
        /// This is useful when you need to create an animation after the current flow has started.
        /// </summary>
        /// <param name="tweenBuilder"></param>
        public Flow QueueDeferred(Func<Tween, Tween> tweenBuilder)
            => QueueDeferred(() => tweenBuilder(new Tween()));

        /// <summary>
        /// Queues a callback, that creates a flow and provides a context to build it, in the current sequence.
        /// This is useful when you need to create an animation after the current flow has started.
        /// </summary>
        /// <param name="flowBuilder"></param>
        public Flow QueueDeferred(Func<Flow, Flow> flowBuilder)
            => QueueDeferred(() => flowBuilder(new Flow()));

        #endregion

        #region At

        /// <summary>
        /// Starts a new sequence at the <paramref name="timeIndex"/> provided.
        /// </summary>
        /// <param name="timeIndex">Time index for the sequence to start.</param>
        /// <param name="animation"></param>
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

        /// <summary>
        /// Creates a tween and provides a context to build it and then starts a new sequence at the <paramref name="timeIndex"/> provided.
        /// </summary>
        /// <param name="timeIndex">Time index for the sequence to start.</param>
        /// <param name="tweenBuilder"></param>
        public Flow At(float timeIndex, Func<Tween, Tween> tweenBuilder)
            => At(timeIndex, tweenBuilder(new Tween(new TweenOptions())));

        /// <summary>
        /// Creates a flow and provides a context to build it and then starts a new sequence at the <paramref name="timeIndex"/> provided.
        /// </summary>
        /// <param name="timeIndex">Time index for the sequence to start.</param>
        /// <param name="flowBuilder"></param>
        public Flow At(float timeIndex, Func<Flow, Flow> flowBuilder)
            => At(timeIndex, flowBuilder(new Flow()));

        #endregion

        #region AtDeferred

        /// <summary>
        /// Starts a new sequence at the <paramref name="timeIndex"/> provided and uses the callback for the animation builder to add an animation to the sequence.
        /// This is useful when you need to create an animation after the current flow has started.
        /// </summary>
        /// <param name="timeIndex">Time index for the sequence to start.</param>
        /// <param name="animationBuilder"></param>
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

        /// <summary>
        /// Starts a new sequence at the <paramref name="timeIndex"/> provided and uses the callback for the animation builder to add an animation to the sequence.
        /// This is useful when you need to create an animation after the current flow has started.
        /// </summary>
        /// <param name="timeIndex">Time index for the sequence to start.</param>
        /// <param name="tweenBuilder"></param>
        public Flow AtDeferred(float timeIndex, Func<Tween, Tween> tweenBuilder)
            => AtDeferred(timeIndex, () => tweenBuilder(new Tween()));

        /// <summary>
        /// Starts a new sequence at the <paramref name="timeIndex"/> provided and uses the callback for the animation builder to add an animation to the sequence.
        /// This is useful when you need to create an animation after the current flow has started.
        /// </summary>
        /// <param name="timeIndex">Time index for the sequence to start.</param>
        /// <param name="flowBuilder"></param>
        public Flow AtDeferred(float timeIndex, Func<Flow, Flow> flowBuilder)
            => AtDeferred(timeIndex, () => flowBuilder(new Flow()));

        #endregion

        #endregion

        #region Events

        /// <summary>
        /// Adds an event called when the flow started.
        /// </summary>
        /// <param name="callback">The event.</param>
        public Flow OnStarted(Action callback)
        {
            onStarted += callback;
            return this;
        }

        /// <summary>
        /// Adds an event called when the flow updated.
        /// </summary>
        /// <param name="callback">The event.</param>
        public Flow OnUpdated(Action<float> callback)
        {
            onUpdated += callback;
            return this;
        }

        /// <summary>
        /// Adds an event called when the flow completed.
        /// </summary>
        /// <param name="callback">The event.</param>
        public Flow OnCompleted(Action callback)
        {
            onCompleted += callback;
            return this;
        }

        /// <summary>
        /// Adds an event called when a loop completed.
        /// </summary>
        /// <param name="callback">The event. The parameter represents the number of loops left. If there are infinite loops it'll send a null param.</param>
        public Flow OnLoopCompleted(Action<int?> callback)
        {
            onLoopCompleted += callback;
            return this;
        }

        #endregion

        #region Options

        /// <summary>
        /// Sets all the options for this flow.
        /// </summary>
        /// <param name="options"></param>
        public Flow SetOptions(FlowOptions options)
        {
            CopyOptions(options);
            return this;
        }

        /// <summary>
        /// Creates a builder for options and then sets all the options for this flow.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public Flow SetOptions(Func<FlowOptions, FlowOptions> optionsBuilder)
        {
            CopyOptions(optionsBuilder(new FlowOptions()));
            return this;
        }

        /// <summary>
        /// Sets the amount of frames you want to skip at when this flow is started.
        /// </summary>
        /// <param name="frames"></param>
        public Flow SetSkipFrames(int frames)
        {
            this.skipFrames = frames;
            return this;
        }

        /// <summary>
        /// Sets the amount of time(s) that you want to delay when this flow is started.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public Flow SetDelay(float time)
        {
            this.delay = time;
            return this;
        }

        /// <summary>
        /// Sets the amount of loops you want this flow to have. If you want infinite loops pass a null value.
        /// Note: all loops are reset. No ping-pong option for flows.
        /// </summary>
        /// <param name="loopCount"></param>
        public Flow SetLoopCount(int? loopCount)
        {
            if (loopCount <= 0)
            {
                throw new ArgumentException(AbstractAnimationOptions.ErrorLoopCountNegative);
            }
            this.loopCount = loopCount;
            return this;
        }

        /// <summary>
        /// Sets the time scale for the current flow(and all it's animations).
        /// </summary>
        /// <param name="timeScale"></param>
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
            SetAutoStart(options.AutoStart);
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