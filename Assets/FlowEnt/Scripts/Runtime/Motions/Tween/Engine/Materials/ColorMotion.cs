using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the <see cref="Material.color" /> value.
    /// </summary>
    public class ColorMotion : AbstractColorMotion<MaterialBuilder>
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
                => new ColorMotion(item, From, to);
        }

        private ColorMotion(MaterialBuilder item, Color value) : base(item, value)
        {
        }

        private ColorMotion(MaterialBuilder item, Color? from, Color to) : base(item, from, to)
        {
        }

        public ColorMotion(Material item, Color value) : this(new MaterialBuilder(item), value)
        {
        }

        public ColorMotion(Material item, Color? from, Color to) : this(new MaterialBuilder(item), from, to)
        {
        }

        protected override Color GetFrom() => item.BuiltMaterial.color;
        protected override void SetValue(Color value) => item.BuiltMaterial.color = value;
    }
}