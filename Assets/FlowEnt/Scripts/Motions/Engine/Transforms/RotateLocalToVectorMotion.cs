using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public class RotateLocalToVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public RotateLocalToVectorMotion(TTransform item, Vector3 to) : base(item)
        {
            this.to = to;
        }

        public RotateLocalToVectorMotion(TTransform item, Vector3 from, Vector3 to) : this(item, to)
        {
            hasFrom = true;
            this.from = from;
        }

        private readonly bool hasFrom;
        private Vector3 from;
        private readonly Vector3 to;

        public override void OnStart()
        {
            if (!hasFrom)
            {
                from = item.eulerAngles;
            }
        }

        public override void OnUpdate(float t)
        {
            item.eulerAngles = Vector3.LerpUnclamped(from, to, t);
        }
    }
}