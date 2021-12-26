using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
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