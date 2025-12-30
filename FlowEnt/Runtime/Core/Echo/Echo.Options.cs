using System;

namespace FriedSynapse.FlowEnt
{
    public partial class Echo : IFluentEchoOptionable<Echo>
    {
        private float? timeout;
        public float? Timeout => timeout;
        private Func<float, bool> stopCondition;
        public Func<float, bool> StopCondition => stopCondition;

        /// <inheritdoc cref="AbstractAnimation.ConditionalInternal{TAnimation}(Func{bool}, Action{TAnimation})" />
        /// \copydoc AbstractAnimation.ConditionalInternal{TAnimation}(Func{bool}, Action{TAnimation})
        public Echo Conditional(Func<bool> condition, Action<Echo> onConditionTrue)
            => ConditionalInternal(condition, onConditionTrue);

        /// <summary>
        /// Sets all the options for this tween.
        /// </summary>
        /// <param name="options"></param>
        public Echo SetOptions(EchoOptions options)
        {
            base.SetOptions(options);
            timeout = options.Timeout;
            stopCondition = options.StopCondition;
            return this;
        }

        /// <summary>
        /// Creates a builder for options and then sets all the options for this echo.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public Echo SetOptions(Func<EchoOptions, EchoOptions> optionsBuilder)
            => SetOptions(optionsBuilder(new EchoOptions()));

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetName
        public new Echo SetName(string name)
        {
            base.SetName(name);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetUpdateType
        public new Echo SetUpdateType(UpdateType updateType)
        {
            base.SetUpdateType(updateType);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetAutoStart
        public new Echo SetAutoStart(bool autoStart)
        {
            base.SetAutoStart(autoStart);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetSkipFrames
        public new Echo SetSkipFrames(int frames)
        {
            base.SetSkipFrames(frames);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelay
        public new Echo SetDelay(float time)
        {
            base.SetDelay(time);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelayUntil
        public new Echo SetDelayUntil(Func<bool> callback)
        {
            base.SetDelayUntil(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetWaitFor
        public new Echo SetWaitFor(params AbstractAnimation[] animations)
        {
            base.SetWaitFor(animations);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetLoopCount
        public new Echo SetLoopCount(int? loopCount)
        {
            base.SetLoopCount(loopCount);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetTimeScale
        public new Echo SetTimeScale(float timeScale)
        {
            base.SetTimeScale(timeScale);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentEchoOptionable.SetTimeout
        public Echo SetTimeout(float? timeout)
        {
            if (timeout < EchoOptions.MinTime)
            {
                throw new ArgumentException(EchoOptions.ErrorTimeoutMin);
            }
            if (timeout != null && float.IsInfinity(timeout.Value))
            {
                throw new ArgumentException(EchoOptions.ErrorTimeoutInfinity);
            }
            this.timeout = timeout != null && float.IsInfinity(timeout.Value) ? null : timeout;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentEchoOptionable.SetStopCondition
        public Echo SetStopCondition(Func<float, bool> stopCondition)
        {
            this.stopCondition = stopCondition;
            return this;
        }
    }
}
