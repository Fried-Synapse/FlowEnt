using UnityEngine;

namespace FlowEnt
{
    public class MoveXMotion : AbstractMotion<Transform>
    {
        public MoveXMotion(Transform item, float x) : base(item)
        {
            X = x;
        }

        public float X { get; }
        public Vector3? To { get; private set; }
        public Vector3? From { get; private set; }

        public override void OnStart()
        {
            From = Item.position;
            To = From + new Vector3(X, 0, 0);
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