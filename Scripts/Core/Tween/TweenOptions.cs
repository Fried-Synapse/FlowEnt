using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class TweenOptions : AbstractAnimationOptions, IFluentTweenOptionable<TweenOptions>
    {
        internal const string ErrorTimeNegative = "Value cannot be less than 0.";
        internal const string ErrorTimeInfinity = "Value cannot be infinity.";
        internal static readonly IEasing LinearEasing = new LinearEasing();

        /// <summary>
        /// Initialises a new instance of the <see cref="TweenOptions"/> class.
        /// </summary>
        public TweenOptions()
        {
        }

        private float time = 1f;
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
        public LoopType LoopType { get; set; }
        public IEasing Easing { get; set; } = LinearEasing;

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
        public TweenOptions SetEasing(IEasing easing)
        {
            Easing = easing;
            return this;
        }

        /// <inheritdoc />
        public TweenOptions SetEasing(Easing easing)
        {
            Easing = EasingFactory.Create(easing);
            return this;
        }

        /// <inheritdoc />
        public TweenOptions SetEasing(AnimationCurve animationCurve)
        {
            Easing = new AnimationCurveEasing(animationCurve);
            return this;
        }
    }
}
