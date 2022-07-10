using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides common events for animations.
    /// </summary>
    public abstract class AbstractAnimationEvents : IFluentAnimationEventable<AbstractAnimationEvents>
    {
        /// <summary>
        /// The event called when the animation is starting.
        /// </summary>
        public Action OnStartingEvent { get; set; }
        /// <summary>
        /// The event called when the animation has started.
        /// </summary>
        public Action OnStartedEvent { get; set; }
        /// <summary>
        /// The event called when the animation is updating.
        /// </summary>
        public Action<float> OnUpdatingEvent { get; set; }
        /// <summary>
        /// The event called when the animation has updated.
        /// </summary>
        public Action<float> OnUpdatedEvent { get; set; }
        /// <summary>
        /// The event called when the animation loop has started.
        /// </summary>
        public Action<int?> OnLoopStartedEvent { get; set; }
        /// <summary>
        /// The event called when the animation loop has completed.
        /// </summary>
        public Action<int?> OnLoopCompletedEvent { get; set; }
        /// <summary>
        /// The event called when the animation is completing.
        /// </summary>
        public Action OnCompletingEvent { get; set; }
        /// <summary>
        /// The event called when the animation has completed.
        /// </summary>
        public Action OnCompletedEvent { get; set; }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarted
        public AbstractAnimationEvents OnStarted(Action callback)
        {
            OnStartedEvent += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarting
        public AbstractAnimationEvents OnStarting(Action callback)
        {
            OnStartingEvent += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdating
        public AbstractAnimationEvents OnUpdating(Action<float> callback)
        {
            OnUpdatingEvent += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdated
        public AbstractAnimationEvents OnUpdated(Action<float> callback)
        {
            OnUpdatedEvent += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopStarted
        public AbstractAnimationEvents OnLoopStarted(Action<int?> callback)
        {
            OnLoopStartedEvent += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopCompleted
        public AbstractAnimationEvents OnLoopCompleted(Action<int?> callback)
        {
            OnLoopCompletedEvent += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleting
        public AbstractAnimationEvents OnCompleting(Action callback)
        {
            OnCompletingEvent += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleted
        public AbstractAnimationEvents OnCompleted(Action callback)
        {
            OnCompletedEvent += callback;
            return this;
        }
    }
}
