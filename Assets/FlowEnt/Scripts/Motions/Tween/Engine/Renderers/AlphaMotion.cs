using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Renderers
{
    /// <summary>
    /// Lerps the alpha for <see cref="Material.color" /> for <see cref="Renderer.material" /> value.
    /// </summary>
    /// <typeparam name="TRenderer"></typeparam>
    public class AlphaMotion<TRenderer> : AbstractAlphaMotion<TRenderer>
        where TRenderer : Renderer
    {
        public AlphaMotion(TRenderer item, float value) : base(item, value)
        {
        }

        public AlphaMotion(TRenderer item, float? from, float to) : base(item, from, to)
        {
        }

        protected override float GetFrom() => item.material.color.a;
        protected override void SetValue(float value) => item.material.color = SetAlpha(item.material.color, value);
    }
}