using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.RectTransforms
{
    public class MoveAnchoredPositionVectorMotion : AbstractMotion<RectTransform>
    {
        public MoveAnchoredPositionVectorMotion(RectTransform item, Vector2 value) : base(item)
        {
            this.value = value;
        }

        private readonly Vector2 value;
        private Vector2 from;
        private Vector2 to;

        public override void OnStart()
        {
            from = item.anchoredPosition;
            to = from + value;
        }

        public override void OnUpdate(float t)
        {
            item.anchoredPosition = Vector2.LerpUnclamped(from, to, t);
        }
    }
}