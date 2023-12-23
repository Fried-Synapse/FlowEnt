using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the value for the specified shader property.
    /// </summary>
    public class FloatPropertyMotion : AbstractFloatMotion<DynamicMaterialWithProperty<float>>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new FloatPropertyMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new FloatPropertyMotion(item, From, to);
        }

        private FloatPropertyMotion(DynamicMaterialWithProperty<float> item, float value)
            : base(item, value)
        {
        }

        private FloatPropertyMotion(DynamicMaterialWithProperty<float> item, float? from, float to)
            : base(item, from, to)
        {
        }

        public FloatPropertyMotion(Material item, int propertyId, float value)
            : this(new DynamicMaterialWithProperty<float>(item, propertyId), value)
        {
        }

        public FloatPropertyMotion(Material item, int propertyId, float? from, float to)
            : this(new DynamicMaterialWithProperty<float>(item, propertyId), from, to)
        {
        }

        public FloatPropertyMotion(Material item, string propertyName, float value)
            : this(item, Shader.PropertyToID(propertyName), value)
        {
        }

        public FloatPropertyMotion(Material item, string propertyName, float? from, float to)
            : this(item, Shader.PropertyToID(propertyName), from, to)
        {
        }

        protected override float GetFrom() => item.Material.GetFloat(item.PropertyId);
        protected override void SetValue(float value) => item.Material.SetFloat(item.PropertyId, value);
    }
}