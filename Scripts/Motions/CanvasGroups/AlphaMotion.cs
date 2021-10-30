using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.CanvasGroups
{
    public class AlphaMotion : AbstractMotion<CanvasGroup>
    {
        public AlphaMotion(CanvasGroup item, float value) : base(item)
        {
            this.value = value;
        }

        private readonly float value;
        private float from;
        private float to;

        public override void OnStart()
        {
            from = item.alpha;
            to = from + value;
        }

        public override void OnUpdate(float t)
        {
            item.alpha = Mathf.LerpUnclamped(from, to, t);
        }
    }
}