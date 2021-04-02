using UnityEngine;

namespace FlowEnt
{
    public class MoveZMotion : AbstractMotion<Transform>
    {
        public MoveZMotion(Transform item, float z) : base(item)
        {
            Z = z;
        }

        public float Z { get; }
        public Vector3? To { get; private set; }
        public Vector3? From { get; private set; }

        public override void OnStart()
        {
            From = Item.position;
            To = From + new Vector3(0, 0, Z);
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