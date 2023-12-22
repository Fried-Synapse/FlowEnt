using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the alpha for <see cref="Material.color" /> value.
    /// </summary>
    public class AlphaMotion : AbstractAlphaMotion<DynamicMaterial>
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
                => new AlphaMotion(item, From, to);
        }

        private AlphaMotion(DynamicMaterial item, float value) : base(item, value)
        {
        }

        private AlphaMotion(DynamicMaterial item, float? from, float to) : base(item, from, to)
        {
        }

        public AlphaMotion(Material item, float value) : this(new DynamicMaterial(item), value)
        {
        }

        public AlphaMotion(Material item, float? from, float to) : this(new DynamicMaterial(item), from, to)
        {
        }

        protected override float GetFrom() => item.Material.color.a;
        protected override void SetValue(float value) => item.Material.color = SetAlpha(item.Material.color, value);
    }
}