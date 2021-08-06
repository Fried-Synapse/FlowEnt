using UnityEngine;
using UnityEngine.UI;

namespace FlowEnt.Motions.Graphics
{
    public class ColorMotion<TGraphic> : AbstractMotion<TGraphic>
        where TGraphic : Graphic
    {
        public ColorMotion(TGraphic item, Color value) : base(item)
        {
            this.value = value;
        }

        private readonly Color value;
        private Color from;
        private Color to;

        public override void OnStart()
        {
            from = item.color;
            to = from + value;
        }

        public override void OnUpdate(float t)
        {
            item.color = Color.Lerp(from, to, t);
        }
    }
}