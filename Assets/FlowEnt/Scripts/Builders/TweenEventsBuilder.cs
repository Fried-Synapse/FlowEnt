using System;
using UnityEngine;
using UnityEngine.Events;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class TweenEventsBuilder : AbstractBuilder<TweenEvents>
    {
#pragma warning disable RCS1085, RCS1169, IDE0044
        [SerializeField]
        private UnityEvent onStarting;
        public UnityEvent OnStarting => onStarting;

        [SerializeField]
        private UnityEvent onStarted;
        public UnityEvent OnStarted => onStarted;

        [SerializeField]
        private UnityEvent<float> onUpdating;
        public UnityEvent<float> OnUpdating => onUpdating;

        [SerializeField]
        private UnityEvent<float> onUpdated;
        public UnityEvent<float> OnUpdated => onUpdated;

        [SerializeField]
        private UnityEvent<int?> onLoopCompleted;
        public UnityEvent<int?> OnLoopCompleted => onLoopCompleted;

        [SerializeField]
        private UnityEvent onCompleting;
        public UnityEvent OnCompleting => onCompleting;

        [SerializeField]
        private UnityEvent onCompleted;
        public UnityEvent OnCompleted => onCompleted;

#pragma warning restore RCS1085, RCS1169, IDE0044

        public override TweenEvents Build()
        {
            TweenEvents events = new TweenEvents();
            if (onStarting?.GetPersistentEventCount() > 0)
            {
                events.OnStarting(() => onStarting.Invoke());
            }
            if (onStarted?.GetPersistentEventCount() > 0)
            {
                events.OnStarted(() => onStarted.Invoke());
            }
            if (onUpdating?.GetPersistentEventCount() > 0)
            {
                events.OnUpdating((t) => onUpdating.Invoke(t));
            }
            if (onUpdated?.GetPersistentEventCount() > 0)
            {
                events.OnUpdated((t) => onUpdated.Invoke(t));
            }
            if (onLoopCompleted?.GetPersistentEventCount() > 0)
            {
                events.OnLoopCompleted((t) => onLoopCompleted.Invoke(t));
            }
            if (onCompleting?.GetPersistentEventCount() > 0)
            {
                events.OnCompleting(() => onCompleting.Invoke());
            }
            if (onCompleted?.GetPersistentEventCount() > 0)
            {
                events.OnCompleted(() => onCompleted.Invoke());
            }
            return events;
        }
    }
}
