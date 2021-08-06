using UnityEngine;

namespace FlowEnt.Motions.Transforms
{
    public class ScaleLocalToVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public ScaleLocalToVectorMotion(TTransform item, Vector3 to) : base(item)
        {
            this.to = to;
        }

        public ScaleLocalToVectorMotion(TTransform item, Vector3 from, Vector3 to) : this(item, to)
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
                from = item.localScale;
            }
        }

        public override void OnUpdate(float t)
        {
            item.localScale = Vector3.Lerp(from, to, t);
        }
    }
}