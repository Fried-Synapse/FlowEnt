using System;

namespace FriedSynapse.FlowEnt
{
    public partial class AbstractAnimation : IFluentAnimationOptionable<AbstractAnimation>
    {
        protected void SetOptions(AbstractAnimationOptions options)
        {
            Name = options.Name;
            AutoStart = options.AutoStart;
            skipFrames = options.SkipFrames;
            delay = options.Delay;
            timeScale = options.TimeScale;
            loopCount = options.LoopCount;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetName
        public AbstractAnimation SetName(string name)
        {
            Name = name;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetAutoStart
        public AbstractAnimation SetAutoStart(bool autoStart)
        {
            AutoStart = autoStart;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetSkipFrames
        public AbstractAnimation SetSkipFrames(int frames)
        {
            skipFrames = frames;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelay
        public AbstractAnimation SetDelay(float time)
        {
            delay = time;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetLoopCount
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
        /// \copydoc IFluentAnimationOptionable.SetTimeScale
        public AbstractAnimation SetTimeScale(float timeScale)
        {
            TimeScale = timeScale;
            return this;
        }
    }
}
