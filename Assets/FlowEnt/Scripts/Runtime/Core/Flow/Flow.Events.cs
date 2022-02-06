using System;

namespace FriedSynapse.FlowEnt
{
    public partial class Flow : IFluentFlowEventable<Flow>
    {
        /// <summary>
        /// Sets all the events for this flow.
        /// </summary>
        /// <param name="events"></param>
        public Flow SetEvents(FlowEvents events)
        {
            base.SetEvents(events);
            return this;
        }

        /// <summary>
        /// Creates a builder for events and then sets all the events for this flow.
        /// </summary>
        /// <param name="eventsBuilder"></param>
        public Flow SetEvents(Func<FlowEvents, FlowEvents> eventsBuilder)
            => SetEvents(eventsBuilder(new FlowEvents()));

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarted
        public new Flow OnStarted(Action callback)
        {
            base.OnStarted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdated
        public new Flow OnUpdated(Action<float> callback)
        {
            base.OnUpdated(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopCompleted
        public new Flow OnLoopCompleted(Action<int?> callback)
        {
            base.OnLoopCompleted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleted
        public new Flow OnCompleted(Action callback)
        {
            base.OnCompleted(callback);
            return this;
        }
    }
}