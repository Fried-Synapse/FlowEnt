using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Lights
{
    public class ColorGradientMotion : AbstractMotion<Light>
    {
        public ColorGradientMotion(Light item, Gradient value) : base(item)
        {
            this.value = value;
        }

        private readonly Gradient value;

        public override void OnUpdate(float t)
        {
            item.color = value.Evaluate(t);
        }
    }
}