using UnityEngine;

namespace FlowEnt.Motions.Renderers
{
    public class AlphaMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public AlphaMotion(TRenderer item, float value) : base(item)
        {
            this.value = value;
        }

        private readonly float value;
        private float from;
        private float to;
        private Color color;

        public override void OnStart()
        {
            from = item.material.color.a;
            to = from + value;
        }

        public override void OnUpdate(float t)
        {
            color = item.material.color;
            color.a = Mathf.Lerp(from, to, t);
            item.material.color = color;
        }
    }
}