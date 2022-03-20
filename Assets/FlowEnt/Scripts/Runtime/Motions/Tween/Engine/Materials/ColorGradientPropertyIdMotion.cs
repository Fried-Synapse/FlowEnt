using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the alpha for the specified shader property using a gradient.
    /// </summary>
    public class ColorGradientPropertyIdMotion : AbstractColorGradientMotion<Material>
    {
        public ColorGradientPropertyIdMotion(Material item, int propertyId, Gradient gradient) : base(item, gradient)
        {
            this.propertyId = propertyId;
        }

        private readonly int propertyId;
        public override void OnUpdate(float t)
        {
            item.SetColor(propertyId, gradient.Evaluate(t));
        }
    }
}