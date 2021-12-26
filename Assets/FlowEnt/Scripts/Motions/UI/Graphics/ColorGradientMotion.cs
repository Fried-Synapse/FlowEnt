using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;
using UnityEngine.UI;

namespace FriedSynapse.FlowEnt.Motions.UI.Graphics
{
    /// <summary>
    /// Lerps the <see cref="Graphic.color" /> value using a gradient.
    /// </summary>
    /// <typeparam name="TGraphic"></typeparam>
    public class ColorGradientMotion<TGraphic> : AbstractGradientMotion<TGraphic>
        where TGraphic : Graphic
    {
        public ColorGradientMotion(TGraphic item, Gradient gradient) : base(item, gradient)
        {
        }

        public override void OnUpdate(float t)
        {
            item.color = gradient.Evaluate(t);
        }
    }
}