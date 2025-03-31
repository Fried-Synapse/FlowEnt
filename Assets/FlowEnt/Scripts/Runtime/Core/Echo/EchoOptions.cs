using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides options for echo.
    /// </summary>
    public class EchoOptions : AbstractAnimationOptions, IFluentEchoOptionable<EchoOptions>
    {
        //TODO use string interpolation
        internal const string ErrorTimeoutMin = "Timeout cannot be 0.001 or less.";
        internal const string ErrorTimeoutInfinity = "Timeout cannot be infinity.";
        internal const float MinTime = 0.001f;
        public const float DefaultTime = 1f;

        /// <summary>
        /// Initialises a new instance of the <see cref="EchoOptions"/> class.
        /// </summary>
        public EchoOptions()
        {
        }

        private float? timeout;

        /// <summary>
        /// The amount of time in seconds that this echo will last.
        /// </summary>
        public float? Timeout
        {
            get => timeout;
            set
            {
                if (value < MinTime)
                {
                    throw new ArgumentException(ErrorTimeoutMin);
                }

                if (value != null && float.IsInfinity(value.Value))
                {
                    throw new ArgumentException(ErrorTimeoutInfinity);
                }

                timeout = value != null && float.IsInfinity(value.Value) ? null : value;
            }
        }

        /// <summary>
        /// The condition that when true, it will stop the echo.
        /// </summary>
        public Func<float, bool> StopCondition { get; set; }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetName
        public new EchoOptions SetName(string name)
        {
            base.SetName(name);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetUpdateType
        public new EchoOptions SetUpdateType(UpdateType updateType)
        {
            base.SetUpdateType(updateType);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetAutoStart
        public new EchoOptions SetAutoStart(bool autoStart)
        {
            base.SetAutoStart(autoStart);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetSkipFrames
        public new EchoOptions SetSkipFrames(int frames)
        {
            base.SetSkipFrames(frames);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelay
        public new EchoOptions SetDelay(float time)
        {
            base.SetDelay(time);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelayUntil
        public new EchoOptions SetDelayUntil(Func<bool> callback)
        {
            base.SetDelayUntil(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetWaitFor
        public new EchoOptions SetWaitFor(params AbstractAnimation[] animations)
        {
            base.SetWaitFor(animations);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetTimeScale
        public new EchoOptions SetTimeScale(float timeScale)
        {
            base.SetTimeScale(timeScale);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetLoopCount
        public new EchoOptions SetLoopCount(int? loopCount)
        {
            base.SetLoopCount(loopCount);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentEchoOptionable.SetTime
        public EchoOptions SetTimeout(float? timeout)
        {
            Timeout = timeout;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentEchoOptionable.SetStopCondition
        public EchoOptions SetStopCondition(Func<float, bool> stopCondition)
        {
            StopCondition = stopCondition;
            return this;
        }
    }
}