using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    public class AlphaToMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public AlphaToMotion(TRenderer item, float to) : base(item)
        {
            this.to = to;
        }

        public AlphaToMotion(TRenderer item, float from, float to) : this(item, to)
        {
            hasFrom = true;
            this.from = from;
        }

        private readonly bool hasFrom;
        private float from;
        private readonly float to;
        private Color color;

        public override void OnStart()
        {
            if (!hasFrom)
            {
                from = item.material.color.a;
            }
        }

        public override void OnUpdate(float t)
        {
            color = item.material.color;
            color.a = Mathf.LerpUnclamped(from, to, t);
            item.material.color = color;
        }
    }
}