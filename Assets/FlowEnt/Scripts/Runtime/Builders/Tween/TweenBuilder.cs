using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class TweenBuilder : AbstractAnimationBuilder<Tween>
    {
#pragma warning disable RCS1169, RCS1085, IDE0044
        [SerializeField]
        private TweenOptionsBuilder options = new();

        public TweenOptionsBuilder Options => options;

        [SerializeField]
        private TweenEventsBuilder events = new();

        public TweenEventsBuilder Events => events;

        [SerializeField]
        private TweenMotionsBuilder motions = new();

        public TweenMotionsBuilder Motions => motions;
#pragma warning restore RCS1169, RCS1085, IDE0044

        public override Tween Build()
            => new Tween(Options.Build())
                .SetEvents(Events.Build())
                .Apply(Motions.Build());
    }
}