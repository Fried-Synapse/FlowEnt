using System;
using FriedSynapse.FlowEnt.Easings;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public partial class Tween
    {
        private float time = 1;
        private IEasing easing = TweenOptions.LinearEasing;
        private LoopType loopType;

        /// <summary>
        /// Sets all the options for this tween.
        /// </summary>
        /// <param name="options"></param>
        public Tween SetOptions(TweenOptions options)
        {
            CopyOptions(options);
            return this;
        }

        /// <summary>
        /// Creates a builder for options and then sets all the options for this tween.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public Tween SetOptions(Func<TweenOptions, TweenOptions> optionsBuilder)
        {
            CopyOptions(optionsBuilder(new TweenOptions()));
            return this;
        }

        /// <inheritdoc />
        public Tween SetAutoStart(bool autoStart)
        {
            AutoStart = autoStart;
            return this;
        }

        /// <inheritdoc />
        public Tween SetSkipFrames(int frames)
        {
            this.skipFrames = frames;
            return this;
        }

        /// <inheritdoc />
        public Tween SetDelay(float time)
        {
            this.delay = time;
            return this;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public Tween SetLoopCount(int? loopCount)
        {
            if (loopCount <= 0)
            {
                throw new ArgumentException(AbstractAnimationOptions.ErrorLoopCountNegative);
            }
            this.loopCount = loopCount;
            return this;
        }

        /// <inheritdoc />
        public Tween SetLoopType(LoopType loopType)
        {
            this.loopType = loopType;
            return this;
        }

        /// <inheritdoc />
        public Tween SetTimeScale(float timeScale)
        {
            TimeScale = timeScale;
            return this;
        }

        /// <inheritdoc />
        public Tween SetEasing(IEasing easing)
        {
            this.easing = easing;
            return this;
        }

        /// <inheritdoc />
        public Tween SetEasing(Easing easing)
        {
            this.easing = EasingFactory.Create(easing);
            return this;
        }

        /// <inheritdoc />
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
