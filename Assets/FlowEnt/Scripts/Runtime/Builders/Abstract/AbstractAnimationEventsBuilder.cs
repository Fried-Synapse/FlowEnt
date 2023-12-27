using System;
using UnityEngine;
using UnityEngine.Events;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public abstract class AbstractAnimationEventsBuilder<TAnimationEvents> : AbstractBuilder<TAnimationEvents>
        where TAnimationEvents : AbstractAnimationEvents, new()
    {
#pragma warning disable RCS1085, RCS1169, IDE0044
        [SerializeField]
        private UnityEvent onStarted;
        public UnityEvent OnStarted => onStarted;

        [SerializeField]
        private UnityEvent<float> onUpdated;
        public UnityEvent<float> OnUpdated => onUpdated;

        [SerializeField]
        private UnityEvent<int?> onLoopStarted;
        public UnityEvent<int?> OnLoopStarted => onLoopStarted;

        [SerializeField]
        private UnityEvent<int?> onLoopCompleted;
        public UnityEvent<int?> OnLoopCompleted => onLoopCompleted;

        [SerializeField]
        private UnityEvent onCompleted;
        public UnityEvent OnCompleted => onCompleted;
#pragma warning restore RCS1085, RCS1169, IDE0044

        public override TAnimationEvents Build()
        {
            TAnimationEvents events = new TAnimationEvents();
            if (OnStarted?.GetPersistentEventCount() > 0)
            {
                events.OnStarted(() => OnStarted.Invoke());
            }
            if (OnUpdated?.GetPersistentEventCount() > 0)
            {
                events.OnUpdated((t) => OnUpdated.Invoke(t));
            }
            if (OnLoopStarted?.GetPersistentEventCount() > 0)
            {
                events.OnLoopStarted((t) => OnLoopStarted.Invoke(t));
            }
            if (OnLoopCompleted?.GetPersistentEventCount() > 0)
            {
                events.OnLoopCompleted((t) => OnLoopCompleted.Invoke(t));
            }
            if (OnCompleted?.GetPersistentEventCount() > 0)
            {
                events.OnCompleted(() => OnCompleted.Invoke());
            }
            return events;
        }
    }
}
