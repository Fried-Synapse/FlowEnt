using System;
using UnityEngine;

namespace FlowEnt
{
    public class TweenMotion<T> :
        IFluentTweenOptionable<TweenMotion<T>>,
        IFluentTweenEventable<TweenMotion<T>>
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

        #region Events

        public TweenMotion<T> OnBeforeStart(Action callback)
        {
            Tween.OnBeforeStart(callback);
            return this;
        }

        public TweenMotion<T> OnAfterStart(Action callback)
        {
            Tween.OnAfterStart(callback);
            return this;
        }

        public TweenMotion<T> OnBeforeUpdate(Action<float> callback)
        {
            Tween.OnBeforeUpdate(callback);
            return this;
        }

        public TweenMotion<T> OnAfterUpdate(Action<float> callback)
        {
            Tween.OnAfterUpdate(callback);
            return this;
        }

        public TweenMotion<T> OnBeforeComplete(Action callback)
        {
            Tween.OnBeforeComplete(callback);
            return this;
        }

        public TweenMotion<T> OnAfterComplete(Action callback)
        {
            Tween.OnAfterComplete(callback);
            return this;
        }

        #endregion

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
