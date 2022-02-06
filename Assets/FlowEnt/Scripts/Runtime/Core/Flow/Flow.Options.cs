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
            base.SetOptions(options);
            return this;
        }

        /// <summary>
        /// Creates a builder for options and then sets all the options for this flow.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public Flow SetOptions(Func<FlowOptions, FlowOptions> optionsBuilder)
            => SetOptions(optionsBuilder(new FlowOptions()));

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetName
        public new Flow SetName(string name)
        {
            base.SetName(name);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetUpdateType
        public new Flow SetUpdateType(UpdateType updateType)
        {
            base.SetUpdateType(updateType);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetAutoStart
        public new Flow SetAutoStart(bool autoStart)
        {
            base.SetAutoStart(autoStart);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetSkipFrames
        public new Flow SetSkipFrames(int frames)
        {
            base.SetSkipFrames(frames);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelay
        public new Flow SetDelay(float time)
        {
            base.SetDelay(time);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetLoopCount
        public new Flow SetLoopCount(int? loopCount)
        {
            base.SetLoopCount(loopCount);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetTimeScale
        public new Flow SetTimeScale(float timeScale)
        {
            base.SetTimeScale(timeScale);
            return this;
        }
    }
}
