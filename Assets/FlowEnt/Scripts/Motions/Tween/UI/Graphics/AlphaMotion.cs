using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine.UI;

namespace FriedSynapse.FlowEnt.Motions.Tween.UI.Graphics
{
    /// <summary>
    /// Lerps the alpha for <see cref="Graphic.color" /> value.
    /// </summary>
    /// <typeparam name="TGraphic"></typeparam>
    public class AlphaMotion<TGraphic> : AbstractAlphaMotion<TGraphic>
        where TGraphic : Graphic
    {
        public AlphaMotion(TGraphic item, float value) : base(item, value)
        {
        }

        public AlphaMotion(TGraphic item, float? from, float to) : base(item, from, to)
        {
        }

        protected override float GetFrom() => item.color.a;
        protected override void SetValue(float value) => item.color = SetAlpha(item.color, value);
    }
}