using System;
using UnityEngine;
using UnityEngine.Events;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public abstract class AbstractAnimationEventsBuilder
    {
        [SerializeField]
        private UnityEvent onStarted;
        public UnityEvent OnStarted { get => onStarted; set => onStarted = value; }

        [SerializeField]
        private UnityEvent<float> onUpdated;
        public UnityEvent<float> OnUpdated { get => onUpdated; set => onUpdated = value; }

        [SerializeField]
        private UnityEvent<int?> onLoopCompleted;
        public UnityEvent<int?> OnLoopCompleted { get => onLoopCompleted; set => onLoopCompleted = value; }

        [SerializeField]
        private UnityEvent onCompleted;
        public UnityEvent OnCompleted { get => onCompleted; set => onCompleted = value; }
    }
}
