using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Cameras
{
    /// <summary>
    /// Lerps the <see cref="Camera.backgroundColor" /> value using a gradient.
    /// </summary>
    public class BackgroundColorGradientMotion : AbstractGradientMotion<Camera>
    {
        public BackgroundColorGradientMotion(Camera item, Gradient gradient) : base(item, gradient)
        {
        }

        public override void OnUpdate(float t)
        {
            item.backgroundColor = gradient.Evaluate(t);
        }
    }
}