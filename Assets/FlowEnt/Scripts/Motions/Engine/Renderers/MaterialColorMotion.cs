using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    /// <summary>
    /// Lerps the color for the specified shader property.
    /// </summary>
    public class MaterialColorMotion<TRenderer> : AbstractColorMotion<TRenderer>
        where TRenderer : Renderer
    {
        public MaterialColorMotion(TRenderer item, string propertyName, Color value) : base(item, value)
        {
            this.propertyName = propertyName;
        }

        public MaterialColorMotion(TRenderer item, string propertyName, Color? from, Color to) : base(item, from, to)
        {
            this.propertyName = propertyName;
        }

        private readonly string propertyName;
        protected override Color GetFrom() => item.material.GetColor(propertyName);
        protected override void SetValue(Color value) => item.material.SetColor(propertyName, value);
    }
}