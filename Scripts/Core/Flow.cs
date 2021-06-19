using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowEnt
{
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

        public Flow(bool autoStart = false) : base(autoStart)
        {
            TimeIndexedAnimationWrappers = new List<AnimationWrapper>();
        }

        private AnimationWrapper LastQueuedAnimationWrapper { get; set; }
        private List<AnimationWrapper> TimeIndexedAnimationWrappers { get; }
        private int AnimationsCount { get; set; }

        #region Internal Members

        private float time;

        private AnimationWrapper[] orderedTimeIndexedAnimationWrappers;
        private AnimationWrapper nextTimeIndexedAnimationWrapper;
        private int nextTimeIndexedAnimationIndex;

        private FastList<AnimationWrapper> runningAnimaionWrappers;

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

        internal override void StartInternal(bool subscribeToUpdate = true)
        {
            orderedTimeIndexedAnimationWrappers = TimeIndexedAnimationWrappers.OrderBy(w => w.TimeIndex).ToArray();
            nextTimeIndexedAnimationWrapper = orderedTimeIndexedAnimationWrappers[nextTimeIndexedAnimationIndex];
            nextTimeIndexedAnimationIndex++;
            runningAnimaionWrappers = new FastList<AnimationWrapper>(AnimationsCount);

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
                if (nextTimeIndexedAnimationIndex < orderedTimeIndexedAnimationWrappers.Length)
                {
                    nextTimeIndexedAnimationWrapper = orderedTimeIndexedAnimationWrappers[nextTimeIndexedAnimationIndex];
                    nextTimeIndexedAnimationIndex++;
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
                AnimationWrapper animationWrapper = runningAnimaionWrappers[i];
                do
                {
                    float? overdraft = animationWrapper.Animation.UpdateInternal(deltaTime);
                    if (overdraft != null)
                    {
                        animationWrapper = runningAnimaionWrappers[i].Next;
                        if (animationWrapper != null)
                        {
                            runningAnimaionWrappers[i] = animationWrapper;
                            animationWrapper.Animation.StartInternal(false);
                        }
                        else
                        {
                            runningAnimaionWrappers.RemoveAt(i);
                            if (runningAnimaionWrappers.Count == 0)
                            {
                                CompleteLoop();
                                return overdraft;
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

        private void CompleteLoop()
        {
            if (IsSubscribedToUpdate)
            {
                FlowEntController.Instance.UnsubscribeFromUpdate(this);
            }

            OnCompleteCallback?.Invoke();

            PlayState = PlayState.Finished;
        }

        #endregion

        #region Setters

        public Flow OnStart(Action callback)
        {
            OnStartCallback += callback;
            return this;
        }

        public Flow OnComplete(Action callback)
        {
            OnCompleteCallback += callback;
            return this;
        }

        public Flow Queue(Tween animation)
        {
            if (LastQueuedAnimationWrapper == null)
            {
                LastQueuedAnimationWrapper = new AnimationWrapper(animation, 0);
                TimeIndexedAnimationWrappers.Add(LastQueuedAnimationWrapper);
            }
            else
            {
                AnimationWrapper animationWrapper = new AnimationWrapper(animation);
                LastQueuedAnimationWrapper.Next = animationWrapper;
                LastQueuedAnimationWrapper = animationWrapper;
            }
            AnimationsCount++;

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

            LastQueuedAnimationWrapper = new AnimationWrapper(animation, timeIndex);
            TimeIndexedAnimationWrappers.Add(LastQueuedAnimationWrapper);
            AnimationsCount++;

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
