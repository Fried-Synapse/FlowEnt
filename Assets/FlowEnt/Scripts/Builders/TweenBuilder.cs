using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class TweenBuilder : AbstractBuilder<Tween>
    {
        [SerializeField]
        private TweenOptionsBuilder options;
        [SerializeField]
        private TweenEventsBuilder events;

        public override Tween Build()
            => new Tween(options.Build()).SetEvents(events.Build());
    }
}
