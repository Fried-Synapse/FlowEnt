using UnityEngine;

namespace FlowEnt.Motions.Transforms
{
    public class RotateLocalToVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public RotateLocalToVectorMotion(TTransform item, Vector3 to) : base(item)
        {
            To = to;
        }

        public RotateLocalToVectorMotion(TTransform item, Vector3 from, Vector3 to) : this(item, to)
        {
            From = from;
        }

        public Vector3? From { get; private set; }
        public Vector3 To { get; }

        public override void OnStart()
        {
            if (From == null)
            {
                From = Item.localRotation.eulerAngles;
            }
            else
            {
                Item.localRotation = Quaternion.Euler(From.Value);
            }
        }

        public override void OnUpdate(float t)
        {
            Item.localRotation = Quaternion.Euler(Vector3.LerpUnclamped(From.Value, To, t));
        }
    }
}