using System;
using System.Threading.Tasks;
using FriedSynapse.FlowEnt.Easings;
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
        /// \copydoc Tween.Apply
        public TweenMotion<T> Apply(IMotion motion)
        {
            Tween.Apply(motion);
            return this;
        }

        /// <inheritdoc cref="Tween.For"/>
        /// \copydoc Tween.For
        public TweenMotion<TElement> For<TElement>(TElement element)
            => new TweenMotion<TElement>(Tween, element);

        /// <inheritdoc cref="Tween.Start"/>
        /// \copydoc Tween.Start
        public Tween Start()
        {
            Tween.Start();
            return Tween;
        }

        /// <inheritdoc cref="Tween.StartAsync"/>
        /// \copydoc Tween.StartAsync
        public async Task<Tween> StartAsync()
        {
            await Tween.StartAsync();
            return Tween;
        }

        /// <inheritdoc cref="AbstractAnimation.AsAsync"/>
        /// \copydoc AbstractAnimation.AsAsync
        public async Task AsAsync()
        {
            await Tween.AsAsync();
        }

        #region Events

        /// <inheritdoc />
        /// \copydoc IFluentTweenEventable.OnStarting
        public TweenMotion<T> OnStarting(Action callback)
        {
            Tween.OnStarting(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarted
        public TweenMotion<T> OnStarted(Action callback)
        {
            Tween.OnStarted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenEventable.OnUpdating
        public TweenMotion<T> OnUpdating(Action<float> callback)
        {
            Tween.OnUpdating(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdated
        public TweenMotion<T> OnUpdated(Action<float> callback)
        {
            Tween.OnUpdated(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopCompleted
        public TweenMotion<T> OnLoopCompleted(Action<int?> callback)
        {
            Tween.OnLoopCompleted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenEventable.OnCompleting
        public TweenMotion<T> OnCompleting(Action callback)
        {
            Tween.OnCompleting(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleted
        public TweenMotion<T> OnCompleted(Action callback)
        {
            Tween.OnCompleted(callback);
            return this;
        }

        #endregion

        #region Options

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetName
        public TweenMotion<T> SetName(string name)
        {
            Tween.SetName(name);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetAutoStart
        public TweenMotion<T> SetAutoStart(bool autoStart)
        {
            Tween.SetAutoStart(autoStart);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetSkipFrames
        public TweenMotion<T> SetSkipFrames(int frames)
        {
            Tween.SetSkipFrames(frames);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetName
        public TweenMotion<T> SetDelay(float time)
        {
            Tween.SetDelay(time);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetTime
        public TweenMotion<T> SetTime(float time)
        {
            Tween.SetTime(time);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetLoopType
        public TweenMotion<T> SetLoopType(LoopType loopType)
        {
            Tween.SetLoopType(loopType);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetLoopCount
        public TweenMotion<T> SetLoopCount(int? loopCount)
        {
            Tween.SetLoopCount(loopCount);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetTimeScale
        public TweenMotion<T> SetTimeScale(float timeScale)
        {
            Tween.SetTimeScale(timeScale);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetEasing
        public TweenMotion<T> SetEasing(IEasing easing, bool reverse = TweenOptions.DefaultEasingReverse)
        {
            Tween.SetEasing(easing, reverse);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetEasing
        public TweenMotion<T> SetEasing(Easing easing, bool reverse = TweenOptions.DefaultEasingReverse)
        {
            Tween.SetEasing(easing, reverse);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetEasing
        public TweenMotion<T> SetEasing(AnimationCurve animationCurve, bool reverse = TweenOptions.DefaultEasingReverse)
        {
            Tween.SetEasing(animationCurve, reverse);
            return this;
        }

        #endregion
    }
}
