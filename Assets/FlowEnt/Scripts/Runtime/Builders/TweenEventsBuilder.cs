using System;
using UnityEngine;
using UnityEngine.Events;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class TweenEventsBuilder : AbstractAnimationEventsBuilder<TweenEvents>
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

        public override TweenEvents Build()
        {
            TweenEvents events = base.Build();
            if (OnStarting?.GetPersistentEventCount() > 0)
            {
                events.OnStarting(() => OnStarting.Invoke());
            }
            if (OnUpdating?.GetPersistentEventCount() > 0)
            {
                events.OnUpdating((t) => OnUpdating.Invoke(t));
            }
            if (OnCompleting?.GetPersistentEventCount() > 0)
            {
                events.OnCompleting(() => OnCompleting.Invoke());
            }
            return events;
        }
    }
}
