using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the alpha for the specified shader property.
    /// </summary>
    public class AlphaPropertyMotion : AbstractAlphaMotion<MaterialBuilderWithProperty<Color>>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new AlphaPropertyMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new AlphaPropertyMotion(item, From, to);
        }

        private AlphaPropertyMotion(MaterialBuilderWithProperty<Color> item, float value) : base(item, value)
        {
        }

        private AlphaPropertyMotion(MaterialBuilderWithProperty<Color> item, float? from, float to) : base(item, from, to)
        {
        }

        public AlphaPropertyMotion(Material item, int propertyId, float value)
            : this(new MaterialBuilderWithProperty<Color>(item, propertyId), value)
        {
        }

        public AlphaPropertyMotion(Material item, int propertyId, float? from, float to)
            : this(new MaterialBuilderWithProperty<Color>(item, propertyId), from, to)
        {
        }

        public AlphaPropertyMotion(Material item, string propertyName, float value)
            : this(item, Shader.PropertyToID(propertyName), value)
        {
        }

        public AlphaPropertyMotion(Material item, string propertyName, float? from, float to)
            : this(item, Shader.PropertyToID(propertyName), from, to)
        {
        }

        protected override float GetFrom() => item.BuiltMaterial.GetColor(item.PropertyId).a;

        protected override void SetValue(float value)
            => item.BuiltMaterial.SetColor(item.PropertyId, SetAlpha(item.BuiltMaterial.GetColor(item.PropertyId), value));
    }
}