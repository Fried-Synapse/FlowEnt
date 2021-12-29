using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Renderers
{
    /// <summary>
    /// Lerps the alpha for the specified shader property using a gradient.
    /// </summary>
    /// <typeparam name="TRenderer"></typeparam>
    public class MaterialColorGradientMotion<TRenderer> : AbstractGradientMotion<TRenderer>
        where TRenderer : Renderer
    {
        public MaterialColorGradientMotion(TRenderer item, string propertyName, Gradient gradient) : base(item, gradient)
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