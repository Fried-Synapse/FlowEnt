using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class TweenBuilder : AbstractBuilder<Tween>
    {
#pragma warning disable RCS1169, IDE0044
        [SerializeField]
        private TweenOptionsBuilder options;
        [SerializeField]
        private TweenEventsBuilder events;
#pragma warning restore RCS1169, IDE0044

        public override Tween Build()
            => new Tween(options.Build()).SetEvents(events.Build());
    }
}
