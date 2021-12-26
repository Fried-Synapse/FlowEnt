using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    public class ColorToMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public ColorToMotion(TRenderer item, Color to) : base(item)
        {
            this.to = to;
        }

        public ColorToMotion(TRenderer item, Color from, Color to) : this(item, to)
        {
            hasFrom = true;
            this.from = from;
        }

        private readonly bool hasFrom;
        private Color from;
        private readonly Color to;

        public override void OnStart()
        {
            if (!hasFrom)
            {
                from = item.material.color;
            }
        }

        public override void OnUpdate(float t)
        {
            item.material.color = Color.LerpUnclamped(from, to, t);
        }
    }
}