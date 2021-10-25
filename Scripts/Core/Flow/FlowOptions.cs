
namespace FriedSynapse.FlowEnt
{
    internal interface IFluentFlowOptionable<T>
    {
        /// <summary>
        /// Sets whether you want this flow to auto-start or not.
        /// </summary>
        /// <remarks>AutoStart will be slower than a manual start. See more at https://flowent.friedsynapse.com/autostart</remarks>
        /// <param name="autoStart"></param>
        T SetAutoStart(bool autoStart);
        /// <summary>
        /// Sets the amount of frames you want to skip at when this flow is started.
        /// </summary>
        /// <param name="frames"></param>
        T SetSkipFrames(int frames);

        /// <summary>
        /// Sets the amount of time(s) that you want to delay when this flow is started.
        /// </summary>
        /// <param name="time"></param>
        T SetDelay(float time);

        /// <summary>
        /// Sets the amount of loops you want this flow to have. If you want infinite loops pass a null value.
        /// Note: all loops are reset. No ping-pong option for flows.
        /// </summary>
        /// <param name="loopCount"></param>
        T SetLoopCount(int? loopCount);

        /// <summary>
        /// Sets the time scale for the current flow(and all it's animations).
        /// </summary>
        /// <param name="timeScale"></param>
        T SetTimeScale(float timeScale);
    }

    public class FlowOptions : AbstractAnimationOptions, IFluentFlowOptionable<FlowOptions>
    {
        public FlowOptions(bool autoStart = false) : base(autoStart)
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
