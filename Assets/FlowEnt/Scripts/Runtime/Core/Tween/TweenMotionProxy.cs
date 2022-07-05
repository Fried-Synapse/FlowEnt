using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FriedSynapse.FlowEnt.Easings;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Wrapper class that is used to apply motions to an object of any type using a tween
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class TweenMotionProxy<TItem> :
        IFluentControllable<TweenMotionProxy<TItem>>,
        IFluentTweenOptionable<TweenMotionProxy<TItem>>,
        IFluentTweenEventable<TweenMotionProxy<TItem>>
        where TItem : class
    {
        public TweenMotionProxy(Tween tween, TItem item)
        {
            Tween = tween;
            Item = item;
        }

        public Tween Tween { get; }
        public TItem Item { get; }

        public static implicit operator Tween(TweenMotionProxy<TItem> proxy) => proxy.Tween;

        #region Motions

        /// <inheritdoc cref="Tween.Apply(ITweenMotion[])"/>
        /// \copydoc Tween.Apply
        public TweenMotionProxy<TItem> Apply(params ITweenMotion[] motions)
        {
            Tween.Apply(motions);
            return this;
        }

        /// <inheritdoc cref="Tween.Apply(ITweenMotion[])"/>
        /// \copydoc Tween.Apply
        public TweenMotionProxy<TItem> Apply(IEnumerable<ITweenMotion> motions)
        {
            Tween.Apply(motions);
            return this;
        }

        /// <inheritdoc cref="Tween.For{TItem}(TItem)"/>
        /// \copydoc Tween.For
        public TweenMotionProxy<TItem2> For<TItem2>(TItem2 item)
            where TItem2 : class
            => new TweenMotionProxy<TItem2>(Tween, item);

        /// <inheritdoc cref="Tween.For{TItem}(TItem[])"/>
        /// \copydoc Tween.For
        public TweenMotionProxyArray<TItem2> For<TItem2>(params TItem2[] elements)
            where TItem2 : class
        {
            return new TweenMotionProxyArray<TItem2>(Tween, elements);
        }

        /// <inheritdoc cref="Tween.ForAll{TItem}(IEnumerable{TItem})"/>
        /// \copydoc Tween.ForAll
        public TweenMotionProxyArray<TItem2> ForAll<TItem2>(IEnumerable<TItem2> elements)
            where TItem2 : class
        {
            return new TweenMotionProxyArray<TItem2>(Tween, elements.ToArray());
        }

        #endregion

        #region Controls

        /// <inheritdoc cref="Tween.StartAsync(CancellationToken?)"/>
        /// \copydoc Tween.StartAsync(CancellationToken?)
        public TweenMotionProxy<TItem> Start()
        {
            Tween.Start();
            return this;
        }

        /// <inheritdoc cref="Tween.StartAsync"/>
        /// \copydoc Tween.StartAsync
        public async Task<TweenMotionProxy<TItem>> StartAsync(CancellationToken? token = null)
        {
            await Tween.StartAsync(token);
            return this;
        }

        /// <inheritdoc cref="Tween.Resume"/>
        /// \copydoc Tween.Resume
        public TweenMotionProxy<TItem> Resume()
        {
            Tween.Resume();
            return this;
        }

        /// <inheritdoc cref="Tween.Pause"/>
        /// \copydoc Tween.Pause
        public TweenMotionProxy<TItem> Pause()
        {
            Tween.Pause();
            return this;
        }

        /// <inheritdoc cref="Tween.Stop(bool)"/>
        /// \copydoc Tween.Stop
        public TweenMotionProxy<TItem> Stop(bool triggerOnCompleted = false)
        {
            Tween.Stop(triggerOnCompleted);
            return this;
        }

        /// <inheritdoc cref="Tween.Reset"/>
        /// \copydoc Tween.Reset
        public TweenMotionProxy<TItem> Reset()
        {
            Tween.Reset();
            return this;
        }

        /// <inheritdoc cref="AbstractAnimation.AsAsync"/>
        /// \copydoc AbstractAnimation.AsAsync
        public async Task AsAsync()
        {
            await Tween.AsAsync();
        }

        #endregion

        #region Events

        /// <inheritdoc />
        /// \copydoc IFluentTweenEventable.OnStarting
        public TweenMotionProxy<TItem> OnStarting(Action callback)
        {
            Tween.OnStarting(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarted
        public TweenMotionProxy<TItem> OnStarted(Action callback)
        {
            Tween.OnStarted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenEventable.OnUpdating
        public TweenMotionProxy<TItem> OnUpdating(Action<float> callback)
        {
            Tween.OnUpdating(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdated
        public TweenMotionProxy<TItem> OnUpdated(Action<float> callback)
        {
            Tween.OnUpdated(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopStarted
        public TweenMotionProxy<TItem> OnLoopStarted(Action<int?> callback)
        {
            Tween.OnLoopStarted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopCompleted
        public TweenMotionProxy<TItem> OnLoopCompleted(Action<int?> callback)
        {
            Tween.OnLoopCompleted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenEventable.OnCompleting
        public TweenMotionProxy<TItem> OnCompleting(Action callback)
        {
            Tween.OnCompleting(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleted
        public TweenMotionProxy<TItem> OnCompleted(Action callback)
        {
            Tween.OnCompleted(callback);
            return this;
        }

        #endregion

        #region Options

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetName
        public TweenMotionProxy<TItem> SetName(string name)
        {
            Tween.SetName(name);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetUpdateType
        public TweenMotionProxy<TItem> SetUpdateType(UpdateType updateType)
        {
            Tween.SetUpdateType(updateType);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetAutoStart
        public TweenMotionProxy<TItem> SetAutoStart(bool autoStart)
        {
            Tween.SetAutoStart(autoStart);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetSkipFrames
        public TweenMotionProxy<TItem> SetSkipFrames(int frames)
        {
            Tween.SetSkipFrames(frames);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetName
        public TweenMotionProxy<TItem> SetDelay(float time)
        {
            Tween.SetDelay(time);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetTime
        public TweenMotionProxy<TItem> SetTime(float time)
        {
            Tween.SetTime(time);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetLoopType
        public TweenMotionProxy<TItem> SetLoopType(LoopType loopType)
        {
            Tween.SetLoopType(loopType);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetLoopCount
        public TweenMotionProxy<TItem> SetLoopCount(int? loopCount)
        {
            Tween.SetLoopCount(loopCount);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetTimeScale
        public TweenMotionProxy<TItem> SetTimeScale(float timeScale)
        {
            Tween.SetTimeScale(timeScale);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetEasing
        public TweenMotionProxy<TItem> SetEasing(IEasing easing, bool reverse = TweenOptions.DefaultEasingReverse)
        {
            Tween.SetEasing(easing, reverse);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetEasing
        public TweenMotionProxy<TItem> SetEasing(Easing easing, bool reverse = TweenOptions.DefaultEasingReverse)
        {
            Tween.SetEasing(easing, reverse);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetEasing
        public TweenMotionProxy<TItem> SetEasing(AnimationCurve animationCurve, bool reverse = TweenOptions.DefaultEasingReverse)
        {
            Tween.SetEasing(animationCurve, reverse);
            return this;
        }

        #endregion
    }
}
