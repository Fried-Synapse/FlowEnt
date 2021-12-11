using System;

namespace FriedSynapse.FlowEnt
{
    public partial class AbstractAnimation : IFluentAnimationOptionable<AbstractAnimation>
    {
        /// <inheritdoc />
        public AbstractAnimation SetName(string name)
        {
            Name = name;
            return this;
        }

        /// <inheritdoc />
        public AbstractAnimation SetAutoStart(bool autoStart)
        {
            AutoStart = autoStart;
            return this;
        }

        /// <inheritdoc />
        public AbstractAnimation SetSkipFrames(int frames)
        {
            skipFrames = frames;
            return this;
        }

        /// <inheritdoc />
        public AbstractAnimation SetDelay(float time)
        {
            delay = time;
            return this;
        }

        /// <inheritdoc />
        public AbstractAnimation SetLoopCount(int? loopCount)
        {
            if (loopCount <= 0)
            {
                throw new ArgumentException(AbstractAnimationOptions.ErrorLoopCountNegative);
            }
            this.loopCount = loopCount;
            return this;
        }

        /// <inheritdoc />
        public AbstractAnimation SetTimeScale(float timeScale)
        {
            TimeScale = timeScale;
            return this;
        }

        protected void CopyOptions(AbstractAnimationOptions options)
        {
            Name = options.Name;
            AutoStart = options.AutoStart;
            skipFrames = options.SkipFrames;
            delay = options.Delay;
            timeScale = options.TimeScale;
            loopCount = options.LoopCount;
        }
    }
}
