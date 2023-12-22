using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.SpriteRenderers
{
    /// <summary>
    /// Lerps the <see cref="SpriteRenderer.size" /> value.
    /// </summary>
    public class SizeMotion : AbstractVector2Motion<SpriteRenderer>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new SizeMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new SizeMotion(item, From, to);
        }

        public SizeMotion(SpriteRenderer item, Vector2 value) : base(item, value)
        {
        }

        public SizeMotion(SpriteRenderer item, Vector2? from, Vector2 to) : base(item, from, to)
        {
        }

        protected override Vector2 GetFrom() => item.size;
        protected override void SetValue(Vector2 value) => item.size = value;
    }
}