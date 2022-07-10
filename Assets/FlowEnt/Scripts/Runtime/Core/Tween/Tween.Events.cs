using System;

namespace FriedSynapse.FlowEnt
{
    public partial class Tween : IFluentTweenEventable<Tween>
    {
        /// <summary>
        /// Sets all the events for this tween.
        /// </summary>
        /// <param name="events"></param>
        public Tween SetEvents(TweenEvents events)
        {
            base.SetEvents(events);
            return this;
        }

        /// <summary>
        /// Creates a builder for events and then sets all the events for this tween.
        /// </summary>
        /// <param name="eventsBuilder"></param>
        public Tween SetEvents(Func<TweenEvents, TweenEvents> eventsBuilder)
            => SetEvents(eventsBuilder(new TweenEvents()));

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarting
        public new Tween OnStarting(Action callback)
        {
            base.OnStarting(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnStarted
        public new Tween OnStarted(Action callback)
        {
            base.OnStarted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdating
        public new Tween OnUpdating(Action<float> callback)
        {
            base.OnUpdating(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnUpdated
        public new Tween OnUpdated(Action<float> callback)
        {
            base.OnUpdated(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopStarted
        public new Tween OnLoopStarted(Action<int?> callback)
        {
            base.OnLoopStarted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnLoopCompleted
        public new Tween OnLoopCompleted(Action<int?> callback)
        {
            base.OnLoopCompleted(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleting
        public new Tween OnCompleting(Action callback)
        {
            base.OnCompleting(callback);
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationEventable.OnCompleted
        public new Tween OnCompleted(Action callback)
        {
            base.OnCompleted(callback);
            return this;
        }
    }
}
