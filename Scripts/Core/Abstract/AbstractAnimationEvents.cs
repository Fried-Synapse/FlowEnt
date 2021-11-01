using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public abstract class AbstractAnimationEvents
    {
        public Action OnStartedEvent { get; set; }
        public Action<float> OnUpdatedEvent { get; set; }
        public Action<int?> OnLoopCompletedEvent { get; set; }
        public Action OnCompletedEvent { get; set; }
    }
}
