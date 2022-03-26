using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.TrailRenderers
{
    /// <summary>
    /// Lerps the <see cref="TrailRenderer.startColor" /> and <see cref="TrailRenderer.endColor" /> values.
    /// </summary>
    public class ColorLinearMotion : AbstractColorLinearMotion<TrailRenderer>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new ColorLinearMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new ColorLinearMotion(item, from, to);
        }

        public ColorLinearMotion(TrailRenderer item, LinearColor value) : base(item, value)
        {
        }

        public ColorLinearMotion(TrailRenderer item, LinearColor from, LinearColor to) : base(item, from, to)
        {
        }

        protected override LinearColor GetFrom() => new LinearColor(item.startColor, item.endColor);
        protected override void SetValue(LinearColor value)
        {
            item.startColor = value.Start;
            item.endColor = value.End;
        }
    }
}