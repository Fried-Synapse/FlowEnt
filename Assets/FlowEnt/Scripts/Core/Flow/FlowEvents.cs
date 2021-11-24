using System;

namespace FriedSynapse.FlowEnt
{
    public class FlowEvents : AbstractAnimationEvents,
        IFluentFlowEventable<FlowEvents>
    {
        /// <inheritdoc />
        public FlowEvents OnStarted(Action callback)
        {
            OnStartedEvent = callback;
            return this;
        }

        /// <inheritdoc />
        public FlowEvents OnUpdated(Action<float> callback)
        {
            OnUpdatedEvent = callback;
            return this;
        }

        /// <inheritdoc />
        public FlowEvents OnLoopCompleted(Action<int?> callback)
        {
            OnLoopCompletedEvent = callback;
            return this;
        }

        /// <inheritdoc />
        public FlowEvents OnCompleted(Action callback)
        {
            OnCompletedEvent = callback;
            return this;
        }
    }
}
