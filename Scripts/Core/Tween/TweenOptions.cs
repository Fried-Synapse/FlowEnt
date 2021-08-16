using System;
using UnityEngine;

namespace FlowEnt
{
    public class TweenOptions : AbstractAnimationOptions, IFluentTweenOptionable<TweenOptions>
    {
        internal const string ErrorTimeNegative = "Value cannot be less than 0.";
        internal static readonly IEasing LinearEasing = new LinearEasing();

        public TweenOptions(bool autoStart = false) : base(autoStart)
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
                time = value;
            }
        }
        public LoopType LoopType { get; set; }
        public IEasing Easing { get; set; } = LinearEasing;

        public TweenOptions SetAutoStart(bool autoStart)
        {
            AutoStart = autoStart;
            return this;
        }

        public TweenOptions SetSkipFrames(int frames)
        {
            SkipFrames = frames;
            return this;
        }

        public TweenOptions SetDelay(float time)
        {
            Delay = time;
            return this;
        }

        public TweenOptions SetTime(float time)
        {
            Time = time;
            return this;
        }

        public TweenOptions SetTimeScale(float timeScale)
        {
            TimeScale = timeScale;
            return this;
        }

        public TweenOptions SetLoopCount(int? loopCount)
        {
            LoopCount = loopCount;
            return this;
        }

        public TweenOptions SetLoopType(LoopType loopType)
        {
            LoopType = loopType;
            return this;
        }

        public TweenOptions SetEasing(IEasing easing)
        {
            Easing = easing;
            return this;
        }

        public TweenOptions SetEasing(Easing easing)
        {
            Easing = EasingFactory.Create(easing);
            return this;
        }

        public TweenOptions SetEasing(AnimationCurve animationCurve)
        {
            Easing = new AnimationCurveEasing(animationCurve);
            return this;
        }


    }
}
