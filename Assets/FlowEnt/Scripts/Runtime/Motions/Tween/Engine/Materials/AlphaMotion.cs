using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the alpha for <see cref="Material.color" /> value.
    /// </summary>
    public class AlphaMotion : AbstractAlphaMotion<MaterialBuilder>
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

        private AlphaMotion(MaterialBuilder item, float value) : base(item, value)
        {
        }

        private AlphaMotion(MaterialBuilder item, float? from, float to) : base(item, from, to)
        {
        }

        public AlphaMotion(Material item, float value) : this(new MaterialBuilder(item), value)
        {
        }

        public AlphaMotion(Material item, float? from, float to) : this(new MaterialBuilder(item), from, to)
        {
        }

        protected override float GetFrom() => item.BuiltMaterial.color.a;
        protected override void SetValue(float value) => item.BuiltMaterial.color = SetAlpha(item.BuiltMaterial.color, value);
    }
}