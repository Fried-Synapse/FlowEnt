using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public class MoveToVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveToVectorMotion(TTransform item, Vector3 to) : base(item)
        {
            this.to = to;
        }

        public MoveToVectorMotion(TTransform item, Vector3 from, Vector3 to) : this(item, to)
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
                from = item.position;
            }
        }

        public override void OnUpdate(float t)
        {
            item.position = Vector3.LerpUnclamped(from, to, t);
        }
    }
}
