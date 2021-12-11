using System;
using FriedSynapse.FlowEnt.Easings;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public partial class Tween : IFluentTweenOptionable<Tween>
    {
        private float time = TweenOptions.DefaultTime;
        private IEasing easing = TweenOptions.DefaultIEasing;
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
        public new Tween SetName(string name)
        {
            base.SetName(name);
            return this;
        }

        /// <inheritdoc />
        public new Tween SetAutoStart(bool autoStart)
        {
            base.SetAutoStart(autoStart);
            return this;
        }

        /// <inheritdoc />
        public new Tween SetSkipFrames(int frames)
        {
            base.SetSkipFrames(frames);
            return this;
        }

        /// <inheritdoc />
        public new Tween SetDelay(float time)
        {
            base.SetDelay(time);
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
        public new Tween SetLoopCount(int? loopCount)
        {
            base.SetLoopCount(loopCount);
            return this;
        }

        /// <inheritdoc />
        public Tween SetLoopType(LoopType loopType)
        {
            this.loopType = loopType;
            return this;
        }

        /// <inheritdoc />
        public new Tween SetTimeScale(float timeScale)
        {
            base.SetTimeScale(timeScale);
            return this;
        }

        /// <inheritdoc />
        public Tween SetEasing(IEasing easing, bool reverse = TweenOptions.DefaultEasingReverse)
        {
            this.easing = reverse ? easing.Reverse() : easing;
            return this;
        }

        /// <inheritdoc />
        public Tween SetEasing(Easing easing, bool reverse = TweenOptions.DefaultEasingReverse)
            => SetEasing(EasingFactory.Create(easing), reverse);

        /// <inheritdoc />
        public Tween SetEasing(AnimationCurve animationCurve, bool reverse = TweenOptions.DefaultEasingReverse)
            => SetEasing(new AnimationCurveEasing(animationCurve), reverse);

        private void CopyOptions(TweenOptions options)
        {
            base.CopyOptions(options);
            time = options.Time;
            loopType = options.LoopType;
            easing = options.Easing;
        }
    }
}
