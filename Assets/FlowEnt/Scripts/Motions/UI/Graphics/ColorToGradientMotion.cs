using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;
using UnityEngine.UI;

namespace FriedSynapse.FlowEnt.Motions.UI.Graphics
{
    public class ColorToGradientMotion<TGraphic> : AbstractMotion<TGraphic>
        where TGraphic : Graphic
    {
        public ColorToGradientMotion(TGraphic item, Gradient gradient) : base(item)
        {
            this.gradient = gradient;
        }

        private readonly Gradient gradient;

        public override void OnUpdate(float t)
        {
            item.color = gradient.Evaluate(t);
        }

    }
}