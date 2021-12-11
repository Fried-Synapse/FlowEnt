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
        public new Flow SetName(string name)
        {
            base.SetName(name);
            return this;
        }

        /// <inheritdoc />
        public new Flow SetAutoStart(bool autoStart)
        {
            base.SetAutoStart(autoStart);
            return this;
        }

        /// <inheritdoc />
        public new Flow SetSkipFrames(int frames)
        {
            base.SetSkipFrames(frames);
            return this;
        }

        /// <inheritdoc />
        public new Flow SetDelay(float time)
        {
            base.SetDelay(time);
            return this;
        }

        /// <inheritdoc />
        public new Flow SetLoopCount(int? loopCount)
        {
            base.SetLoopCount(loopCount);
            return this;
        }

        /// <inheritdoc />
        public new Flow SetTimeScale(float timeScale)
        {
            base.SetTimeScale(timeScale);
            return this;
        }

        private void CopyOptions(FlowOptions options)
            => base.CopyOptions(options);
    }
}
