using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the value for the specified shader property.
    /// </summary>
    public class FloatPropertyMotion : AbstractFloatMotion<Material>
    {
        public FloatPropertyMotion(Material item, string propertyName, float value)
            : this(item, Shader.PropertyToID(propertyName), value)
        {
        }

        public FloatPropertyMotion(Material item, string propertyName, float? from, float to)
            : this(item, Shader.PropertyToID(propertyName), from, to)
        {
        }

        public FloatPropertyMotion(Material item, int propertyId, float value) : base(item, value)
        {
            this.propertyId = propertyId;
        }

        public FloatPropertyMotion(Material item, int propertyId, float? from, float to) : base(item, from, to)
        {
            this.propertyId = propertyId;
        }

        private readonly int propertyId;
        protected override float GetFrom() => item.GetFloat(propertyId);
        protected override void SetValue(float value) => item.SetFloat(propertyId, value);
    }
}