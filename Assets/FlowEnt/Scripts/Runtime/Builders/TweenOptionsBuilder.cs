using System;
using FriedSynapse.FlowEnt.Easings;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class TweenOptionsBuilder : AbstractAnimationOptionsBuilder<TweenOptions>
    {
#pragma warning disable RCS1169, RCS1085, IDE0044
        [SerializeField]
        private float time = TweenOptions.DefaultTime;
        public float Time => time;
        [SerializeField]
        private Easing easing = TweenOptions.DefaultEasing;
        public Easing Easing => easing;
        [SerializeField]
        private LoopType loopType;
        public LoopType LoopType => loopType;

#pragma warning restore RCS1169, RCS1085, IDE0044

        public override TweenOptions Build()
        {
            TweenOptions options = base.Build();
            options.Time = time;
            options.Easing = EasingFactory.Create(easing);
            options.LoopType = loopType;
            return options;
        }
    }
}
