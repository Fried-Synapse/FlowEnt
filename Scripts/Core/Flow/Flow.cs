using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowEnt
{
    public sealed class Flow : AbstractAnimation, IFluentFlowOptionable<Flow>
    {
        private class AnimationWrapper : AbstractFastListItem
        {
            public AnimationWrapper(AbstractAnimation animation, int index, float? startingTime = null)
            {
                Animation = animation;
                Index = index;
                TimeIndex = startingTime;
            }

            public AbstractAnimation Animation { get; }
            public float? TimeIndex { get; }
            public AnimationWrapper Next { get; set; }
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

        private List<AnimationWrapper> timeIndexedAnimationWrappers = new List<AnimationWrapper>();
        private AnimationWrapper lastQueuedAnimationWrapper;
        private int animationsCount;

        private AnimationWrapper[] orderedTimeIndexedAnimationWrappers;
        private FastList<AnimationWrapper> runningOrderedTimeIndexedAnimationWrappers;
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

            if (orderedTimeIndexedAnimationWrappers == null)
            {
                orderedTimeIndexedAnimationWrappers = timeIndexedAnimationWrappers.ToArray();
                QuickSortByTimeIndex(orderedTimeIndexedAnimationWrappers, 0, orderedTimeIndexedAnimationWrappers.Length - 1);
            }
            runningOrderedTimeIndexedAnimationWrappers = new FastList<AnimationWrapper>(orderedTimeIndexedAnimationWrappers);

            nextTimeIndexedAnimationWrapper = runningOrderedTimeIndexedAnimationWrappers.Last();
            runningOrderedTimeIndexedAnimationWrappers.RemoveLast();
            runningAnimationWrappers = new AnimationWrapper[animationsCount];
        }

        internal override float? UpdateInternal(float deltaTime)
        {
            float scaledDeltaTime = deltaTime * timeScale;
            time += scaledDeltaTime;

            #region TimeBased start

            while (nextTimeIndexedAnimationWrapper != null && time > nextTimeIndexedAnimationWrapper.TimeIndex)
            {
                nextTimeIndexedAnimationWrapper.Animation.StartInternal(false);
                runningAnimationWrappers[nextTimeIndexedAnimationWrapper.Index] = nextTimeIndexedAnimationWrapper;
                ++runningAnimationWrappersCount;
                if (runningOrderedTimeIndexedAnimationWrappers.Count > 0)
                {
                    nextTimeIndexedAnimationWrapper = runningOrderedTimeIndexedAnimationWrappers.Last();
                    runningOrderedTimeIndexedAnimationWrappers.RemoveLast();
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
                    float? overdraft = animationWrapper.Animation.UpdateInternal(runningDeltaTime);
                    if (overdraft != null)
                    {
                        animationWrapper = runningAnimationWrappers[i].Next;
                        if (animationWrapper != null)
                        {
                            runningAnimationWrappers[i] = animationWrapper;
                            animationWrapper.Animation.StartInternal(false);
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
                lastQueuedAnimationWrapper = new AnimationWrapper(animation, animationsCount, 0);
                timeIndexedAnimationWrappers.Add(lastQueuedAnimationWrapper);
            }
            else
            {
                AnimationWrapper animationWrapper = new AnimationWrapper(animation, lastQueuedAnimationWrapper.Index);
                lastQueuedAnimationWrapper.Next = animationWrapper;
                lastQueuedAnimationWrapper = animationWrapper;
            }
            animationsCount++;

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

            lastQueuedAnimationWrapper = new AnimationWrapper(animation, animationsCount, timeIndex);
            timeIndexedAnimationWrappers.Add(lastQueuedAnimationWrapper);
            animationsCount++;

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
            float p = arr[end].TimeIndex.Value;
            int i = start - 1;

            for (int j = start; j <= end - 1; j++)
            {
                if (arr[j].TimeIndex >= p)
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
