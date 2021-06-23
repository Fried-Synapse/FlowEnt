using UnityEngine;

namespace FlowEnt
{
    internal interface IFluentTweenOptionable<T>
    {
        T SetTime(float time);

        T SetEasing(IEasing easing);

        T SetEasing(Easing easing);

        T SetEasing(AnimationCurve animationCurve);

        T SetLoopType(LoopType loopType);

        T SetLoopCount(int? loopCount);
    }

    public class TweenOptions : AbstractAnimationOptions, IFluentTweenOptionable<TweenOptions>
    {
        private static readonly IEasing LinearEasing = new LinearEasing();

        public float Time { get; set; } = 1f;
        public LoopType LoopType { get; set; } = LoopType.Reset;
        public int? LoopCount { get; set; } = 1;
        public IEasing Easing { get; set; } = LinearEasing;

        public TweenOptions SetAutoStart(bool autoStart)
        {
            AutoStart = autoStart;
            return this;
        }

        public TweenOptions SetTime(float time)
        {
            Time = time;
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

        public TweenOptions SetLoopType(LoopType loopType)
        {
            LoopType = loopType;
            return this;
        }

        public TweenOptions SetLoopCount(int? loopCount)
        {
            LoopCount = loopCount;
            return this;
        }
    }
}
