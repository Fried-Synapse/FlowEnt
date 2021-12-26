using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public class ScaleLocalAnimationCurve3dMotion<TTransform> : AbstractAnimationCurve3dMotion<TTransform>
        where TTransform : Transform
    {
        public ScaleLocalAnimationCurve3dMotion(TTransform item, AnimationCurve3d animationCurve) : base(item, animationCurve)
        {
        }

        public override void OnUpdate(float t)
        {
            item.localScale = animationCurve.Evaluate(t);
        }
    }
}
