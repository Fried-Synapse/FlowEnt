using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;
using UnityEngine.UI;

namespace FriedSynapse.FlowEnt.Motions.Tween.UI.Graphics
{
    /// <summary>
    /// Lerps the <see cref="Graphic.color" /> value.
    /// </summary>
    /// <typeparam name="TGraphic"></typeparam>
    public class ColorMotion<TGraphic> : AbstractColorMotion<TGraphic>
        where TGraphic : Graphic
    {
        public ColorMotion(TGraphic item, Color value) : base(item, value)
        {
        }

        public ColorMotion(TGraphic item, Color? from, Color to) : base(item, from, to)
        {
        }

        protected override Color GetFrom() => item.color;
        protected override void SetValue(Color value) => item.color = value;
    }
}