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
        private UnityEvent<int?> onLoopCompleted;
        public UnityEvent<int?> OnLoopCompleted => onLoopCompleted;

        [SerializeField]
        private UnityEvent onCompleted;
        public UnityEvent OnCompleted => onCompleted;
#pragma warning restore RCS1085, RCS1169, IDE0044

        public override TAnimationEvents Build()
        {
            TAnimationEvents events = new TAnimationEvents();
            if (onStarted?.GetPersistentEventCount() > 0)
            {
                events.OnStarted(() => onStarted.Invoke());
            }
            if (onUpdated?.GetPersistentEventCount() > 0)
            {
                events.OnUpdated((t) => onUpdated.Invoke(t));
            }
            if (onLoopCompleted?.GetPersistentEventCount() > 0)
            {
                events.OnLoopCompleted((t) => onLoopCompleted.Invoke(t));
            }
            if (onCompleted?.GetPersistentEventCount() > 0)
            {
                events.OnCompleted(() => onCompleted.Invoke());
            }
            return events;
        }
    }
}
