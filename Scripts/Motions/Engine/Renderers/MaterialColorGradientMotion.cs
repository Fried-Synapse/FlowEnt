using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    public class MaterialColorGradientMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public MaterialColorGradientMotion(TRenderer item, string propertyName, Gradient value) : base(item)
        {
            this.propertyName = propertyName;
            this.value = value;
        }

        private readonly string propertyName;
        private readonly Gradient value;

        public override void OnUpdate(float t)
        {
            item.material.SetColor(propertyName, value.Evaluate(t));
        }
    }
}