using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class TweenBuilder : AbstractAnimationBuilder<Tween>, IGizmoDrawer
    {
        [SerializeField]
        private TweenOptionsBuilder options = new();

        public TweenOptionsBuilder Options => options;

        [SerializeField]
        private TweenEventsBuilder events = new();

        public TweenEventsBuilder Events => events;

        [SerializeField]
        private TweenMotionsBuilder motions = new();

        public TweenMotionsBuilder Motions => motions;

        public override Tween Build()
            => new Tween(Options.Build())
#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
                .SetHierarchy<Tween>(hierarchy)
#endif
                .SetEvents(Events.Build())
                .Apply(Motions.Build());

#if UNITY_EDITOR
        void IGizmoDrawer.OnGizmosDrawing(GizmoOptions options)
        {
            foreach (AbstractTweenMotionBuilder motion in Motions.Items)
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