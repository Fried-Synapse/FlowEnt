using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the vector for the specified shader property.
    /// </summary>
    /// <typeparam name="TMaterial"></typeparam>
    public class VectorPropertyIdMotion<TMaterial> : AbstractVector4Motion<TMaterial>
        where TMaterial : Material
    {
        public VectorPropertyIdMotion(TMaterial item, int propertyId, Vector4 value) : base(item, value)
        {
            this.propertyId = propertyId;
        }

        public VectorPropertyIdMotion(TMaterial item, int propertyId, Vector4? from, Vector4 to) : base(item, from, to)
        {
            this.propertyId = propertyId;
        }

        private readonly int propertyId;
        protected override Vector4 GetFrom() => item.GetVector(propertyId);
        protected override void SetValue(Vector4 value) => item.SetVector(propertyId, value);
    }
}