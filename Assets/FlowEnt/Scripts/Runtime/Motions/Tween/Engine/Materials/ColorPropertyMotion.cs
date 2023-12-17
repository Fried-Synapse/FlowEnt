using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the color for the specified shader property.
    /// </summary>
    public class ColorPropertyMotion : AbstractColorMotion<DynamicMaterialWithProperty<Color>>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new ColorPropertyMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new ColorPropertyMotion(item, from, to);
        }
        
        private ColorPropertyMotion(DynamicMaterialWithProperty<Color> item, Color value)
            : base(item, value)
        {
        }

        private ColorPropertyMotion(DynamicMaterialWithProperty<Color> item, Color? from, Color to)
            : base(item, from, to)
        {
        }

        public ColorPropertyMotion(Material item, int propertyId, Color value)
            : this(new DynamicMaterialWithProperty<Color>(item, propertyId), value)
        {
        }

        public ColorPropertyMotion(Material item, int propertyId, Color? from, Color to)
            : this(new DynamicMaterialWithProperty<Color>(item, propertyId), from, to)
        {
        }

        public ColorPropertyMotion(Material item, string propertyName, Color value)
            : this(item, Shader.PropertyToID(propertyName), value)
        {
        }

        public ColorPropertyMotion(Material item, string propertyName, Color? from, Color to)
            : this(item, Shader.PropertyToID(propertyName), from, to)
        {
        }

        protected override Color GetFrom() => item.Material.GetColor(item.PropertyId);
        protected override void SetValue(Color value) => item.Material.SetColor(item.PropertyId, value);
    }
}