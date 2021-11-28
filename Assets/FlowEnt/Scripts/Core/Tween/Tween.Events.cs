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
            CopyEvents(events);
            return this;
        }

        /// <summary>
        /// Creates a builder for events and then sets all the events for this tween.
        /// </summary>
        /// <param name="eventsBuilder"></param>
        public Tween SetEvents(Func<TweenEvents, TweenEvents> eventsBuilder)
        {
            CopyEvents(eventsBuilder(new TweenEvents()));
            return this;
        }

        /// <inheritdoc />
        public Tween OnStarting(Action callback)
        {
            onStarting += callback;
            return this;
        }

        /// <inheritdoc />
        public Tween OnStarted(Action callback)
        {
            onStarted += callback;
            return this;
        }

        /// <inheritdoc />
        public Tween OnUpdating(Action<float> callback)
        {
            onUpdating += callback;
            return this;
        }

        /// <inheritdoc />
        public Tween OnUpdated(Action<float> callback)
        {
            onUpdated += callback;
            return this;
        }

        /// <inheritdoc />
        public Tween OnLoopCompleted(Action<int?> callback)
        {
            onLoopCompleted += callback;
            return this;
        }

        /// <inheritdoc />
        public Tween OnCompleting(Action callback)
        {
            onCompleting += callback;
            return this;
        }

        /// <inheritdoc />
        public Tween OnCompleted(Action callback)
        {
            onCompleted += callback;
            return this;
        }

        private void CopyEvents(TweenEvents options)
        {
            onStarting = options.OnStartingEvent;
            onStarted = options.OnStartedEvent;
            onUpdating = options.OnUpdatingEvent;
            onUpdated = options.OnUpdatedEvent;
            onLoopCompleted = options.OnLoopCompletedEvent;
            onCompleting = options.OnCompletingEvent;
            onCompleted = options.OnCompletedEvent;
        }
    }
}
