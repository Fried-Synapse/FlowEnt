using System;
using UnityEngine;

namespace FlowEnt
{
    public class TweenMotion<T> : IFluentTweenOptionable<TweenMotion<T>>
    {
        public TweenMotion(Tween tween, T item)
        {
            Tween = tween;
            Item = item;
        }

        public Tween Tween { get; }
        public T Item { get; }
        public static implicit operator Tween(TweenMotion<T> motionWrapper) => motionWrapper.Tween;

        public TweenMotion<T> Apply(IMotion motion)
        {
            Tween.Apply(motion);
            return this;
        }

        public TweenMotion<TElement> For<TElement>(TElement element)
            => new TweenMotion<TElement>(Tween, element);

        #region Options

        public TweenMotion<T> SetTime(float time)
        {
            Tween.SetTime(time);
            return this;
        }

        public TweenMotion<T> SetEasing(IEasing easing)
        {
            Tween.SetEasing(easing);
            return this;
        }

        public TweenMotion<T> SetEasing(Easing easing)
        {
            Tween.SetEasing(easing);
            return this;
        }

        public TweenMotion<T> SetEasing(AnimationCurve animationCurve)
        {
            Tween.SetEasing(animationCurve);
            return this;
        }

        public TweenMotion<T> SetLoopType(LoopType loopType)
        {
            Tween.SetLoopType(loopType);
            return this;
        }

        public TweenMotion<T> SetLoopCount(int? loopCount)
        {
            Tween.SetLoopCount(loopCount);
            return this;
        }

        public TweenMotion<T> SetTimeScale(float timeScale)
        {
            Tween.SetTimeScale(timeScale);
            return this;
        }

        #endregion
    }
}
