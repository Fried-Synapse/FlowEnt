using UnityEngine;

namespace FlowEnt.Motions.Transforms
{
    public class MoveToVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveToVectorMotion(TTransform item, Vector3 to) : base(item)
        {
            To = to;
        }

        public MoveToVectorMotion(TTransform item, Vector3 from, Vector3 to) : this(item, to)
        {
            hasFrom = true;
            this.from = from;
        }

        private readonly bool hasFrom;
        private Vector3 from;
        private readonly Vector3 To;

        public override void OnStart()
        {
            if (!hasFrom)
            {
                from = item.position;
            }
        }

        public override void OnUpdate(float t)
        {
            item.position = Vector3.LerpUnclamped(from, To, t);
        }
    }
}
