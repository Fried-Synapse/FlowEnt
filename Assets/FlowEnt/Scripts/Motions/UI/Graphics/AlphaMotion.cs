using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;
using UnityEngine.UI;

namespace FriedSynapse.FlowEnt.Motions.UI.Graphics
{
    public class AlphaMotion<TGraphic> : AbstractMotion<TGraphic>
        where TGraphic : Graphic
    {
        public AlphaMotion(TGraphic item, float value) : base(item)
        {
            this.value = value;
        }

        private readonly float value;
        private float from;
        private float to;
        private Color color;

        public override void OnStart()
        {
            from = item.color.a;
            to = from + value;
        }

        public override void OnUpdate(float t)
        {
            color = item.color;
            color.a = Mathf.LerpUnclamped(from, to, t);
            item.color = color;
        }
    }
}