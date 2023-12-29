using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the value for the specified shader property.
    /// </summary>
    public class IntPropertyMotion : AbstractIntMotion<MaterialBuilderWithProperty<int>>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new IntPropertyMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new IntPropertyMotion(item, From, to);
        }

        private IntPropertyMotion(MaterialBuilderWithProperty<int> item, int value)
            : base(item, value)
        {
        }

        private IntPropertyMotion(MaterialBuilderWithProperty<int> item, int? from, int to)
            : base(item, from, to)
        {
        }

        public IntPropertyMotion(Material item, int propertyId, int value)
            : this(new MaterialBuilderWithProperty<int>(item, propertyId), value)
        {
        }

        public IntPropertyMotion(Material item, int propertyId, int? from, int to)
            : this(new MaterialBuilderWithProperty<int>(item, propertyId), from, to)
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

        protected override int GetFrom() => item.BuiltMaterial.GetInt(item.PropertyId);
        protected override void SetValue(int value) => item.BuiltMaterial.SetInt(item.PropertyId, value);
    }
}