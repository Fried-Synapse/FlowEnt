using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the alpha for the specified shader property.
    /// </summary>
    public class AlphaPropertyMotion : AbstractAlphaMotion<MaterialWithProperty<Color>>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new AlphaPropertyMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new AlphaPropertyMotion(item, from, to);
        }

        private AlphaPropertyMotion(MaterialWithProperty<Color> item, float value) : base(item, value)
        {
        }

        private AlphaPropertyMotion(MaterialWithProperty<Color> item, float? from, float to) : base(item, from, to)
        {
        }

        public AlphaPropertyMotion(Material item, int propertyId, float value)
            : base(new MaterialWithProperty<Color>(item, propertyId), value)
        {
        }

        public AlphaPropertyMotion(Material item, int propertyId, float? from, float to)
            : base(new MaterialWithProperty<Color>(item, propertyId), from, to)
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

        protected override float GetFrom() => item.Material.GetColor(item.PropertyId).a;

        protected override void SetValue(float value)
            => item.Material.SetColor(item.PropertyId, SetAlpha(item.Material.GetColor(item.PropertyId), value));
    }
}