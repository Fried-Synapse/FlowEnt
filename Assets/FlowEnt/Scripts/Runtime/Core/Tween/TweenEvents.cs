using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides events for tween.
    /// </summary>
    public class TweenEvents : AbstractAnimationEvents, IFluentTweenEventable<TweenEvents>
    {
        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarting
        public new TweenEvents OnStarting(Action callback)
        {
            OnStartingEvent += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarted
        public new TweenEvents OnStarted(Action callback)
        {
            base.OnStarted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdating
        public new TweenEvents OnUpdating(Action<float> callback)
        {
            OnUpdatingEvent += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdated
        public new TweenEvents OnUpdated(Action<float> callback)
        {
            base.OnUpdated(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopStarted
        public new TweenEvents OnLoopStarted(Action<int?> callback)
        {
            base.OnLoopStarted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopCompleted
        public new TweenEvents OnLoopCompleted(Action<int?> callback)
        {
            base.OnLoopCompleted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleting
        public new TweenEvents OnCompleting(Action callback)
        {
            OnCompletingEvent += callback;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleted
        public new TweenEvents OnCompleted(Action callback)
        {
            base.OnCompleted(callback);
            return this;
        }
    }
}
