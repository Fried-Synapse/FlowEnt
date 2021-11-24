using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Lights
{
    public class ColorToGradientMotion : AbstractMotion<Light>
    {
        public ColorToGradientMotion(Light item, Gradient gradient) : base(item)
        {
            this.gradient = gradient;
        }

        private readonly Gradient gradient;

        public override void OnUpdate(float t)
        {
            item.color = gradient.Evaluate(t);
        }
    }
}