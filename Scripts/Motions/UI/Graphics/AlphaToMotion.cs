using UnityEngine;
using UnityEngine.UI;

namespace FriedSynapse.FlowEnt.Motions.UI.Graphics
{
    public class AlphaToMotion<TGraphic> : AbstractMotion<TGraphic>
         where TGraphic : Graphic
    {
        public AlphaToMotion(TGraphic item, float to) : base(item)
        {
            this.to = to;
        }

        public AlphaToMotion(TGraphic item, float from, float to) : this(item, to)
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
                from = item.color.a;
            }
        }

        public override void OnUpdate(float t)
        {
            color = item.color;
            color.a = Mathf.LerpUnclamped(from, to, t);
            item.color = color;
        }
    }
}