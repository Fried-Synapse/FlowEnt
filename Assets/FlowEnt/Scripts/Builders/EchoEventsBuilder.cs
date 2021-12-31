using System;
using UnityEngine;
using UnityEngine.Events;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class EchoEventsBuilder : AbstractAnimationEventsBuilder<EchoEvents>
    {
#pragma warning disable RCS1085, RCS1169, IDE0044
        [SerializeField]
        private UnityEvent onStarting;
        public UnityEvent OnStarting => onStarting;

        [SerializeField]
        private UnityEvent<float> onUpdating;
        public UnityEvent<float> OnUpdating => onUpdating;

        [SerializeField]
        private UnityEvent onCompleting;
        public UnityEvent OnCompleting => onCompleting;
#pragma warning restore RCS1085, RCS1169, IDE0044

        public override EchoEvents Build()
        {
            EchoEvents events = base.Build();
            if (onStarting?.GetPersistentEventCount() > 0)
            {
                events.OnStarting(() => onStarting.Invoke());
            }
            if (onUpdating?.GetPersistentEventCount() > 0)
            {
                events.OnUpdating((t) => onUpdating.Invoke(t));
            }
            if (onCompleting?.GetPersistentEventCount() > 0)
            {
                events.OnCompleting(() => onCompleting.Invoke());
            }
            return events;
        }
    }
}
