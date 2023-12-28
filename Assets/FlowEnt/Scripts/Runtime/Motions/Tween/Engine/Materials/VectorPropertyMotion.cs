using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the vector for the specified shader property.
    /// </summary>
    public class VectorPropertyMotion : AbstractVector4Motion<MaterialBuilderWithProperty<Vector4>>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new VectorPropertyMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new VectorPropertyMotion(item, From, to);
        }

        private VectorPropertyMotion(MaterialBuilderWithProperty<Vector4> item, Vector4 value)
            : base(item, value)
        {
        }

        private VectorPropertyMotion(MaterialBuilderWithProperty<Vector4> item, Vector4? from, Vector4 to)
            : base(item, from, to)
        {
        }

        public VectorPropertyMotion(Material item, int propertyId, Vector4 value) 
            : base(new MaterialBuilderWithProperty<Vector4>(item, propertyId), value)
        {
        }

        public VectorPropertyMotion(Material item, int propertyId, Vector4? from, Vector4 to) 
            : base(new MaterialBuilderWithProperty<Vector4>(item, propertyId), from, to)
        {
        }
        
        public VectorPropertyMotion(Material item, string propertyName, Vector4 value)
            : this(item, Shader.PropertyToID(propertyName), value)
        {
        }

        public VectorPropertyMotion(Material item, string propertyName, Vector4? from, Vector4 to)
            : this(item, Shader.PropertyToID(propertyName), from, to)
        {
        }

        protected override Vector4 GetFrom() => item.BuiltMaterial.GetVector(item.PropertyId);
        protected override void SetValue(Vector4 value) => item.BuiltMaterial.SetVector(item.PropertyId, value);
    }
}