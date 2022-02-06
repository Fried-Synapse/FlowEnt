using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the alpha for the specified shader property.
    /// </summary>
    /// <typeparam name="TMaterial"></typeparam>
    public class AlphaPropertyIdMotion<TMaterial> : AbstractAlphaMotion<TMaterial>
        where TMaterial : Material
    {
        public AlphaPropertyIdMotion(TMaterial item, int propertyId, float value) : base(item, value)
        {
            this.propertyId = propertyId;
        }

        public AlphaPropertyIdMotion(TMaterial item, int propertyId, float? from, float to) : base(item, from, to)
        {
            this.propertyId = propertyId;
        }

        private readonly int propertyId;
        protected override float GetFrom() => item.GetColor(propertyId).a;
        protected override void SetValue(float value) => item.SetColor(propertyId, SetAlpha(item.GetColor(propertyId), value));
    }
}