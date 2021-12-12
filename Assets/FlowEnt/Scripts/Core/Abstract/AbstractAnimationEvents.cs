using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides common events for animations.
    /// </summary>
    public abstract class AbstractAnimationEvents : IFluentAnimationEventable<AbstractAnimationEvents>
    {
        /// <summary>
        /// The event called when an animation has started.
        /// </summary>
        public Action OnStartedEvent { get; set; }
        /// <summary>
        /// The event called when an animation has updated.
        /// </summary>
        public Action<float> OnUpdatedEvent { get; set; }
        /// <summary>
        /// The event called when an animation loop has completed.
        /// </summary>
        public Action<int?> OnLoopCompletedEvent { get; set; }
        /// <summary>
        /// The event called when an animation has completed.
        /// </summary>
        public Action OnCompletedEvent { get; set; }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarted
        public AbstractAnimationEvents OnStarted(Action callback)
        {
            OnStartedEvent = callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdated
        public AbstractAnimationEvents OnUpdated(Action<float> callback)
        {
            OnUpdatedEvent = callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopCompleted
        public AbstractAnimationEvents OnLoopCompleted(Action<int?> callback)
        {
            OnLoopCompletedEvent = callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleted
        public AbstractAnimationEvents OnCompleted(Action callback)
        {
            OnCompletedEvent = callback;
            return this;
        }
    }
}
