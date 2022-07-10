using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides events for flows.
    /// </summary>
    public class FlowEvents : AbstractAnimationEvents,
        IFluentFlowEventable<FlowEvents>
    {
        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarting
        public new FlowEvents OnStarting(Action callback)
        {
            base.OnStarting(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarted
        public new FlowEvents OnStarted(Action callback)
        {
            base.OnStarted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdating
        public new FlowEvents OnUpdating(Action<float> callback)
        {
            base.OnUpdating(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdated
        public new FlowEvents OnUpdated(Action<float> callback)
        {
            base.OnUpdated(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopStarted
        public new FlowEvents OnLoopStarted(Action<int?> callback)
        {
            base.OnLoopStarted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopCompleted
        public new FlowEvents OnLoopCompleted(Action<int?> callback)
        {
            base.OnLoopCompleted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleting
        public new FlowEvents OnCompleting(Action callback)
        {
            base.OnCompleting(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleted
        public new FlowEvents OnCompleted(Action callback)
        {
            base.OnCompleted(callback);
            return this;
        }
    }
}
