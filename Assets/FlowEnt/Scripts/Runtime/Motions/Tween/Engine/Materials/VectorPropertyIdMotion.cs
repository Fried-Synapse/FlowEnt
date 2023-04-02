using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the vector for the specified shader property.
    /// </summary>
    public class VectorPropertyIdMotion : AbstractVector4Motion<Material>
    {
        public VectorPropertyIdMotion(Material item, int propertyId, Vector4 value) : base(item, value)
        {
            this.propertyId = propertyId;
        }

        public VectorPropertyIdMotion(Material item, int propertyId, Vector4? from, Vector4 to) : base(item, from, to)
        {
            this.propertyId = propertyId;
        }

        private readonly int propertyId;
        protected override Vector4 GetFrom() => item.GetVector(propertyId);
        protected override void SetValue(Vector4 value) => item.SetVector(propertyId, value);
    }
}