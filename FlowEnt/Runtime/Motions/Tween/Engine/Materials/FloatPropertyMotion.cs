using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the value for the specified shader property.
    /// </summary>
    public class FloatPropertyMotion : AbstractFloatMotion<MaterialBuilderWithProperty<float>>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new FloatPropertyMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new FloatPropertyMotion(item, From, to);
        }

        private FloatPropertyMotion(MaterialBuilderWithProperty<float> item, float value)
            : base(item, value)
        {
        }

        private FloatPropertyMotion(MaterialBuilderWithProperty<float> item, float? from, float to)
            : base(item, from, to)
        {
        }

        public FloatPropertyMotion(Material item, int propertyId, float value)
            : this(new MaterialBuilderWithProperty<float>(item, propertyId), value)
        {
        }

        public FloatPropertyMotion(Material item, int propertyId, float? from, float to)
            : this(new MaterialBuilderWithProperty<float>(item, propertyId), from, to)
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

        protected override float GetFrom() => item.BuiltMaterial.GetFloat(item.PropertyId);
        protected override void SetValue(float value) => item.BuiltMaterial.SetFloat(item.PropertyId, value);
    }
}