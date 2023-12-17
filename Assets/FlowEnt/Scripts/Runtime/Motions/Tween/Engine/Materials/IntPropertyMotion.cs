using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the value for the specified shader property.
    /// </summary>
    public class IntPropertyMotion : AbstractIntMotion<DynamicMaterialWithProperty<int>>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new IntPropertyMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new IntPropertyMotion(item, from, to);
        }

        private IntPropertyMotion(DynamicMaterialWithProperty<int> item, int value)
            : base(item, value)
        {
        }

        private IntPropertyMotion(DynamicMaterialWithProperty<int> item, int? from, int to)
            : base(item, from, to)
        {
        }

        public IntPropertyMotion(Material item, string propertyName, int value)
            : this(item, Shader.PropertyToID(propertyName), value)
        {
        }

        public IntPropertyMotion(Material item, string propertyName, int? from, int to)
            : this(item, Shader.PropertyToID(propertyName), from, to)
        {
        }

        public IntPropertyMotion(Material item, int propertyId, int value)
            : base(new DynamicMaterialWithProperty<int>(item, propertyId), value)
        {
        }

        public IntPropertyMotion(Material item, int propertyId, int? from, int to)
            : base(new DynamicMaterialWithProperty<int>(item, propertyId), from, to)
        {
        }

        protected override int GetFrom() => item.Material.GetInt(item.PropertyId);
        protected override void SetValue(int value) => item.Material.SetInt(item.PropertyId, value);
    }
}