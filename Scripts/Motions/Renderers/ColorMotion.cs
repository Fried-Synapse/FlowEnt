using UnityEngine;

namespace FlowEnt.Motions.Renderers
{
    public class ColorMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public ColorMotion(TRenderer item, Color value) : base(item)
        {
            this.value = value;
        }

        private readonly Color value;
        private Color from;
        private Color to;

        public override void OnStart()
        {
            from = item.material.color;
            to = from + value;
        }

        public override void OnUpdate(float t)
        {
            item.material.color = Color.Lerp(from, to, t);
        }
    }
}