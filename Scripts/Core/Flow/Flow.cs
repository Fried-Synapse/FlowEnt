using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowEnt
{
    public sealed class Flow : AbstractAnimation, IFluentFlowOptionable<Flow>
    {
        private class AnimationWrapper
        {
            public AnimationWrapper(AbstractAnimation animation, int index, float? timeIndex = null)
            {
                this.animation = animation;
                this.index = index;
                this.timeIndex = timeIndex;
            }

            public int index;
            public AbstractAnimation animation;
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

        private Action onStartCallback;
        private Action onCompleteCallback;

        #region Options

        private int? loopCount = 1;
        private float timeScale = 1;

        #endregion

        #region Internal Members

        private List<AnimationWrapper> animationWrappersQueue = new List<AnimationWrapper>();
        private AnimationWrapper lastQueuedAnimationWrapper;

        private AnimationWrapper[] animationWrappersOrderedByTimeIndexed;
        private int nextTimeIndexedAnimationWrapperIndex;
        private AnimationWrapper nextTimeIndexedAnimationWrapper;
        private AnimationWrapper[] runningAnimationWrappers;
        private int runningAnimationWrappersCount;

        private float time;
        private int? remainingLoops;

        #endregion

        #region Lifecycle

        protected override void OnAutoStart(float deltaTime)
        {
            if (PlayState != PlayState.Building)
            {
                return;
            }

            StartInternal();
            UpdateInternal(deltaTime);
        }

        public Flow Start()
        {
            if (PlayState != PlayState.Building)
            {
                throw new FlowEntException("Flow already started.");
            }

            StartInternal();
            return this;
        }

        public async Task<Flow> StartAsync()
        {
            if (PlayState != PlayState.Building)
            {
                throw new FlowEntException("Flow already started.");
            }

            StartInternal();
            await new AwaitableAnimation(this);
            return this;
        }

        internal override void StartInternal(bool subscribeToUpdate = true)
        {
            remainingLoops = loopCount;

            Init();

            IsSubscribedToUpdate = subscribeToUpdate;
            if (IsSubscribedToUpdate)
            {
                FlowEntController.Instance.SubscribeToUpdate(this);
            }

            onStartCallback?.Invoke();

            PlayState = PlayState.Playing;
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

            runningAnimationWrappers = new AnimationWrapper[animationWrappersQueue.Count];
        }

        //TODO this can be done faster for sure...
        internal override float? UpdateInternal(float deltaTime)
        {
            float scaledDeltaTime = deltaTime * timeScale;
            time += scaledDeltaTime;

            #region TimeBased start

            while (nextTimeIndexedAnimationWrapper != null && time > nextTimeIndexedAnimationWrapper.timeIndex)
            {
                nextTimeIndexedAnimationWrapper.animation.StartInternal(false);
                runningAnimationWrappers[nextTimeIndexedAnimationWrapper.index] = nextTimeIndexedAnimationWrapper;
                ++runningAnimationWrappersCount;
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

            #region Updating animations

            for (int i = 0; i < runningAnimationWrappers.Length; i++)
            {
                if (runningAnimationWrappers[i] == null)
                {
                    continue;
                }

                bool isUpdated = false;
                float runningDeltaTime = scaledDeltaTime;
                AnimationWrapper animationWrapper = runningAnimationWrappers[i];
                do
                {
                    float? overdraft = animationWrapper.animation.UpdateInternal(runningDeltaTime);
                    if (overdraft != null)
                    {
                        animationWrapper = runningAnimationWrappers[i].next;
                        if (animationWrapper != null)
                        {
                            runningAnimationWrappers[i] = animationWrapper;
                            animationWrapper.animation.StartInternal(false);
                            runningDeltaTime = overdraft.Value;
                        }
                        else
                        {
                            runningAnimationWrappers[i] = null;
                            --runningAnimationWrappersCount;
                            if (runningAnimationWrappersCount == 0 && nextTimeIndexedAnimationWrapper == null)
                            {
                                return CompleteLoop(overdraft.Value);
                            }
                            i--;
                            break;
                        }
                    }
                    else
                    {
                        isUpdated = true;
                    }
                }
                while (!isUpdated);
            }

            #endregion

            return null;
        }

        private float? CompleteLoop(float overdraft)
        {
            remainingLoops--;
            if (remainingLoops > 0)
            {
                Init();
                UpdateInternal(overdraft);
                return null;
            }

            if (IsSubscribedToUpdate)
            {
                FlowEntController.Instance.UnsubscribeFromUpdate(this);
            }

            onCompleteCallback?.Invoke();

            PlayState = PlayState.Finished;
            return overdraft;
        }

        #endregion

        #region Setters

        #region Events

        public Flow OnStart(Action callback)
        {
            onStartCallback += callback;
            return this;
        }

        public Flow OnComplete(Action callback)
        {
            onCompleteCallback += callback;
            return this;
        }

        protected override void OnCompleteInternal(Action callback)
        {
            onCompleteCallback += callback;
        }

        #endregion

        #region Threads

        public Flow Queue(AbstractAnimation animation)
        {
            if (animation.PlayState != PlayState.Building)
            {
                throw new FlowEntException("Cannot add animation that has already started.");
            }

            animation.CancelAutoStart();

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

            animation.CancelAutoStart();

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
            int i;
            if (start < end)
            {
                i = Partition(arr, start, end);

                QuickSortByTimeIndex(arr, start, i - 1);
                QuickSortByTimeIndex(arr, i + 1, end);
            }
        }

        private int Partition(AnimationWrapper[] arr, int start, int end)
        {
            AnimationWrapper temp;
            float p = arr[end].timeIndex.Value;
            int i = start - 1;

            for (int j = start; j <= end - 1; j++)
            {
                if (arr[j].timeIndex >= p)
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