using UnityEngine;

namespace FlowEnt
{
    public class MoveLocalToMotion : AbstractMotion<Transform>
    {
        public MoveLocalToMotion(Transform item, Vector3 to) : base(item)
        {
            To = to;
        }

        public Vector3 To { get; }
        public Vector3? From { get; private set; }

        public override void OnStart()
        {
            From = Item.localPosition;
        }

        public override void OnUpdate(float t)
        {
            Item.localPosition = Vector3.LerpUnclamped(From.Value, To, t);
        }

        public override void OnComplete()
        {
        }
    }
}
