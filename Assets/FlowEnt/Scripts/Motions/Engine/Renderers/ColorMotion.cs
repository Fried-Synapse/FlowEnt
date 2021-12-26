using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    /// <summary>
    /// Lerps the <see cref="Material.color" /> for <see cref="Renderer.material" /> value.
    /// </summary>
    /// <typeparam name="TRenderer"></typeparam>
    public class ColorMotion<TRenderer> : AbstractColorMotion<TRenderer>
        where TRenderer : Renderer
    {
        public ColorMotion(TRenderer item, Color value) : base(item, value)
        {
        }

        public ColorMotion(TRenderer item, Color? from, Color to) : base(item, from, to)
        {
        }

        protected override Color GetFrom() => item.material.color;
        protected override void SetValue(Color value) => item.material.color = value;
    }
}