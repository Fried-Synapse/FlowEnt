using System;

namespace FriedSynapse.FlowEnt
{
    public partial class AbstractAnimation : IFluentAnimationEventable<AbstractAnimation>
    {
        private protected Action<int?> onLoopCompleted;
        private protected Action<int?> onLoopStarted;

        protected void SetEvents(AbstractAnimationEvents events)
        {
            onStarted = events.OnStartedEvent;
            onUpdated = events.OnUpdatedEvent;
            onLoopCompleted = events.OnLoopCompletedEvent;
            onCompleted = events.OnCompletedEvent;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarted
        public AbstractAnimation OnStarted(Action callback)
        {
            onStarted += callback;
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
        /// \copydoc IFluentAnimationEventable.OnCompleted
        public AbstractAnimation OnCompleted(Action callback)
        {
            onCompleted += callback;
            return this;
        }
    }
}
