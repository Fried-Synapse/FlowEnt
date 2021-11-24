using System;

namespace FriedSynapse.FlowEnt
{
    public partial class Flow
    {
        /// <summary>
        /// Sets all the events for this flow.
        /// </summary>
        /// <param name="events"></param>
        public Flow SetEvents(FlowEvents events)
        {
            CopyEvents(events);
            return this;
        }

        /// <summary>
        /// Creates a builder for events and then sets all the events for this flow.
        /// </summary>
        /// <param name="eventsBuilder"></param>
        public Flow SetEvents(Func<FlowEvents, FlowEvents> eventsBuilder)
        {
            CopyEvents(eventsBuilder(new FlowEvents()));
            return this;
        }

        /// <inheritdoc />
        public Flow OnStarted(Action callback)
        {
            onStarted += callback;
            return this;
        }

        /// <inheritdoc />
        public Flow OnUpdated(Action<float> callback)
        {
            onUpdated += callback;
            return this;
        }

        /// <inheritdoc />
        public Flow OnLoopCompleted(Action<int?> callback)
        {
            onLoopCompleted += callback;
            return this;
        }

        /// <inheritdoc />
        public Flow OnCompleted(Action callback)
        {
            onCompleted += callback;
            return this;
        }

        private void CopyEvents(FlowEvents options)
        {
            onStarted = options.OnStartedEvent;
            onUpdated = options.OnUpdatedEvent;
            onLoopCompleted = options.OnLoopCompletedEvent;
            onCompleted = options.OnCompletedEvent;
        }
    }
}
