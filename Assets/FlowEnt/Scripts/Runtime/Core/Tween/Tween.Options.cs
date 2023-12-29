using System;
using FriedSynapse.FlowEnt.Easings;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public partial class Tween : IFluentTweenOptionable<Tween>
    {
        private float time = TweenOptions.DefaultTime;
        public float Time => time;
        private IEasing easing = TweenOptions.DefaultIEasing;
        public IEasing Easing => easing;
        private LoopType loopType;
        public LoopType LoopType => loopType;

        /// <inheritdoc cref="AbstractAnimation.ConditionalInternal{TAnimation}(Func{bool}, Action{TAnimation})" />
        /// \copydoc AbstractAnimation.ConditionalInternal{TAnimation}(Func{bool}, Action{TAnimation})
        public Tween Conditional(Func<bool> condition, Action<Tween> onConditionTrue)
            => ConditionalInternal(condition, onConditionTrue);

        /// <summary>
        /// Sets all the options for this tween.
        /// </summary>
        /// <param name="options"></param>
        public Tween SetOptions(TweenOptions options)
        {
            base.SetOptions(options);
            time = options.Time;
            loopType = options.LoopType;
            easing = options.Easing;
            return this;
        }

        /// <summary>
        /// Creates a builder for options and then sets all the options for this tween.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public Tween SetOptions(Func<TweenOptions, TweenOptions> optionsBuilder)
            => SetOptions(optionsBuilder(new TweenOptions()));

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetName
        public new Tween SetName(string name)
        {
            base.SetName(name);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetUpdateType
        public new Tween SetUpdateType(UpdateType updateType)
        {
            base.SetUpdateType(updateType);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetAutoStart
        public new Tween SetAutoStart(bool autoStart)
        {
            base.SetAutoStart(autoStart);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetSkipFrames
        public new Tween SetSkipFrames(int frames)
        {
            base.SetSkipFrames(frames);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelay
        public new Tween SetDelay(float time)
        {
            base.SetDelay(time);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelayUntil
        public new Tween SetDelayUntil(Func<bool> callback)
        {
            base.SetDelayUntil(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetTime
        public Tween SetTime(float time)
        {
            if (time < TweenOptions.MinTime)
            {
                throw new ArgumentException(TweenOptions.ErrorTimeMin);
            }
            if (float.IsInfinity(time))
            {
                throw new ArgumentException(TweenOptions.ErrorTimeInfinity);
            }
            this.time = time;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetLoopCount
        public new Tween SetLoopCount(int? loopCount)
        {
            base.SetLoopCount(loopCount);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetLoopType
        public Tween SetLoopType(LoopType loopType)
        {
            this.loopType = loopType;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetTimeScale
        public new Tween SetTimeScale(float timeScale)
        {
            base.SetTimeScale(timeScale);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetEasing
        public Tween SetEasing(IEasing easing, bool reverse = TweenOptions.DefaultEasingReverse)
        {
            this.easing = reverse ? new ReverseEasing(easing) : easing;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetEasing
        public Tween SetEasing(Easing easing, bool reverse = TweenOptions.DefaultEasingReverse)
            => SetEasing(EasingFactory.Create(easing), reverse);

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetEasing
        public Tween SetEasing(Func<float, float> easing, bool reverse = TweenOptions.DefaultEasingReverse)
            => SetEasing(new FunctionEasing(easing), reverse);

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetEasing
        public Tween SetEasing(AnimationCurve animationCurve, bool reverse = TweenOptions.DefaultEasingReverse)
            => SetEasing(new AnimationCurveEasing(animationCurve), reverse);
    }
}
