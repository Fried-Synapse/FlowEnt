using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;
using UnityEngine.UI;

namespace FriedSynapse.FlowEnt.Motions.Tween.UI.Graphics
{
    /// <summary>
    /// Lerps the <see cref="Graphic.color" /> value using a gradient.
    /// </summary>
    public class ColorGradientMotion : AbstractColorGradientMotion<Graphic>
    {
        public ColorGradientMotion(Graphic item, Gradient gradient) : base(item, gradient)
        {
        }

        public override void OnUpdate(float t)
        {
            item.color = gradient.Evaluate(t);
        }
    }
}