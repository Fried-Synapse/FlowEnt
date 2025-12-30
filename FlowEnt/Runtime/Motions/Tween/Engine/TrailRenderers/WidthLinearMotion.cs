using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.TrailRenderers
{
    /// <summary>
    /// Lerps the <see cref="TrailRenderer.startWidth" /> and <see cref="TrailRenderer.endWidth" /> values.
    /// </summary>
    public class WidthLinearMotion : AbstractFloatLinearMotion<TrailRenderer>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new WidthLinearMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new WidthLinearMotion(item, From, to);
        }

        public WidthLinearMotion(TrailRenderer item, LinearFloat value) : base(item, value)
        {
        }

        public WidthLinearMotion(TrailRenderer item, LinearFloat? from, LinearFloat to) : base(item, from, to)
        {
        }

        protected override LinearFloat GetFrom() => new(item.startWidth, item.endWidth);

        protected override void SetValue(LinearFloat value)
        {
            item.startWidth = value.Start;
            item.endWidth = value.End;
        }
    }
}