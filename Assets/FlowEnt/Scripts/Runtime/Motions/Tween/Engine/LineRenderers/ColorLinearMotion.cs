using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.LineRenderers
{
    /// <summary>
    /// Lerps the <see cref="LineRenderer.startColor" /> and <see cref="LineRenderer.endColor" /> values.
    /// </summary>
    public class ColorLinearMotion : AbstractColorLinearMotion<LineRenderer>
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
                => new ColorLinearMotion(item, From, to);
        }

        public ColorLinearMotion(LineRenderer item, LinearColor value) : base(item, value)
        {
        }

        public ColorLinearMotion(LineRenderer item, LinearColor? from, LinearColor to) : base(item, from, to)
        {
        }

        protected override LinearColor GetFrom() => new(item.startColor, item.endColor);

        protected override void SetValue(LinearColor value)
        {
            item.startColor = value.Start;
            item.endColor = value.End;
        }
    }
}