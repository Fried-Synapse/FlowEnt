using System;

namespace FriedSynapse.FlowEnt
{
    public partial class Flow : IFluentFlowOptionable<Flow>
    {
        /// <summary>
        /// Sets all the options for this flow.
        /// </summary>
        /// <param name="options"></param>
        public Flow SetOptions(FlowOptions options)
        {
            CopyOptions(options);
            return this;
        }

        /// <summary>
        /// Creates a builder for options and then sets all the options for this flow.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public Flow SetOptions(Func<FlowOptions, FlowOptions> optionsBuilder)
        {
            CopyOptions(optionsBuilder(new FlowOptions()));
            return this;
        }

        /// <inheritdoc />
        public Flow SetAutoStart(bool autoStart)
        {
            AutoStart = autoStart;
            return this;
        }

        /// <inheritdoc />
        public Flow SetSkipFrames(int frames)
        {
            this.skipFrames = frames;
            return this;
        }

        /// <inheritdoc />
        public Flow SetDelay(float time)
        {
            this.delay = time;
            return this;
        }


        /// <inheritdoc />
        public Flow SetLoopCount(int? loopCount)
        {
            if (loopCount <= 0)
            {
                throw new ArgumentException(AbstractAnimationOptions.ErrorLoopCountNegative);
            }
            this.loopCount = loopCount;
            return this;
        }


        /// <inheritdoc />
        public Flow SetTimeScale(float timeScale)
        {
            if (timeScale < 0)
            {
                throw new ArgumentException(AbstractAnimationOptions.ErrorTimeScaleNegative);
            }
            this.timeScale = timeScale;
            return this;
        }

        private void CopyOptions(FlowOptions options)
        {
            SetAutoStart(options.AutoStart);
            skipFrames = options.SkipFrames;
            delay = options.Delay;
            loopCount = options.LoopCount;
            timeScale = options.TimeScale;
        }
    }
}
