using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.LineRenderers
{
    public class GradientMotion : AbstractGradientMotion<LineRenderer>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new GradientMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new GradientMotion(item, From, to);
        }

        public GradientMotion(LineRenderer item, Gradient value) : base(item, value)
        {
        }

        public GradientMotion(LineRenderer item, Gradient from, Gradient to) : base(item, from, to)
        {
        }

        protected override Gradient GetFrom() => item.colorGradient;
        protected override void SetValue(Gradient value) => item.colorGradient = value;
    }
}