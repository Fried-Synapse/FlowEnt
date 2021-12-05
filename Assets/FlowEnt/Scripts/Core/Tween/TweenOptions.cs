using System;
using FriedSynapse.FlowEnt.Easings;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class TweenOptions : AbstractAnimationOptions,
        IFluentTweenOptionable<TweenOptions>
    {
        internal const string ErrorTimeNegative = "Value cannot be less than 0.";
        internal const string ErrorTimeInfinity = "Value cannot be infinity.";
        internal const float DefaultTime = 1f;
        internal const bool DefaultEasingReverse = false;
        internal const Easing DefaultEasing = FlowEnt.Easing.Linear;
        internal static readonly IEasing DefaultIEasing = EasingFactory.Create(DefaultEasing);

        /// <summary>
        /// Initialises a new instance of the <see cref="TweenOptions"/> class.
        /// </summary>
        public TweenOptions()
        {
        }

        private float time = 1f;
        /// <summary>
        /// The amount of time in seconds that this tween will last.
        /// </summary>
        public float Time
        {
            get { return time; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ErrorTimeNegative);
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
        public TweenOptions SetName(string name)
        {
            Name = name;
            return this;
        }

        /// <inheritdoc />
        public TweenOptions SetAutoStart(bool autoStart)
        {
            AutoStart = autoStart;
            return this;
        }

        /// <inheritdoc />
        public TweenOptions SetSkipFrames(int frames)
        {
            SkipFrames = frames;
            return this;
        }

        /// <inheritdoc />
        public TweenOptions SetDelay(float time)
        {
            Delay = time;
            return this;
        }

        /// <inheritdoc />
        public TweenOptions SetTime(float time)
        {
            Time = time;
            return this;
        }

        /// <inheritdoc />
        public TweenOptions SetTimeScale(float timeScale)
        {
            TimeScale = timeScale;
            return this;
        }

        /// <inheritdoc />
        public TweenOptions SetLoopCount(int? loopCount)
        {
            LoopCount = loopCount;
            return this;
        }

        /// <inheritdoc />
        public TweenOptions SetLoopType(LoopType loopType)
        {
            LoopType = loopType;
            return this;
        }

        /// <inheritdoc />
        public TweenOptions SetEasing(IEasing easing, bool reverse = DefaultEasingReverse)
        {
            Easing = reverse ? easing.Reverse() : easing;
            return this;
        }

        /// <inheritdoc />
        public TweenOptions SetEasing(Easing easing, bool reverse = DefaultEasingReverse)
            => SetEasing(EasingFactory.Create(easing), reverse);

        /// <inheritdoc />
        public TweenOptions SetEasing(AnimationCurve animationCurve, bool reverse = DefaultEasingReverse)
            => SetEasing(new AnimationCurveEasing(animationCurve), reverse);
    }
}
