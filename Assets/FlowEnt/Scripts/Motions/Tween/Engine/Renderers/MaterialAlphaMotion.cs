using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Renderers
{
    /// <summary>
    /// Lerps the alpha for the specified shader property.
    /// </summary>
    /// <typeparam name="TRenderer"></typeparam>
    public class MaterialAlphaMotion<TRenderer> : AbstractAlphaMotion<TRenderer>
        where TRenderer : Renderer
    {
        public MaterialAlphaMotion(TRenderer item, string propertyName, float value) : base(item, value)
        {
            this.propertyName = propertyName;
        }

        public MaterialAlphaMotion(TRenderer item, string propertyName, float? from, float to) : base(item, from, to)
        {
            this.propertyName = propertyName;
        }

        private readonly string propertyName;
        protected override float GetFrom() => item.material.GetColor(propertyName).a;
        protected override void SetValue(float value) => item.material.SetColor(propertyName, SetAlpha(item.material.GetColor(propertyName), value));
    }
}