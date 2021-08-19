using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public class RotateLocalToQuaternionMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public RotateLocalToQuaternionMotion(TTransform item, Quaternion to) : base(item)
        {
            this.to = to;
        }

        public RotateLocalToQuaternionMotion(TTransform item, Quaternion from, Quaternion to) : this(item, to)
        {
            hasFrom = true;
            this.from = from;
        }

        private readonly bool hasFrom;
        private Quaternion from;
        private readonly Quaternion to;

        public override void OnStart()
        {
            if (!hasFrom)
            {
                from = item.localRotation;
            }
        }

        public override void OnUpdate(float t)
        {
            item.localRotation = Quaternion.LerpUnclamped(from, to, t);
        }
    }
}