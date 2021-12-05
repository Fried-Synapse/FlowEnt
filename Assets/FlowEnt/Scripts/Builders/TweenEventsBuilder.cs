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
        public UnityEvent OnStarting { get => onStarting; set => onStarting = value; }

        [SerializeField]
        private UnityEvent onStarted;
        public UnityEvent OnStarted { get => onStarted; set => onStarted = value; }

        [SerializeField]
        private UnityEvent<float> onUpdating;
        public UnityEvent<float> OnUpdating { get => onUpdating; set => onUpdating = value; }

        [SerializeField]
        private UnityEvent<float> onUpdated;
        public UnityEvent<float> OnUpdated { get => onUpdated; set => onUpdated = value; }

        [SerializeField]
        private UnityEvent<int?> onLoopCompleted;
        public UnityEvent<int?> OnLoopCompleted { get => onLoopCompleted; set => onLoopCompleted = value; }

        [SerializeField]
        private UnityEvent onCompleting;
        public UnityEvent OnCompleting { get => onCompleting; set => onCompleting = value; }

        [SerializeField]
        private UnityEvent onCompleted;
        public UnityEvent OnCompleted { get => onCompleted; set => onCompleted = value; }

#pragma warning restore RCS1085, RCS1169, IDE0044

        public override TweenEvents Build()
        {
            TweenEvents events = new TweenEvents();
            if (OnStarting?.GetPersistentEventCount() > 0)
            {
                events.OnStarting(() => OnStarting.Invoke());
            }
            if (OnStarted?.GetPersistentEventCount() > 0)
            {
                events.OnStarted(() => OnStarted.Invoke());
            }
            if (OnUpdating?.GetPersistentEventCount() > 0)
            {
                events.OnUpdating((t) => OnUpdating.Invoke(t));
            }
            if (OnUpdated?.GetPersistentEventCount() > 0)
            {
                events.OnUpdated((t) => OnUpdated.Invoke(t));
            }
            if (OnLoopCompleted?.GetPersistentEventCount() > 0)
            {
                events.OnLoopCompleted((t) => OnLoopCompleted.Invoke(t));
            }
            if (OnCompleting?.GetPersistentEventCount() > 0)
            {
                events.OnCompleting(() => OnCompleting.Invoke());
            }
            if (OnCompleted?.GetPersistentEventCount() > 0)
            {
                events.OnCompleted(() => OnCompleted.Invoke());
            }
            return events;
        }
    }
}
