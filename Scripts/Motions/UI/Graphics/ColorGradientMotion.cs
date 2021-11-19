using UnityEngine;
using UnityEngine.UI;

namespace FriedSynapse.FlowEnt.Motions.UI.Graphics
{
    public class ColorGradientMotion<TGraphic> : AbstractMotion<TGraphic>
        where TGraphic : Graphic
    {
        public ColorGradientMotion(TGraphic item, Gradient value) : base(item)
        {
            this.value = value;
        }

        private readonly Gradient value;

        public override void OnUpdate(float t)
        {
            item.color = value.Evaluate(t);
        }

    }
}