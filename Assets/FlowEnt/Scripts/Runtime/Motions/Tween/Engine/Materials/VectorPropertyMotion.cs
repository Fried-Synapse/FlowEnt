using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the vector for the specified shader property.
    /// </summary>
    public class VectorPropertyMotion : AbstractVector4Motion<Material>
    {
        public VectorPropertyMotion(Material item, string propertyName, Vector4 value)
            : this(item, Shader.PropertyToID(propertyName), value)
        {
        }

        public VectorPropertyMotion(Material item, string propertyName, Vector4? from, Vector4 to)
            : this(item, Shader.PropertyToID(propertyName), from, to)
        {
        }

        public VectorPropertyMotion(Material item, int propertyId, Vector4 value) : base(item, value)
        {
            this.propertyId = propertyId;
        }

        public VectorPropertyMotion(Material item, int propertyId, Vector4? from, Vector4 to) : base(item, from, to)
        {
            this.propertyId = propertyId;
        }

        private readonly int propertyId;
        protected override Vector4 GetFrom() => item.GetVector(propertyId);
        protected override void SetValue(Vector4 value) => item.SetVector(propertyId, value);
    }
}