using UnityEngine;

namespace FlowEnt.Motions.RectTransformMotions
{
    public class MoveAnchoredPositionToVectorMotion : AbstractMotion<RectTransform>
    {
        public MoveAnchoredPositionToVectorMotion(RectTransform item, Vector2 to) : base(item)
        {
            To = to;
        }

        public MoveAnchoredPositionToVectorMotion(RectTransform item, Vector2 from, Vector2 to) : this(item, to)
        {
            From = from;
        }

        public Vector2? From { get; private set; }
        public Vector2 To { get; }

        public override void OnStart()
        {
            if (From == null)
            {
                From = Item.anchoredPosition;
            }
            else
            {
                Item.anchoredPosition = From.Value;
            }
        }

        public override void OnUpdate(float t)
        {
            Item.anchoredPosition = Vector2.LerpUnclamped(From.Value, To, t);
        }
    }
}
