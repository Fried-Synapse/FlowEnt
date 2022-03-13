using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.SpriteRenderers
{
    /// <summary>
    /// Lerps the <see cref="SpriteRenderer.color" /> value.
    /// </summary>
    public class ColorMotion : AbstractColorMotion<SpriteRenderer>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new ColorMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new ColorMotion(item, from, to);
        }

        public ColorMotion(SpriteRenderer item, Color value) : base(item, value)
        {
        }

        public ColorMotion(SpriteRenderer item, Color? from, Color to) : base(item, from, to)
        {
        }

        protected override Color GetFrom() => item.color;
        protected override void SetValue(Color value) => item.color = value;
    }
}