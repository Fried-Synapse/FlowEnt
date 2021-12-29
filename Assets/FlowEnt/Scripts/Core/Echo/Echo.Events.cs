using System;

namespace FriedSynapse.FlowEnt
{
    public partial class Echo : IFluentEchoEventable<Echo>
    {
        private Action onStarting;
        private Action<float> onUpdating;
        private Action onCompleting;

        /// <summary>
        /// Sets all the events for this echo.
        /// </summary>
        /// <param name="events"></param>
        public Echo SetEvents(EchoEvents events)
        {
            base.SetEvents(events);
            onStarting = events.OnStartingEvent;
            onUpdating = events.OnUpdatingEvent;
            onCompleting = events.OnCompletingEvent;
            return this;
        }

        /// <summary>
        /// Creates a builder for events and then sets all the events for this echo.
        /// </summary>
        /// <param name="eventsBuilder"></param>
        public Echo SetEvents(Func<EchoEvents, EchoEvents> eventsBuilder)
            => SetEvents(eventsBuilder(new EchoEvents()));

        /// <inheritdoc />
        /// \copydoc IFluentEchoEventable.OnStarting
        public Echo OnStarting(Action callback)
        {
            onStarting += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarted
        public new Echo OnStarted(Action callback)
        {
            base.OnStarted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentEchoEventable.OnUpdating
        public Echo OnUpdating(Action<float> callback)
        {
            onUpdating += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdated
        public new Echo OnUpdated(Action<float> callback)
        {
            base.OnUpdated(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopCompleted
        public new Echo OnLoopCompleted(Action<int?> callback)
        {
            base.OnLoopCompleted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentEchoEventable.OnCompleting
        public Echo OnCompleting(Action callback)
        {
            onCompleting += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleted
        public new Echo OnCompleted(Action callback)
        {
            base.OnCompleted(callback);
            return this;
        }
    }
}
