using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public class RotateToQuaternionMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public RotateToQuaternionMotion(TTransform item, Quaternion to) : base(item)
        {
            this.to = to;
        }

        public RotateToQuaternionMotion(TTransform item, Quaternion from, Quaternion to) : this(item, to)
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
                from = item.rotation;
            }
        }

        public override void OnUpdate(float t)
        {
            item.rotation = Quaternion.LerpUnclamped(from, to, t);
        }
    }
}