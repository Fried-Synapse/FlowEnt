using System;
using System.Threading.Tasks;
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

        public Tween Start()
        {
            Tween.Start();
            return Tween;
        }

        public async Task<Tween> StartAsync()
        {
            await Tween.StartAsync();
            return Tween;
        }

        public async Task AsAsync()
        {
            await Tween.AsAsync();
        }

        #region Events

        public TweenMotion<T> OnStarting(Action callback)
        {
            Tween.OnStarting(callback);
            return this;
        }

        public TweenMotion<T> OnStarted(Action callback)
        {
            Tween.OnStarted(callback);
            return this;
        }

        public TweenMotion<T> OnUpdating(Action<float> callback)
        {
            Tween.OnUpdating(callback);
            return this;
        }

        public TweenMotion<T> OnUpdated(Action<float> callback)
        {
            Tween.OnUpdated(callback);
            return this;
        }

        public TweenMotion<T> OnLoopCompleted(Action<int?> callback)
        {
            Tween.OnLoopCompleted(callback);
            return this;
        }

        public TweenMotion<T> OnCompleted(Action callback)
        {
            Tween.OnCompleted(callback);
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

        public TweenMotion<T> SetSkipFrames(int frames)
        {
            Tween.SetSkipFrames(frames);
            return this;
        }

        public TweenMotion<T> SetDelay(float time)
        {
            Tween.SetDelay(time);
            return this;
        }

        #endregion
    }
}
