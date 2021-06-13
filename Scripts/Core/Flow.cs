using System;
using System.Collections.Generic;
using System.Linq;

namespace FlowEnt
{
    public sealed class Flow : AbstractAnimation
    {
        private class AnimationWrapper
        {
            public AnimationWrapper(AbstractAnimation animation, float? startingTime = null)
            {
                Animation = animation;
                TimeIndex = startingTime;
            }

            public AbstractAnimation Animation { get; }
            public float? TimeIndex { get; }
            public AnimationWrapper Next { get; set; }
            public int Index { get; set; }
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



        private AnimationWrapper[] runningAnimaionWrappers;
        private int runningAnimaionWrappersIndex;

        #endregion

        #region Events

        protected override void OnAutoStart(float deltaTime)
        {
            if (PlayState != PlayState.Building)
            {
                return;
            }

            Start();
            Update(deltaTime);
        }

        public Flow Start()
        {
            orderedTimeIndexedAnimationWrappers = TimeIndexedAnimationWrappers.OrderBy(w => w.TimeIndex).ToArray();
            nextTimeIndexedAnimationWrapper = orderedTimeIndexedAnimationWrappers[nextTimeIndexedAnimationIndex];
            nextTimeIndexedAnimationIndex++;
            runningAnimaionWrappers = new AnimationWrapper[AnimationsCount];
            FlowEntController.Instance.SubscribeToUpdate(this);

            return this;
        }

        public override void Update(float deltaTime)
        {
            time += deltaTime;

            while (time > nextTimeIndexedAnimationWrapper.TimeIndex)
            {
                nextTimeIndexedAnimationWrapper.Animation.Start();
                runningAnimaionWrappers[runningAnimaionWrappersIndex] = nextTimeIndexedAnimationWrapper;
                runningAnimaionWrappersIndex++;
                if (nextTimeIndexedAnimationIndex < orderedTimeIndexedAnimationWrappers.Length)
                {
                    nextTimeIndexedAnimationWrapper = orderedTimeIndexedAnimationWrappers[nextTimeIndexedAnimationIndex];
                    nextTimeIndexedAnimationIndex++;
                }
            }

            for (int i = 0; i < runningAnimaionWrappers.Length; i++)
            {
                runningAnimaionWrappers[i].Animation.Update(deltaTime);
            }
        }

        #endregion

        #region Setters

        public Flow Queue(AbstractAnimation animation)
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

        public Flow At(float timeIndex, AbstractAnimation animation)
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

        #endregion

    }
}
