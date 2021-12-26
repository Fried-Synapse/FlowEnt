using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Lights
{
    /// <summary>
    /// Lerps the <see cref="Light.color" /> value using a gradient.
    /// </summary>
    public class ColorGradientMotion : AbstractGradientMotion<Light>
    {
        public ColorGradientMotion(Light item, Gradient gradient) : base(item, gradient)
        {
        }

        public override void OnUpdate(float t)
        {
            item.color = gradient.Evaluate(t);
        }
    }
}