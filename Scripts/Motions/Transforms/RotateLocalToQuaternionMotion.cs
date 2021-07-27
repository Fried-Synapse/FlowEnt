using UnityEngine;

namespace FlowEnt.Motions.Transforms
{
    public class RotateLocalToQuaternionMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public RotateLocalToQuaternionMotion(TTransform item, Quaternion to) : base(item)
        {
            To = to;
        }

        public RotateLocalToQuaternionMotion(TTransform item, Quaternion from, Quaternion to) : this(item, to)
        {
            From = from;
        }

        public Quaternion? From { get; private set; }
        public Quaternion To { get; }

        public override void OnStart()
        {
            if (From == null)
            {
                From = Item.localRotation;
            }
            else
            {
                Item.localRotation = From.Value;
            }
        }

        public override void OnUpdate(float t)
        {
            Item.localRotation = Quaternion.LerpUnclamped(From.Value, To, t);
        }
    }
}