using System;
using FriedSynapse.FlowEnt.Easings;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class TweenOptionsBuilder : AbstractAnimationOptionsBuilder<TweenOptions>
    {
        public enum EasingType
        {
            Predefined,
            AnimationCurve,
        }

#pragma warning disable RCS1169, RCS1085, IDE0044
        [SerializeField, Min(TweenOptions.MinTime)]
        private float time = TweenOptions.DefaultTime;
        public float Time => time;
        [SerializeField]
        private EasingType easingType;
        [SerializeField]
        [UrlButton(UrlButtonAttribute.PredefinedType.Easing)]
        private Easing easing = TweenOptions.DefaultEasing;
        public IEasing Easing => easingType switch
        {
            EasingType.Predefined => EasingFactory.Create(easing),
            EasingType.AnimationCurve => new AnimationCurveEasing(easingCurve),
            _ => throw new NotImplementedException(),
        };
        [SerializeField]
        private AnimationCurve easingCurve;
        [SerializeField]
        private LoopType loopType;
        public LoopType LoopType => loopType;

#pragma warning restore RCS1169, RCS1085, IDE0044

        public override TweenOptions Build()
        {
            TweenOptions options = base.Build();
            options.Time = Time;
            options.Easing = Easing;
            options.LoopType = LoopType;
            return options;
        }
    }
}
