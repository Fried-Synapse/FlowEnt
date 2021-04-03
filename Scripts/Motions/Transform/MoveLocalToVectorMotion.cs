using UnityEngine;

namespace FlowEnt.Motions.TransformMotions
{
    public class MoveLocalToVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveLocalToVectorMotion(TTransform item, Vector3 to) : base(item)
        {
            To = to;
        }

        public Vector3? From { get; private set; }
        public Vector3 To { get; }

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
