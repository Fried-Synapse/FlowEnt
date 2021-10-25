using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public partial class Tween
    {
        private float time = 1;
        private IEasing easing = TweenOptions.LinearEasing;
        private LoopType loopType;

        public Tween SetOptions(TweenOptions options)
        {
            CopyOptions(options);
            return this;
        }

        public Tween SetOptions(Func<TweenOptions, TweenOptions> optionsBuilder)
        {
            CopyOptions(optionsBuilder(new TweenOptions()));
            return this;
        }

        public Tween SetSkipFrames(int frames)
        {
            this.skipFrames = frames;
            return this;
        }

        public Tween SetDelay(float time)
        {
            this.delay = time;
            return this;
        }

        public Tween SetTime(float time)
        {
            if (time < 0)
            {
                throw new ArgumentException(TweenOptions.ErrorTimeNegative);
            }
            if (float.IsInfinity(time))
            {
                throw new ArgumentException(TweenOptions.ErrorTimeInfinity);
            }
            this.time = time;
            return this;
        }

        public Tween SetLoopCount(int? loopCount)
        {
            if (loopCount <= 0)
            {
                throw new ArgumentException(AbstractAnimationOptions.ErrorLoopCountNegative);
            }
            this.loopCount = loopCount;
            return this;
        }

        public Tween SetLoopType(LoopType loopType)
        {
            this.loopType = loopType;
            return this;
        }

        public Tween SetTimeScale(float timeScale)
        {
            if (timeScale < 0)
            {
                throw new ArgumentException(AbstractAnimationOptions.ErrorTimeScaleNegative);
            }
            this.timeScale = timeScale;
            return this;
        }

        public Tween SetEasing(IEasing easing)
        {
            this.easing = easing;
            return this;
        }

        public Tween SetEasing(Easing easing)
        {
            this.easing = EasingFactory.Create(easing);
            return this;
        }

        public Tween SetEasing(AnimationCurve animationCurve)
        {
            this.easing = new AnimationCurveEasing(animationCurve);
            return this;
        }

        private void CopyOptions(TweenOptions options)
        {
            AutoStart = options.AutoStart;
            skipFrames = options.SkipFrames;
            delay = options.Delay;
            time = options.Time;
            timeScale = options.TimeScale;
            loopCount = options.LoopCount;
            loopType = options.LoopType;
            easing = options.Easing;
        }
    }
}
