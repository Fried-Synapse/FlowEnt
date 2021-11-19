using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    public class ColorGradientMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public ColorGradientMotion(TRenderer item, Gradient value) : base(item)
        {
            this.value = value;
        }

        private readonly Gradient value;

        public override void OnUpdate(float t)
        {
            item.material.color = value.Evaluate(t);
        }
    }
}