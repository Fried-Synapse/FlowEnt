using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowEnt
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

            public AnimationWrapper(Func<AbstractAnimation> animationCreation, int index, float? timeIndex = null)
            {
                this.animationCreation = animationCreation;
                this.index = index;
                this.timeIndex = timeIndex;
            }

            public int index;
            public AbstractAnimation animation;
            public Func<AbstractAnimation> animationCreation;
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
        private readonly List<AnimationWrapper> animationWrappersQueue = new List<AnimationWrapper>(1);
        private AnimationWrapper lastQueuedAnimationWrapper;

        private AnimationWrapper[] animationWrappersOrderedByTimeIndexed;
        private int nextTimeIndexedAnimationWrapperIndex;
        private AnimationWrapper nextTimeIndexedAnimationWrapper;
        private readonly Dictionary<ulong, AnimationWrapper> runningAnimationWrappers = new Dictionary<ulong, AnimationWrapper>();
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
                QuickSortByTimeIndex(animationWrappersOrderedByTimeIndexed, 0, animationWrappersOrderedByTimeIndexed.Length - 1);
            }

            nextTimeIndexedAnimationWrapperIndex = 0;
            nextTimeIndexedAnimationWrapper = animationWrappersOrderedByTimeIndexed[nextTimeIndexedAnimationWrapperIndex++];
        }

        internal void CompleteAnimation(AbstractAnimation animation)
        {
            AnimationWrapper animationWrapper = runningAnimationWrappers[animation.Id];
            runningAnimationWrappers.Remove(animation.Id);
            AnimationWrapper nextAnimationWrapper = animationWrapper.next;
            if (nextAnimationWrapper == null)
            {
                --runningAnimationWrappersCount;
                if (runningAnimationWrappersCount == 0 && nextTimeIndexedAnimationWrapper == null)
                {
                    overdraft = animationWrapper.animation.OverDraft;
                    CompleteLoop();
                }
                return;
            }

            runningAnimationWrappers.Add(nextAnimationWrapper.animation.Id, nextAnimationWrapper);
            animation = nextAnimationWrapper.animation ?? nextAnimationWrapper.animationCreation();
            animation.StartInternal(animationWrapper.animation.OverDraft.Value);
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
                runningAnimationWrappers.Add(nextTimeIndexedAnimationWrapper.animation.Id, nextTimeIndexedAnimationWrapper);
                AbstractAnimation animation = nextTimeIndexedAnimationWrapper.animation ?? nextTimeIndexedAnimationWrapper.animationCreation();
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
            if (remainingLoops > 0)
            {
                Init();
                UpdateInternal(overdraft.Value);
                return;
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

        internal override void OnCompletedInternal(Action callback)
        {
            onCompleted += callback;
        }

        #endregion

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
            => Queue(tweenBuilder(new Tween(new TweenOptions())));

        public Flow Queue(Func<Flow, Flow> flowBuilder)
            => Queue(flowBuilder(new Flow()));

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

        #endregion

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
            this.loopCount = loopCount;
            return this;
        }

        public Flow SetTimeScale(float timeScale)
        {
            if (timeScale < 0)
            {
                throw new ArgumentException("Value cannot be less than 0");
            }
            this.timeScale = timeScale;
            return this;
        }

        private void CopyOptions(FlowOptions options)
        {
            loopCount = options.LoopCount;
            timeScale = options.TimeScale;
        }

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