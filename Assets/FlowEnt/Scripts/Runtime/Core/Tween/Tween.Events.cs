using System;

namespace FriedSynapse.FlowEnt
{
    public partial class Tween : IFluentTweenEventable<Tween>
    {
        private Action onStarting;
        private Action<float> onUpdating;
        private Action onCompleting;

        /// <summary>
        /// Sets all the events for this tween.
        /// </summary>
        /// <param name="events"></param>
        public Tween SetEvents(TweenEvents events)
        {
            base.SetEvents(events);
            onStarting = events.OnStartingEvent;
            onUpdating = events.OnUpdatingEvent;
            onCompleting = events.OnCompletingEvent;
            return this;
        }

        /// <summary>
        /// Creates a builder for events and then sets all the events for this tween.
        /// </summary>
        /// <param name="eventsBuilder"></param>
        public Tween SetEvents(Func<TweenEvents, TweenEvents> eventsBuilder)
            => SetEvents(eventsBuilder(new TweenEvents()));

        /// <inheritdoc />
        /// \copydoc IFluentTweenEventable.OnStarting
        public Tween OnStarting(Action callback)
        {
            onStarting += callback;
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
        /// \copydoc IFluentTweenEventable.OnUpdating
        public Tween OnUpdating(Action<float> callback)
        {
            onUpdating += callback;
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
        /// \copydoc IFluentTweenEventable.OnCompleting
        public Tween OnCompleting(Action callback)
        {
            onCompleting += callback;
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
