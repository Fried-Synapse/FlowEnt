using UnityEngine;

namespace FlowEnt.Motions.TransformMotions
{
    public class RotateLocalToQuaternionMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public RotateLocalToQuaternionMotion(TTransform item, Quaternion to) : base(item)
        {
            To = to;
        }

        public Quaternion? From { get; private set; }
        public Quaternion To { get; }

        public override void OnStart()
        {
            From = Item.localRotation;
        }

        public override void OnUpdate(float t)
        {
            Item.localRotation = Quaternion.LerpUnclamped(From.Value, To, t);
        }

        public override void OnComplete()
        {
        }
    }
}