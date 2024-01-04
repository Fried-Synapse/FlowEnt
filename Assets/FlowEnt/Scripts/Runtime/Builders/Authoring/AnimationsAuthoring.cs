using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class AnimationsAuthoring : AbstractAuthoring, IGizmoDrawer
    {
        [SerializeField]
        private AnimationListBuilder animationBuilders;

        public AnimationListBuilder AnimationBuilders => animationBuilders;

        public List<AbstractAnimation> Animations { get; set; }

        protected override void StartAnimations()
        {
            Animations = new List<AbstractAnimation>();

            void startAnimations()
            {
                Animations.AddRange(AnimationBuilders.Build().Start());
            }

            if (Delay > 0)
            {
                Animations.Add(new Tween(Delay).OnCompleted(startAnimations).Start());
            }
            else
            {
                startAnimations();
            }
        }

        protected override void StopAnimations()
        {
            Animations.Stop(TriggerOnCompleted);
        }

#if UNITY_EDITOR
        void IGizmoDrawer.OnGizmosDrawing(GizmoOptions options)
        {
            AnimationBuilders.Items.ForEach(item => ((IGizmoDrawer)item).OnGizmosDrawing(options));
        }
#endif
    }
}