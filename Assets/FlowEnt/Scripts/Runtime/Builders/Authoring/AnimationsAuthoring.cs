using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace FriedSynapse.FlowEnt
{
    public class AnimationsAuthoring : AbstractAuthoring
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
    }
}