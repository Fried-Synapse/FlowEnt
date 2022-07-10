using System;

namespace FriedSynapse.FlowEnt
{
    public partial class Echo : IFluentEchoEventable<Echo>
    {
        /// <summary>
        /// Sets all the events for this echo.
        /// </summary>
        /// <param name="events"></param>
        public Echo SetEvents(EchoEvents events)
        {
            base.SetEvents(events);
            return this;
        }

        /// <summary>
        /// Creates a builder for events and then sets all the events for this echo.
        /// </summary>
        /// <param name="eventsBuilder"></param>
        public Echo SetEvents(Func<EchoEvents, EchoEvents> eventsBuilder)
            => SetEvents(eventsBuilder(new EchoEvents()));

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarting
        public new Echo OnStarting(Action callback)
        {
            base.OnStarting(callback);
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
        /// \copydoc IFluentAnimationEventable.OnUpdating
        public new Echo OnUpdating(Action<float> callback)
        {
            base.OnUpdating(callback);
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
        /// \copydoc IFluentAnimationEventable.OnLoopStarted
        public new Echo OnLoopStarted(Action<int?> callback)
        {
            base.OnLoopStarted(callback);
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
        /// \copydoc IFluentAnimationEventable.OnCompleting
        public new Echo OnCompleting(Action callback)
        {
            base.OnCompleting(callback);
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
