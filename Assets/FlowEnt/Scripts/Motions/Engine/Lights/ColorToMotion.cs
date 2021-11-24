using UnityEngine;
using UnityEngine.UI;

namespace FriedSynapse.FlowEnt.Motions.Lights
{
    public class ColorToMotion : AbstractMotion<Light>
    {
        public ColorToMotion(Light item, Color to) : base(item)
        {
            this.to = to;
        }

        public ColorToMotion(Light item, Color from, Color to) : this(item, to)
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
                from = item.color;
            }
        }

        public override void OnUpdate(float t)
        {
            item.color = Color.LerpUnclamped(from, to, t);
        }
    }
}