using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public class MoveLocalToAnimationCurve3dMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveLocalToAnimationCurve3dMotion(TTransform item, AnimationCurve3d to) : base(item)
        {
            this.to = to;
        }

        private readonly AnimationCurve3d to;
        public override void OnUpdate(float t)
        {
            item.localPosition = to.Evaluate(t);
        }
    }
}
