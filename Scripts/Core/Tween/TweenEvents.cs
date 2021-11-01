using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class TweenEvents : AbstractAnimationEvents,
        IFluentTweenEventable<TweenEvents>
    {
        public Action OnStartingEvent { get; set; }
        public Action<float> OnUpdatingEvent { get; set; }
        public Action<int?> OnLoopCompletingEvent { get; set; }
        public Action OnCompletingEvent { get; set; }

        /// <inheritdoc />
        public TweenEvents OnStarting(Action callback)
        {
            OnStartingEvent = callback;
            return this;
        }

        /// <inheritdoc />
        public TweenEvents OnStarted(Action callback)
        {
            OnStartedEvent = callback;
            return this;
        }

        /// <inheritdoc />
        public TweenEvents OnUpdating(Action<float> callback)
        {
            OnUpdatingEvent = callback;
            return this;
        }

        /// <inheritdoc />
        public TweenEvents OnUpdated(Action<float> callback)
        {
            OnUpdatedEvent = callback;
            return this;
        }

        /// <inheritdoc />
        public TweenEvents OnLoopCompleted(Action<int?> callback)
        {
            OnLoopCompletedEvent = callback;
            return this;
        }

        /// <inheritdoc />
        public TweenEvents OnCompleting(Action callback)
        {
            OnCompletingEvent = callback;
            return this;
        }

        /// <inheritdoc />
        public TweenEvents OnCompleted(Action callback)
        {
            OnCompletedEvent = callback;
            return this;
        }
    }
}
