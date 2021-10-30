namespace FriedSynapse.FlowEnt
{
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
        public FlowOptions SetAutoStart(bool autoStart)
        {
            AutoStart = autoStart;
            return this;
        }

        /// <inheritdoc />
        public FlowOptions SetSkipFrames(int frames)
        {
            SkipFrames = frames;
            return this;
        }

        /// <inheritdoc />
        public FlowOptions SetDelay(float time)
        {
            Delay = time;
            return this;
        }

        /// <inheritdoc />
        public FlowOptions SetLoopCount(int? loopCount)
        {
            LoopCount = loopCount;
            return this;
        }

        /// <inheritdoc />
        public FlowOptions SetTimeScale(float timeScale)
        {
            TimeScale = timeScale;
            return this;
        }

        #endregion

    }
}
