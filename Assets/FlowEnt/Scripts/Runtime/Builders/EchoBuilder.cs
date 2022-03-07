using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class EchoBuilder : AbstractAnimationBuilder<Echo>
    {
#pragma warning disable RCS1169, RCS1085, IDE0044
        [SerializeField]
        private EchoOptionsBuilder options;
        public EchoOptionsBuilder Options => options;

        [SerializeField]
        private EchoEventsBuilder events;
        public EchoEventsBuilder Events => events;
        [SerializeField]
        private EchoMotionsBuilder motions;
        public EchoMotionsBuilder Motions => motions;
#pragma warning restore RCS1169, RCS1085, IDE0044

        public override Echo Build()
            => SetAll(new Echo());

        public Echo Build(IUpdateController updateController = null)
            => SetAll(new Echo(updateController));

        private Echo SetAll(Echo echo)
            => echo.SetOptions(Options.Build())
                .SetEvents(Events.Build())
                .Apply(Motions.Build());
    }
}
