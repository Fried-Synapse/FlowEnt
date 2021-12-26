using FriedSynapse.FlowEnt.Motions.Abstract;
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
        public override void OnUpdate(float t)
        {
            item.position = to.Evaluate(t);
        }
    }
}
