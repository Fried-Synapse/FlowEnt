using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowEnt
{
    public class FlowOptions : AbstractAnimationOptions
    {
        public int? LoopCount { get; set; } = 1;
    }

    public sealed class Flow : AbstractAnimation
    {
        private class AnimationWrapper : AbstractFastListItem
        {
            public AnimationWrapper(Tween animation, float? startingTime = null)
            {
                Animation = animation;
                TimeIndex = startingTime;
            }

            public Tween Animation { get; }
            public float? TimeIndex { get; }
            public AnimationWrapper Next { get; set; }
        }

        public Flow(FlowOptions options) : base(options.AutoStart)
        {
            Options = options;
        }

        public Flow(bool autoStart = false) : this(new FlowOptions() { AutoStart = autoStart })
        {
        }

        private FlowOptions Options { get; set; }

        #region Internal Members

        private List<AnimationWrapper> timeIndexedAnimationWrappers = new List<AnimationWrapper>();
        private AnimationWrapper lastQueuedAnimationWrapper;
        private int animationsCount;

        private FastList<AnimationWrapper> orderedTimeIndexedAnimationWrappers;
        private AnimationWrapper nextTimeIndexedAnimationWrapper;

        private FastList<AnimationWrapper> runningAnimaionWrappers;
        private float time;
        private int? remainingLoops;

        #endregion

        #region Events

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
            StartInternal();
            return this;
        }

        public async Task<Flow> StartAsync()
        {
            StartInternal();
            await new AwaitableAnimation(this);
            return this;
        }

        private void Init()
        {
            time = 0;
            orderedTimeIndexedAnimationWrappers = new FastList<AnimationWrapper>(timeIndexedAnimationWrappers.OrderByDescending(w => w.TimeIndex).ToArray());
            nextTimeIndexedAnimationWrapper = orderedTimeIndexedAnimationWrappers.Last();
            orderedTimeIndexedAnimationWrappers.RemoveLast();
            runningAnimaionWrappers = new FastList<AnimationWrapper>(animationsCount);
        }

        internal override void StartInternal(bool subscribeToUpdate = true)
        {
            remainingLoops = Options.LoopCount;

            Init();

            IsSubscribedToUpdate = subscribeToUpdate;
            if (IsSubscribedToUpdate)
            {
                FlowEntController.Instance.SubscribeToUpdate(this);
            }

            OnStartCallback?.Invoke();

            PlayState = PlayState.Playing;
        }

        internal override float? UpdateInternal(float deltaTime)
        {
            time += deltaTime;

            #region TimeBased start

            while (nextTimeIndexedAnimationWrapper != null && time > nextTimeIndexedAnimationWrapper.TimeIndex)
            {
                nextTimeIndexedAnimationWrapper.Animation.StartInternal(false);
                runningAnimaionWrappers.Add(nextTimeIndexedAnimationWrapper);
                if (orderedTimeIndexedAnimationWrappers.Count > 0)
                {
                    nextTimeIndexedAnimationWrapper = orderedTimeIndexedAnimationWrappers.Last();
                    orderedTimeIndexedAnimationWrappers.RemoveLast();
                }
                else
                {
                    nextTimeIndexedAnimationWrapper = null;
                }
            }

            #endregion

            #region Updating animations

            for (int i = 0; i < runningAnimaionWrappers.Count; i++)
            {
                bool isUpdated = false;
                float runningDeltaTime = deltaTime;
                AnimationWrapper animationWrapper = runningAnimaionWrappers[i];
                do
                {
                    float? overdraft = animationWrapper.Animation.UpdateInternal(runningDeltaTime);
                    if (overdraft != null)
                    {
                        animationWrapper = runningAnimaionWrappers[i].Next;
                        if (animationWrapper != null)
                        {
                            runningAnimaionWrappers[i] = animationWrapper;
                            animationWrapper.Animation.StartInternal(false);
                            runningDeltaTime = overdraft.Value;
                        }
                        else
                        {
                            runningAnimaionWrappers.RemoveAt(i);
                            if (runningAnimaionWrappers.Count == 0 && nextTimeIndexedAnimationWrapper == null)
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

            OnCompleteCallback?.Invoke();

            PlayState = PlayState.Finished;
            return overdraft;
        }

        #endregion

        #region Setters

        public Flow OnStart(Action callback)
        {
            OnStartCallback += callback;
            return this;
        }

        public new Flow OnComplete(Action callback)
        {
            OnCompleteCallback += callback;
            return this;
        }

        public Flow Queue(Tween animation)
        {
            if (lastQueuedAnimationWrapper == null)
            {
                lastQueuedAnimationWrapper = new AnimationWrapper(animation, 0);
                timeIndexedAnimationWrappers.Add(lastQueuedAnimationWrapper);
            }
            else
            {
                AnimationWrapper animationWrapper = new AnimationWrapper(animation);
                lastQueuedAnimationWrapper.Next = animationWrapper;
                lastQueuedAnimationWrapper = animationWrapper;
            }
            animationsCount++;

            return this;
        }

        public Flow Queue(Func<Tween, Tween> createTween)
            => Queue(createTween(new Tween()));

        public Flow Queue<T>(Func<Tween, MotionWrapper<T>> createTween)
            => Queue(createTween(new Tween()).Tween);

        public Flow Queue(TweenOptions options, Func<Tween, Tween> createTween)
            => Queue(createTween(new Tween(options)));

        public Flow Queue<T>(TweenOptions options, Func<Tween, MotionWrapper<T>> createTween)
            => Queue(createTween(new Tween(options)).Tween);

        public Flow At(float timeIndex, Tween animation)
        {
            if (timeIndex < 0)
            {
                throw new ArgumentException($"Time index cannot be negative. Value: {timeIndex}");
            }

            lastQueuedAnimationWrapper = new AnimationWrapper(animation, timeIndex);
            timeIndexedAnimationWrappers.Add(lastQueuedAnimationWrapper);
            animationsCount++;

            return this;
        }

        public Flow At(float timeIndex, Func<Tween, Tween> createTween)
            => At(timeIndex, createTween(new Tween()));

        public Flow At<T>(float timeIndex, Func<Tween, MotionWrapper<T>> createTween)
            => At(timeIndex, createTween(new Tween()).Tween);

        public Flow At(float timeIndex, TweenOptions options, Func<Tween, Tween> createTween)
            => At(timeIndex, createTween(new Tween(options)));

        public Flow At<T>(float timeIndex, TweenOptions options, Func<Tween, MotionWrapper<T>> createTween)
            => At(timeIndex, createTween(new Tween(options)).Tween);

        #endregion

    }
}
