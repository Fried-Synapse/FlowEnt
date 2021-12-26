using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public class ScaleLocalToAnimationCurve3dMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public ScaleLocalToAnimationCurve3dMotion(TTransform item, AnimationCurve3d to) : base(item)
        {
            this.to = to;
        }

        private readonly AnimationCurve3d to;
        public override void OnUpdate(float t)
        {
            item.localScale = to.Evaluate(t);
        }
    }
}
