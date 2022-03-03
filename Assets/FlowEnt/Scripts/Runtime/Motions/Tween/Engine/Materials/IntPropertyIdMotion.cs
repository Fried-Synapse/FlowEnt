using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the value for the specified shader property.
    /// </summary>
    public class IntPropertyIdMotion : AbstractIntMotion<Material>
    {
        public IntPropertyIdMotion(Material item, int propertyId, int value) : base(item, value)
        {
            this.propertyId = propertyId;
        }

        public IntPropertyIdMotion(Material item, int propertyId, int? from, int to) : base(item, from, to)
        {
            this.propertyId = propertyId;
        }

        private readonly int propertyId;
        protected override int GetFrom() => item.GetInt(propertyId);
        protected override void SetValue(int value) => item.SetInt(propertyId, value);
    }
}