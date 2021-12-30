namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides options for flows.
    /// </summary>
    public class FlowOptions : AbstractAnimationOptions, IFluentFlowOptionable<FlowOptions>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="FlowOptions"/> class.
        /// </summary>
        public FlowOptions()
        {
        }

        #region Options

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetName
        public new FlowOptions SetName(string name)
        {
            base.SetName(name);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetUpdateType
        public new FlowOptions SetUpdateType(UpdateType updateType)
        {
            base.SetUpdateType(updateType);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetAutoStart
        public new FlowOptions SetAutoStart(bool autoStart)
        {
            base.SetAutoStart(autoStart);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetSkipFrames
        public new FlowOptions SetSkipFrames(int frames)
        {
            base.SetSkipFrames(frames);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelay
        public new FlowOptions SetDelay(float time)
        {
            base.SetDelay(time);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetLoopCount
        public new FlowOptions SetLoopCount(int? loopCount)
        {
            base.SetLoopCount(loopCount);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetTimeScale
        public new FlowOptions SetTimeScale(float timeScale)
        {
            base.SetTimeScale(timeScale);
            return this;
        }

        #endregion

    }
}
