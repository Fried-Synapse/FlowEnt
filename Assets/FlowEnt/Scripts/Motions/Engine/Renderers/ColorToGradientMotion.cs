using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    public class ColorToGradientMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public ColorToGradientMotion(TRenderer item, Gradient gradient) : base(item)
        {
            this.gradient = gradient;
        }

        private readonly Gradient gradient;

        public override void OnUpdate(float t)
        {
            item.material.color = gradient.Evaluate(t);
        }
    }
}