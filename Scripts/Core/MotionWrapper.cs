using System;
using UnityEngine;

namespace FlowEnt
{
    public class MotionWrapper<T> : IFluentTweenOptionable<MotionWrapper<T>>
    {
        public MotionWrapper(Tween tween, T item)
        {
            Tween = tween;
            Item = item;
        }

        public Tween Tween { get; }
        public T Item { get; }
        public static implicit operator Tween(MotionWrapper<T> motionWrapper) => motionWrapper.Tween;

        public MotionWrapper<T> Apply(IMotion motion)
        {
            Tween.Apply(motion);
            return this;
        }

        public MotionWrapper<TElement> For<TElement>(TElement element)
            => new MotionWrapper<TElement>(Tween, element);

        #region Options

        public MotionWrapper<T> SetAutoStart(bool autoStart)
        {
            Tween.SetAutoStart(autoStart);
            return this;
        }

        public MotionWrapper<T> SetTime(float time)
        {
            Tween.SetTime(time);
            return this;
        }

        public MotionWrapper<T> SetEasing(IEasing easing)
        {
            Tween.SetEasing(easing);
            return this;
        }

        public MotionWrapper<T> SetEasing(Easing easing)
        {
            Tween.SetEasing(easing);
            return this;
        }

        public MotionWrapper<T> SetEasing(AnimationCurve animationCurve)
        {
            Tween.SetEasing(animationCurve);
            return this;
        }

        public MotionWrapper<T> SetLoopType(LoopType loopType)
        {
            Tween.SetLoopType(loopType);
            return this;
        }

        public MotionWrapper<T> SetLoopCount(int? loopCount)
        {
            Tween.SetLoopCount(loopCount);
            return this;
        }

        #endregion
    }
}
