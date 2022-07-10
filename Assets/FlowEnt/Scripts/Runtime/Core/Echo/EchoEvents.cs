using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides events for echo.
    /// </summary>
    public class EchoEvents : AbstractAnimationEvents,
        IFluentEchoEventable<EchoEvents>
    {
        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarting
        public new EchoEvents OnStarting(Action callback)
        {
            base.OnStarting(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarted
        public new EchoEvents OnStarted(Action callback)
        {
            base.OnStarted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdating
        public new EchoEvents OnUpdating(Action<float> callback)
        {
            base.OnUpdating(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdated
        public new EchoEvents OnUpdated(Action<float> callback)
        {
            base.OnUpdated(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopStarted
        public new EchoEvents OnLoopStarted(Action<int?> callback)
        {
            base.OnLoopStarted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopCompleted
        public new EchoEvents OnLoopCompleted(Action<int?> callback)
        {
            base.OnLoopCompleted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleting
        public new EchoEvents OnCompleting(Action callback)
        {
            base.OnCompleting(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleted
        public new EchoEvents OnCompleted(Action callback)
        {
            base.OnCompleted(callback);
            return this;
        }
    }
}
