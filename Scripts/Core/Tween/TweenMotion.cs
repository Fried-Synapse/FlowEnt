using System;
using System.Threading.Tasks;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    internal interface ITweenProxy
    {
        Tween Tween { get; }
    }

    /// <summary>
    /// Wrapper class that is used to apply motions to an object of any type using a tween
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TweenMotion<T> :
        ITweenProxy,
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

        /// <inheritdoc cref="Tween.Apply"/>
        public TweenMotion<T> Apply(IMotion motion)
        {
            Tween.Apply(motion);
            return this;
        }

        /// <inheritdoc cref="Tween.For"/>
        public TweenMotion<TElement> For<TElement>(TElement element)
            => new TweenMotion<TElement>(Tween, element);

        /// <inheritdoc cref="Tween.Start"/>
        public Tween Start()
        {
            Tween.Start();
            return Tween;
        }

        /// <inheritdoc cref="Tween.StartAsync"/>
        public async Task<Tween> StartAsync()
        {
            await Tween.StartAsync();
            return Tween;
        }

        /// <inheritdoc cref="AbstractAnimation.AsAsync"/>
        public async Task AsAsync()
        {
            await Tween.AsAsync();
        }

        #region Events

        /// <inheritdoc />
        public TweenMotion<T> OnStarting(Action callback)
        {
            Tween.OnStarting(callback);
            return this;
        }

        /// <inheritdoc />
        public TweenMotion<T> OnStarted(Action callback)
        {
            Tween.OnStarted(callback);
            return this;
        }

        /// <inheritdoc />
        public TweenMotion<T> OnUpdating(Action<float> callback)
        {
            Tween.OnUpdating(callback);
            return this;
        }

        /// <inheritdoc />
        public TweenMotion<T> OnUpdated(Action<float> callback)
        {
            Tween.OnUpdated(callback);
            return this;
        }

        /// <inheritdoc />
        public TweenMotion<T> OnLoopCompleted(Action<int?> callback)
        {
            Tween.OnLoopCompleted(callback);
            return this;
        }

        /// <inheritdoc />
        public TweenMotion<T> OnCompleting(Action callback)
        {
            Tween.OnCompleting(callback);
            return this;
        }

        /// <inheritdoc />
        public TweenMotion<T> OnCompleted(Action callback)
        {
            Tween.OnCompleted(callback);
            return this;
        }

        #endregion

        #region Options

        /// <inheritdoc />
        public TweenMotion<T> SetAutoStart(bool autoStart)
        {
            Tween.SetAutoStart(autoStart);
            return this;
        }

        /// <inheritdoc />
        public TweenMotion<T> SetTime(float time)
        {
            Tween.SetTime(time);
            return this;
        }

        /// <inheritdoc />
        public TweenMotion<T> SetEasing(IEasing easing)
        {
            Tween.SetEasing(easing);
            return this;
        }

        /// <inheritdoc />
        public TweenMotion<T> SetEasing(Easing easing)
        {
            Tween.SetEasing(easing);
            return this;
        }

        /// <inheritdoc />
        public TweenMotion<T> SetEasing(AnimationCurve animationCurve)
        {
            Tween.SetEasing(animationCurve);
            return this;
        }

        /// <inheritdoc />
        public TweenMotion<T> SetLoopType(LoopType loopType)
        {
            Tween.SetLoopType(loopType);
            return this;
        }

        /// <inheritdoc />
        public TweenMotion<T> SetLoopCount(int? loopCount)
        {
            Tween.SetLoopCount(loopCount);
            return this;
        }

        /// <inheritdoc />
        public TweenMotion<T> SetTimeScale(float timeScale)
        {
            Tween.SetTimeScale(timeScale);
            return this;
        }

        /// <inheritdoc />
        public TweenMotion<T> SetSkipFrames(int frames)
        {
            Tween.SetSkipFrames(frames);
            return this;
        }

        /// <inheritdoc />
        public TweenMotion<T> SetDelay(float time)
        {
            Tween.SetDelay(time);
            return this;
        }

        #endregion
    }
}
