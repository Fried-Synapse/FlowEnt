using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.RectTransforms
{
    public class MoveAnchoredPositionToVectorMotion : AbstractMotion<RectTransform>
    {
        public MoveAnchoredPositionToVectorMotion(RectTransform item, Vector2 to) : base(item)
        {
            this.to = to;
        }

        public MoveAnchoredPositionToVectorMotion(RectTransform item, Vector2 from, Vector2 to) : this(item, to)
        {
            hasFrom = true;
            this.from = from;
        }

        private readonly bool hasFrom;
        private Vector2 from;
        private readonly Vector2 to;

        public override void OnStart()
        {
            if (!hasFrom)
            {
                from = item.anchoredPosition;
            }
        }

        public override void OnUpdate(float t)
        {
            item.anchoredPosition = Vector2.LerpUnclamped(from, to, t);
        }
    }
}
