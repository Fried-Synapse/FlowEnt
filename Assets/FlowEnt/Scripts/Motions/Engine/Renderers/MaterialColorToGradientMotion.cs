using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    public class MaterialColorToGradientMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public MaterialColorToGradientMotion(TRenderer item, string propertyName, Gradient gradient) : base(item)
        {
            this.propertyName = propertyName;
            this.gradient = gradient;
        }

        private readonly string propertyName;
        private readonly Gradient gradient;

        public override void OnUpdate(float t)
        {
            item.material.SetColor(propertyName, gradient.Evaluate(t));
        }
    }
}