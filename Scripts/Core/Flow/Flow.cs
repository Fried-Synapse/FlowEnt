using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowEnt
{
    public sealed class Flow : AbstractAnimation, IFluentFlowOptionable<Flow>
    {
        private class AnimationWrapper
        {
            public AnimationWrapper(AbstractAnimation animation, float? startingTime = null)
            {
                this.animation = animation;
                this.timeIndex = startingTime;
            }

            public AbstractAnimation animation;
            public float? timeIndex;
            public AnimationWrapper next;
        }

        private class AnimationThread : FastListItem<AnimationThread>
        {
            public AnimationWrapper active;
        }

        private class AnimationThreadAnchor : AnimationThread
        {
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

        private List<AnimationWrapper> animationThreads = new List<AnimationWrapper>();
        private AnimationWrapper queueingAnimationThread;

        private AnimationWrapper[] animationThreadsOrderedByTimeIndex;
        private AnimationWrapper nextAnimationThreadsOrderedByTimeIndex;
        private int fuckingIndex;
        private FastList<AnimationThread, AnimationThreadAnchor> runningAnimationThreads;

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

            if (animationThreadsOrderedByTimeIndex == null)
            {
                animationThreadsOrderedByTimeIndex = animationThreads.ToArray();
                QuickSortByTimeIndex(animationThreadsOrderedByTimeIndex, 0, animationThreadsOrderedByTimeIndex.Length - 1);
            }

            fuckingIndex = 0;
            nextAnimationThreadsOrderedByTimeIndex = animationThreadsOrderedByTimeIndex[fuckingIndex++];
            runningAnimationThreads = new FastList<AnimationThread, AnimationThreadAnchor>();
        }

        internal override float? UpdateInternal(float deltaTime)
        {
            float scaledDeltaTime = deltaTime * timeScale;
            time += scaledDeltaTime;

            #region TimeBased start

            while (nextAnimationThreadsOrderedByTimeIndex != null && time > nextAnimationThreadsOrderedByTimeIndex.timeIndex)
            {
                nextAnimationThreadsOrderedByTimeIndex.animation.StartInternal(false);
                runningAnimationThreads.Add(nextAnimationThreadsOrderedByTimeIndex);
                if (fuckingIndex < animationThreadsOrderedByTimeIndex.Length)
                {
                    nextAnimationThreadsOrderedByTimeIndex = animationThreadsOrderedByTimeIndex[fuckingIndex++];
                }
                else
                {
                    nextAnimationThreadsOrderedByTimeIndex = null;
                }
            }

            #endregion

            #region Updating animations

            AnimationWrapper index = runningAnimationThreads.Anchor.next;
            int x = 0;
            while (index != null && x < 10)
            {
                x++;
                bool isUpdated = false;
                float runningDeltaTime = scaledDeltaTime;
                AnimationWrapper animationWrapper = index;
                do
                {
                    float? overdraft = animationWrapper.animation.UpdateInternal(runningDeltaTime);
                    if (overdraft != null)
                    {
                        AnimationWrapper nextAnimationWrapper = animationWrapper.next;
                        if (nextAnimationWrapper != null)
                        {
                            runningAnimationThreads.Replace(animationWrapper, nextAnimationWrapper);
                            animationWrapper = nextAnimationWrapper;
                            animationWrapper.animation.StartInternal(false);
                            runningDeltaTime = overdraft.Value;
                        }
                        else
                        {
                            runningAnimationThreads.Remove(animationWrapper);
                            if (runningAnimationThreads.Anchor.next == null && nextAnimationThreadsOrderedByTimeIndex == null)
                            {
                                return CompleteLoop(overdraft.Value);
                            }
                            break;
                        }
                    }
                    else
                    {
                        isUpdated = true;
                    }
                }
                while (!isUpdated);
                index = index.next;
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

            if (queueingAnimationThread == null)
            {
                queueingAnimationThread = new AnimationWrapper(animation, 0);
                animationThreads.Add(queueingAnimationThread);
            }
            else
            {
                AnimationWrapper animationWrapper = new AnimationWrapper(animation);
                queueingAnimationThread.next = animationWrapper;
                queueingAnimationThread = animationWrapper;
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

            queueingAnimationThread = new AnimationWrapper(animation, timeIndex);
            animationThreads.Add(queueingAnimationThread);

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
                if (arr[j].timeIndex <= p)
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
