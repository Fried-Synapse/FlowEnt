using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.CanvasGroups
{
    public class AlphaToMotion : AbstractMotion<CanvasGroup>
    {
        public AlphaToMotion(CanvasGroup item, float to) : base(item)
        {
            this.to = to;
        }

        public AlphaToMotion(CanvasGroup item, float from, float to) : this(item, to)
        {
            hasFrom = true;
            this.from = from;
        }

        private readonly bool hasFrom;
        private float from;
        private readonly float to;

        public override void OnStart()
        {
            if (!hasFrom)
            {
                from = item.alpha;
            }
        }

        public override void OnUpdate(float t)
        {
            item.alpha = Mathf.Lerp(from, to, t);
        }
    }
}