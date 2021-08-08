using UnityEngine;

namespace FlowEnt.Motions.Transforms
{
    public class MoveLocalToVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveLocalToVectorMotion(TTransform item, Vector3 to) : base(item)
        {
            this.to = to;
        }

        public MoveLocalToVectorMotion(TTransform item, Vector3 from, Vector3 to) : this(item, to)
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
                from = item.localPosition;
            }
        }

        public override void OnUpdate(float t)
        {
            item.localPosition = Vector3.LerpUnclamped(from, to, t);
        }
    }
}
