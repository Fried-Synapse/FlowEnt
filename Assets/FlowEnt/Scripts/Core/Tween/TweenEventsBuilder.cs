using System;
using UnityEngine;
using UnityEngine.Events;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class TweenEventsBuilder : AbstractAnimationEventsBuilder
    {
        [SerializeField]
        private UnityEvent onStarting;
        public UnityEvent OnStarting { get => onStarting; set => onStarting = value; }

        [SerializeField]
        private UnityEvent<float> onUpdating;
        public UnityEvent<float> OnUpdating { get => onUpdating; set => onUpdating = value; }

        [SerializeField]
        private UnityEvent<int?> onLoopCompleting;
        public UnityEvent<int?> OnLoopCompleting { get => onLoopCompleting; set => onLoopCompleting = value; }

        [SerializeField]
        private UnityEvent onCompleting;
        public UnityEvent OnCompleting { get => onCompleting; set => onCompleting = value; }

        public TweenEvents Build()
        {
            return new TweenEvents()
            {
                //TODO copy events
            };
        }
    }
}
