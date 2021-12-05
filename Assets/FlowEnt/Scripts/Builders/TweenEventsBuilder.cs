using System;
using UnityEngine;
using UnityEngine.Events;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class TweenEventsBuilder : AbstractBuilder<TweenEvents>
    {
#pragma warning disable RCS1169, IDE0044
        [SerializeField]
        private UnityEvent onStarting;
        [SerializeField]
        private UnityEvent onStarted;
        [SerializeField]
        private UnityEvent<float> onUpdating;
        [SerializeField]
        private UnityEvent<float> onUpdated;
        [SerializeField]
        private UnityEvent<int?> onLoopCompleting;
        [SerializeField]
        private UnityEvent<int?> onLoopCompleted;
        [SerializeField]
        private UnityEvent onCompleting;
        [SerializeField]
        private UnityEvent onCompleted;
#pragma warning restore RCS1169, IDE0044

        public override TweenEvents Build()
        {
            TweenEvents events = new TweenEvents();
            if (onStarting?.GetPersistentEventCount() > 0)
            {
                events.OnStartingEvent = () => onStarting.Invoke();
            }
            if (onStarted?.GetPersistentEventCount() > 0)
            {
                events.OnStartedEvent = () => onStarted.Invoke();
            }
            if (onUpdating?.GetPersistentEventCount() > 0)
            {
                events.OnUpdatingEvent = (t) => onUpdating.Invoke(t);
            }
            if (onUpdated?.GetPersistentEventCount() > 0)
            {
                events.OnUpdatedEvent = (t) => onUpdated.Invoke(t);
            }
            if (onLoopCompleting?.GetPersistentEventCount() > 0)
            {
                events.OnLoopCompletingEvent = (t) => onLoopCompleting.Invoke(t);
            }
            if (onLoopCompleted?.GetPersistentEventCount() > 0)
            {
                events.OnLoopCompletedEvent = (t) => onLoopCompleted.Invoke(t);
            }
            if (onCompleting?.GetPersistentEventCount() > 0)
            {
                events.OnCompletingEvent = () => onCompleting.Invoke();
            }
            if (onCompleted?.GetPersistentEventCount() > 0)
            {
                events.OnCompletedEvent = () => onCompleted.Invoke();
            }
            return events;
        }
    }
}
