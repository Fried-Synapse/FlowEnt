using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public class MoveToAnimationCurve3dMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveToAnimationCurve3dMotion(TTransform item, AnimationCurve3d to) : base(item)
        {
            this.to = to;
        }

        private readonly AnimationCurve3d to;
        private Vector3 cache = Vector3.zero;
        public override void OnUpdate(float t)
        {
            cache.x = to.x.Evaluate(t);
            cache.y = to.y.Evaluate(t);
            cache.z = to.z.Evaluate(t);
            item.position = cache;
        }
    }
}
