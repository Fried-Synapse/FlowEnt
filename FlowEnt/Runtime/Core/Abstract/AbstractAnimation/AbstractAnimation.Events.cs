using System;

namespace FriedSynapse.FlowEnt
{
    public partial class AbstractAnimation : IFluentAnimationEventable<AbstractAnimation>
    {
        private protected Action<int?> onLoopCompleted;
        private protected Action<int?> onLoopStarted;

        protected void SetEvents(AbstractAnimationEvents events)
        {
            onStarting = events.OnStartingEvent;
            onStarted = events.OnStartedEvent;
            onUpdating = events.OnUpdatingEvent;
            onUpdated = events.OnUpdatedEvent;
            onLoopStarted = events.OnLoopStartedEvent;
            onLoopCompleted = events.OnLoopCompletedEvent;
            onCompleting = events.OnCompletingEvent;
            onCompleted = events.OnCompletedEvent;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarting
        public AbstractAnimation OnStarting(Action callback)
        {
            onStarting += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarted
        public AbstractAnimation OnStarted(Action callback)
        {
            onStarted += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdating
        public AbstractAnimation OnUpdating(Action<float> callback)
        {
            onUpdating += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdated
        public AbstractAnimation OnUpdated(Action<float> callback)
        {
            onUpdated += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopCompleted
        public AbstractAnimation OnLoopStarted(Action<int?> callback)
        {
            onLoopStarted += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopCompleted
        public AbstractAnimation OnLoopCompleted(Action<int?> callback)
        {
            onLoopCompleted += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleting
        public AbstractAnimation OnCompleting(Action callback)
        {
            onCompleting += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleted
        public AbstractAnimation OnCompleted(Action callback)
        {
            onCompleted += callback;
            return this;
        }
    }
}
