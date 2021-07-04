using UnityEngine;

namespace FlowEnt.Motions.RectTransformMotions
{
    public class MoveAnchoredPositionVectorMotion : AbstractMotion<RectTransform>
    {
        public MoveAnchoredPositionVectorMotion(RectTransform item, Vector2 value) : base(item)
        {
            Value = value;
        }

        public Vector2 Value { get; }
        public Vector2? From { get; private set; }
        public Vector2? To { get; private set; }

        public override void OnStart()
        {
            From = Item.anchoredPosition;
            To = From + Value;
        }

        public override void OnUpdate(float t)
        {
            Item.anchoredPosition = Vector2.LerpUnclamped(From.Value, To.Value, t);
        }

        public override void OnComplete()
        {
        }
    }
}