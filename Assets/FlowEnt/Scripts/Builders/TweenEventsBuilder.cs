using System;
using UnityEngine;
using UnityEngine.Events;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class TweenEventsBuilder : AbstractBuilder<TweenEvents>
    {
        [SerializeField]
        public UnityEvent onStarting;
        [SerializeField]
        public UnityEvent onStarted;
        [SerializeField]
        public UnityEvent<float> onUpdating;
        [SerializeField]
        public UnityEvent<float> onUpdate;
        [SerializeField]
        public UnityEvent<int?> onLoopCompleting;
        [SerializeField]
        public UnityEvent<int?> onLoopCompleted;
        [SerializeField]
        public UnityEvent onCompleting;
        [SerializeField]
        public UnityEvent onCompleted;

        public override TweenEvents Build()
            => new TweenEvents()
            {
                OnStartingEvent = () => onStarting?.Invoke(),
                OnStartedEvent = () => onStarted?.Invoke(),
                OnUpdatingEvent = (t) => onUpdating?.Invoke(t),
                OnUpdatedEvent = (t) => onUpdate?.Invoke(t),
                OnLoopCompletingEvent = (t) => onLoopCompleting?.Invoke(t),
                OnLoopCompletedEvent = (t) => onLoopCompleted?.Invoke(t),
                OnCompletingEvent = () => onCompleting?.Invoke(),
                OnCompletedEvent = () => onCompleted?.Invoke(),
            };
    }
}
