using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    /// <summary>
    /// Lerps the <see cref="Material.color" /> for <see cref="Renderer.material" /> value using a gradient.
    /// </summary>
    public class ColorGradientMotion<TRenderer> : AbstractGradientMotion<TRenderer>
        where TRenderer : Renderer
    {
        public ColorGradientMotion(TRenderer item, Gradient gradient) : base(item, gradient)
        {
        }

        public override void OnUpdate(float t)
        {
            item.material.color = gradient.Evaluate(t);
        }
    }
}