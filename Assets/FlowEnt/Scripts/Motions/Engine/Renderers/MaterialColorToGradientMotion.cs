using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    public class MaterialColorToGradientMotion<TRenderer> : AbstractGradientMotion<TRenderer>
        where TRenderer : Renderer
    {
        public MaterialColorToGradientMotion(TRenderer item, string propertyName, Gradient gradient) : base(item, gradient)
        {
            this.propertyName = propertyName;
        }

        private readonly string propertyName;
        public override void OnUpdate(float t)
        {
            item.material.SetColor(propertyName, gradient.Evaluate(t));
        }
    }
}