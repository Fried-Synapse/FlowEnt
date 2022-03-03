using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the alpha for <see cref="Material.color" /> value.
    /// </summary>
    public class AlphaMotion : AbstractAlphaMotion<Material>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new AlphaMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new AlphaMotion(item, from, to);
        }

        public AlphaMotion(Material item, float value) : base(item, value)
        {
        }

        public AlphaMotion(Material item, float? from, float to) : base(item, from, to)
        {
        }

        protected override float GetFrom() => item.color.a;
        protected override void SetValue(float value) => item.color = SetAlpha(item.color, value);
    }
}