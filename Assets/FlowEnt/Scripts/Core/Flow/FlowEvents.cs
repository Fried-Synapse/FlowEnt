using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides events for flows.
    /// </summary>
    public class FlowEvents : AbstractAnimationEvents, IFluentFlowEventable<FlowEvents>
    {
        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarted
        public new FlowEvents OnStarted(Action callback)
        {
            base.OnStarted(callback);
            OnStartedEvent = callback;
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
        /// \copydoc IFluentAnimationEventable.OnLoopCompleted
        public new FlowEvents OnLoopCompleted(Action<int?> callback)
        {
            base.OnLoopCompleted(callback);
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
