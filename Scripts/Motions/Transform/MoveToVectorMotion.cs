using UnityEngine;

namespace FlowEnt.Motions.TransformMotions
{
    public class MoveToVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveToVectorMotion(TTransform item, Vector3 to) : base(item)
        {
            To = to;
        }

        public Vector3? From { get; private set; }
        public Vector3 To { get; }

        public override void OnStart()
        {
            From = Item.position;
        }

        public override void OnUpdate(float t)
        {
            Item.position = Vector3.LerpUnclamped(From.Value, To, t);
        }

        public override void OnComplete()
        {
        }
    }
}
