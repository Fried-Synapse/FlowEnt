using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class EchoBuilder : AbstractAnimationBuilder<Echo>, IGizmoDrawer
    {
        [SerializeField]
        private EchoOptionsBuilder options = new();

        public EchoOptionsBuilder Options => options;

        [SerializeField]
        private EchoEventsBuilder events = new();

        public EchoEventsBuilder Events => events;

        [SerializeField]
        private EchoMotionsBuilder motions = new();

        public EchoMotionsBuilder Motions => motions;

        public override Echo Build()
            => new Echo(Options.Build())
#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
                .SetHierarchy<Echo>(hierarchy)
#endif
                .SetEvents(Events.Build())
                .Apply(Motions.Build());
        
#if UNITY_EDITOR
        void IGizmoDrawer.OnGizmosDrawing(GizmoOptions options)
        {
            foreach (AbstractEchoMotionBuilder motion in Motions.Items)
            {
                if (motion.IsEnabled && motion is IGizmoDrawer drawer)
                {
                    drawer.OnGizmosDrawing(options);
                }
            }
        }
#endif
    }
}