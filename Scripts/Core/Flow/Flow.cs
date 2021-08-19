using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    public sealed class Flow : AbstractAnimation,
        IUpdateController,
        IFluentFlowOptionable<Flow>
    {
        private class AnimationWrapper
        {
            public AnimationWrapper(AbstractAnimation animation, int index, float? timeIndex = null)
            {
                this.animation = animation;
                this.index = index;
                this.timeIndex = timeIndex;
            }

            public AnimationWrapper(Func<AbstractAnimation> animationBuilder, int index, float? timeIndex = null)
            {
                this.animationBuilder = animationBuilder;
                this.index = index;
                this.timeIndex = timeIndex;
            }

            public int index;
            public AbstractAnimation animation;
            public Func<AbstractAnimation> animationBuilder;
            public float? timeIndex;
            public AnimationWrapper next;
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
        private readonly List<AnimationWrapper> animationWrappersQueue = new List<AnimationWrapper>(2);
        private AnimationWrapper lastQueuedAnimationWrapper;
        private AnimationWrapper[] animationWrappersOrderedByTimeIndexed;
        private int nextTimeIndexedAnimationWrapperIndex;
        private AnimationWrapper nextTimeIndexedAnimationWrapper;
        private readonly Dictionary<ulong, AnimationWrapper> runningAnimationWrappers = new Dictionary<ulong, AnimationWrapper>(2);
        private int runningAnimationWrappersCount;

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

            onStarted?.Invoke();

            playState = PlayState.Playing;

            UpdateInternal(deltaTime);
        }

        private void Init()
        {
            time = 0;

            if (animationWrappersOrderedByTimeIndexed == null)
            {
                animationWrappersOrderedByTimeIndexed = animationWrappersQueue.ToArray();
                //TODO do we really need to apply quick sort?
                QuickSortByTimeIndex(animationWrappersOrderedByTimeIndexed, 0, animationWrappersOrderedByTimeIndexed.Length - 1);
            }

            nextTimeIndexedAnimationWrapperIndex = 0;
            nextTimeIndexedAnimationWrapper = animationWrappersOrderedByTimeIndexed[nextTimeIndexedAnimationWrapperIndex++];
        }

        internal void CompleteAnimation(AbstractAnimation animation)
        {
            float overdraft = animation.OverDraft.Value;
            animation.OverDraft = null;
            AnimationWrapper nextAnimationWrapper = runningAnimationWrappers[animation.Id].next;
            runningAnimationWrappers.Remove(animation.Id);
            if (nextAnimationWrapper == null)
            {
                --runningAnimationWrappersCount;
                if (runningAnimationWrappersCount == 0 && nextTimeIndexedAnimationWrapper == null)
                {
                    this.overdraft = overdraft;
                    CompleteLoop();
                }
                return;
            }

            animation = nextAnimationWrapper.animation ?? nextAnimationWrapper.animationBuilder();
            runningAnimationWrappers.Add(animation.Id, nextAnimationWrapper);
            animation.StartInternal(overdraft);
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

            while (nextTimeIndexedAnimationWrapper != null && time >= nextTimeIndexedAnimationWrapper.timeIndex)
            {
                ++runningAnimationWrappersCount;
                AbstractAnimation animation = nextTimeIndexedAnimationWrapper.animation ?? nextTimeIndexedAnimationWrapper.animationBuilder();
                runningAnimationWrappers.Add(animation.Id, nextTimeIndexedAnimationWrapper);
                animation.StartInternal(time - nextTimeIndexedAnimationWrapper.timeIndex.Value);

                if (nextTimeIndexedAnimationWrapperIndex < animationWrappersOrderedByTimeIndexed.Length)
                {
                    nextTimeIndexedAnimationWrapper = animationWrappersOrderedByTimeIndexed[nextTimeIndexedAnimationWrapperIndex++];
                }
                else
                {
                    nextTimeIndexedAnimationWrapper = null;
                }
            }

            #endregion
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

            onCompleted?.Invoke();

            if (updateController is Flow parentFlow)
            {
                parentFlow.CompleteAnimation(this);
            }

            playState = PlayState.Finished;
        }

        #endregion

        #region Setters

        #region Threads

        public Flow Queue(AbstractAnimation animation)
        {
            if (animation.PlayState != PlayState.Building)
            {
                throw new FlowEntException("Cannot add animation that has already started.");
            }

            if (animation.HasAutoStart)
            {
                animation.CancelAutoStart();
            }
            animation.updateController = this;

            if (lastQueuedAnimationWrapper == null)
            {
                lastQueuedAnimationWrapper = new AnimationWrapper(animation, animationWrappersQueue.Count, 0);
                animationWrappersQueue.Add(lastQueuedAnimationWrapper);
            }
            else
            {
                AnimationWrapper animationWrapper = new AnimationWrapper(animation, lastQueuedAnimationWrapper.index);
                lastQueuedAnimationWrapper.next = animationWrapper;
                lastQueuedAnimationWrapper = animationWrapper;
            }

            return this;
        }

        public Flow Queue(Func<Tween, Tween> tweenBuilder)
            => Queue(tweenBuilder(new Tween()));

        public Flow Queue(Func<Flow, Flow> flowBuilder)
            => Queue(flowBuilder(new Flow()));

        public Flow QueueDeferred(Func<AbstractAnimation> animationBuilder)
        {
            AbstractAnimation createAnimation()
            {
                AbstractAnimation animation = animationBuilder();

                if (animation.PlayState != PlayState.Building)
                {
                    throw new FlowEntException("Cannot add animation that has already started.");
                }

                if (animation.HasAutoStart)
                {
                    animation.CancelAutoStart();
                }
                animation.updateController = this;

                return animation;
            }

            if (lastQueuedAnimationWrapper == null)
            {
                lastQueuedAnimationWrapper = new AnimationWrapper(createAnimation, animationWrappersQueue.Count, 0);
                animationWrappersQueue.Add(lastQueuedAnimationWrapper);
            }
            else
            {
                AnimationWrapper animationWrapper = new AnimationWrapper(createAnimation, lastQueuedAnimationWrapper.index);
                lastQueuedAnimationWrapper.next = animationWrapper;
                lastQueuedAnimationWrapper = animationWrapper;
            }

            return this;
        }

        public Flow QueueDeferred(Func<Tween, Tween> tweenBuilder)
            => QueueDeferred(() => tweenBuilder(new Tween()));

        public Flow QueueDeferred(Func<Flow, Flow> flowBuilder)
            => QueueDeferred(() => flowBuilder(new Flow()));

        public Flow At(float timeIndex, AbstractAnimation animation)
        {
            if (timeIndex < 0)
            {
                throw new ArgumentException($"Time index cannot be negative. Value: {timeIndex}");
            }

            if (animation.PlayState != PlayState.Building)
            {
                throw new FlowEntException("Cannot add animation that has already started.");
            }

            if (animation.HasAutoStart)
            {
                animation.CancelAutoStart();
            }
            animation.updateController = this;

            lastQueuedAnimationWrapper = new AnimationWrapper(animation, animationWrappersQueue.Count, timeIndex);
            animationWrappersQueue.Add(lastQueuedAnimationWrapper);

            return this;
        }

        public Flow At(float timeIndex, Func<Tween, Tween> tweenBuilder)
            => At(timeIndex, tweenBuilder(new Tween(new TweenOptions())));

        public Flow At(float timeIndex, Func<Flow, Flow> flowBuilder)
            => At(timeIndex, flowBuilder(new Flow()));

        public Flow AtDeferred(float timeIndex, Func<AbstractAnimation> animationBuilder)
        {
            AbstractAnimation createAnimation()
            {
                AbstractAnimation animation = animationBuilder();

                if (timeIndex < 0)
                {
                    throw new ArgumentException($"Time index cannot be negative. Value: {timeIndex}");
                }

                if (animation.PlayState != PlayState.Building)
                {
                    throw new FlowEntException("Cannot add animation that has already started.");
                }

                if (animation.HasAutoStart)
                {
                    animation.CancelAutoStart();
                }
                animation.updateController = this;

                return animation;
            }

            lastQueuedAnimationWrapper = new AnimationWrapper(createAnimation, animationWrappersQueue.Count, timeIndex);
            animationWrappersQueue.Add(lastQueuedAnimationWrapper);

            return this;
        }

        public Flow AtDeferred(float timeIndex, Func<Tween, Tween> tweenBuilder)
            => AtDeferred(timeIndex, () => tweenBuilder(new Tween()));

        public Flow AtDeferred(float timeIndex, Func<Flow, Flow> flowBuilder)
            => AtDeferred(timeIndex, () => flowBuilder(new Flow()));

        #endregion

        #region Events

        public Flow OnStarted(Action callback)
        {
            onStarted += callback;
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

        private void QuickSortByTimeIndex(AnimationWrapper[] arr, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            int i = Partition(arr, start, end);
            QuickSortByTimeIndex(arr, start, i - 1);
            QuickSortByTimeIndex(arr, i + 1, end);
        }

        private int Partition(AnimationWrapper[] arr, int start, int end)
        {
            AnimationWrapper temp;
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