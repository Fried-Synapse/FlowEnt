using UnityEngine;

namespace FlowEnt
{
    public class MoveYMotion : AbstractMotion<Transform>
    {
        public MoveYMotion(Transform item, float y) : base(item)
        {
            Y = y;
        }

        public float Y { get; }
        public Vector3? To { get; private set; }
        public Vector3? From { get; private set; }

        public override void OnStart()
        {
            From = Item.position;
            To = From + new Vector3(0, Y, 0);
        }

        public override void OnUpdate(float t)
        {
            Item.position = Vector3.LerpUnclamped(From.Value, To.Value, t);
        }

        public override void OnComplete()
        {
        }
    }
}