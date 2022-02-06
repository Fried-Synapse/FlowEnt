using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the color for the specified shader property.
    /// </summary>
    /// <typeparam name="TMaterial"></typeparam>
    public class ColorPropertyIdMotion<TMaterial> : AbstractColorMotion<TMaterial>
        where TMaterial : Material
    {
        public ColorPropertyIdMotion(TMaterial item, int propertyId, Color value) : base(item, value)
        {
            this.propertyId = propertyId;
        }

        public ColorPropertyIdMotion(TMaterial item, int propertyId, Color? from, Color to) : base(item, from, to)
        {
            this.propertyId = propertyId;
        }

        private readonly int propertyId;
        protected override Color GetFrom() => item.GetColor(propertyId);
        protected override void SetValue(Color value) => item.SetColor(propertyId, value);
    }
}