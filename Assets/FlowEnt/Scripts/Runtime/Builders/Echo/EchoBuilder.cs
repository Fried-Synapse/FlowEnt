using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class EchoBuilder : AbstractAnimationBuilder<Echo>
    {
#pragma warning disable RCS1169, RCS1085, IDE0044
        [SerializeField]
        private EchoOptionsBuilder options = new();

        public EchoOptionsBuilder Options => options;

        [SerializeField]
        private EchoEventsBuilder events = new();

        public EchoEventsBuilder Events => events;

        [SerializeField]
        private EchoMotionsBuilder motions = new();

        public EchoMotionsBuilder Motions => motions;
#pragma warning restore RCS1169, RCS1085, IDE0044

        public override Echo Build()
            => new Echo(Options.Build())
                .SetEvents(Events.Build())
                .Apply(Motions.Build());
    }
}