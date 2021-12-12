using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides events for tweens.
    /// </summary>
    public class TweenEvents : AbstractAnimationEvents, IFluentTweenEventable<TweenEvents>
    {
        /// <summary>
        /// The event called when an animation is starting.
        /// </summary>
        public Action OnStartingEvent { get; set; }
        /// <summary>
        /// The event called when an animation is updating.
        /// </summary>
        public Action<float> OnUpdatingEvent { get; set; }
        /// <summary>
        /// The event called when an animation is completing.
        /// </summary>
        public Action OnCompletingEvent { get; set; }

        /// <inheritdoc />
        /// \copydoc IFluentTweenEventable.OnStarting
        public TweenEvents OnStarting(Action callback)
        {
            OnStartingEvent = callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarted
        public new TweenEvents OnStarted(Action callback)
        {
            OnStartedEvent = callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenEventable.OnUpdating
        public TweenEvents OnUpdating(Action<float> callback)
        {
            OnUpdatingEvent = callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdated
        public new TweenEvents OnUpdated(Action<float> callback)
        {
            OnUpdatedEvent = callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopCompleted
        public new TweenEvents OnLoopCompleted(Action<int?> callback)
        {
            OnLoopCompletedEvent = callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentTweenEventable.OnCompleting
        public TweenEvents OnCompleting(Action callback)
        {
            OnCompletingEvent = callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleted
        public new TweenEvents OnCompleted(Action callback)
        {
            OnCompletedEvent = callback;
            return this;
        }
    }
}
