using System;
using FriedSynapse.FlowEnt.Easings;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides options for tween.
    /// </summary>
    public class TweenOptions : AbstractAnimationOptions, IFluentTweenOptionable<TweenOptions>
    {
        //TODO use string interpolation
        internal const string ErrorTimeMin = "Time cannot be 0.001 or less.";
        internal const string ErrorTimeInfinity = "Time cannot be infinity.";
        internal const float MinTime = 0.001f;
        public const float DefaultTime = 1f;
        public const bool DefaultEasingReverse = false;
        public const Easing DefaultEasing = FlowEnt.Easing.Linear;
        public static readonly IEasing DefaultIEasing = EasingFactory.Create(DefaultEasing);

        /// <summary>
        /// Initialises a new instance of the <see cref="TweenOptions"/> class.
        /// </summary>
        public TweenOptions()
        {
        }

        private float time = DefaultTime;
        /// <summary>
        /// The amount of time in seconds that this tween will last.
        /// </summary>
        public float Time
        {
            get => time;
            set
            {
                if (value < MinTime)
                {
                    throw new ArgumentException(ErrorTimeMin);
                }
                if (float.IsInfinity(value))
                {
                    throw new ArgumentException(ErrorTimeInfinity);
                }
                time = value;
            }
        }
        /// <summary>
        /// The easing of the tween.
        /// </summary>
        public IEasing Easing { get; set; } = DefaultIEasing;

        /// <summary>
        /// The loop type of the tween.
        /// </summary>
        public LoopType LoopType { get; set; }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetName
        public new TweenOptions SetName(string name)
        {
            base.SetName(name);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetUpdateType
        public new TweenOptions SetUpdateType(UpdateType updateType)
        {
            base.SetUpdateType(updateType);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetAutoStart
        public new TweenOptions SetAutoStart(bool autoStart)
        {
            base.SetAutoStart(autoStart);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetSkipFrames
        public new TweenOptions SetSkipFrames(int frames)
        {
            base.SetSkipFrames(frames);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelay
        public new TweenOptions SetDelay(float time)
        {
            base.SetDelay(time);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelayUntil
        public new TweenOptions SetDelayUntil(Func<bool> callback)
        {
            base.SetDelayUntil(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetWaitFor
        public new TweenOptions SetWaitFor(params AbstractAnimation[] animations)
        {
            base.SetWaitFor(animations);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetTime
        public TweenOptions SetTime(float time)
        {
            Time = time;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetTimeScale
        public new TweenOptions SetTimeScale(float timeScale)
        {
            base.SetTimeScale(timeScale);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetLoopCount
        public new TweenOptions SetLoopCount(int? loopCount)
        {
            base.SetLoopCount(loopCount);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetLoopType
        public TweenOptions SetLoopType(LoopType loopType)
        {
            LoopType = loopType;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetEasing
        public TweenOptions SetEasing(IEasing easing, bool reverse = DefaultEasingReverse)
        {
            Easing = reverse ? easing.Reverse() : easing;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetEasing
        public TweenOptions SetEasing(Easing easing, bool reverse = DefaultEasingReverse)
            => SetEasing(EasingFactory.Create(easing), reverse);

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetEasing
        public TweenOptions SetEasing(Func<float, float> easing, bool reverse = DefaultEasingReverse)
            => SetEasing(new FunctionEasing(easing), reverse);

        /// <inheritdoc />
        /// \copydoc IFluentTweenOptionable.SetEasing
        public TweenOptions SetEasing(AnimationCurve animationCurve, bool reverse = DefaultEasingReverse)
            => SetEasing(new AnimationCurveEasing(animationCurve), reverse);
    }
}
