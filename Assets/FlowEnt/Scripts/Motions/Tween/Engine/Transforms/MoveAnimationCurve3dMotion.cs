using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.position" /> value using an animation curve 3d.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class MoveAnimationCurve3dMotion<TTransform> : AbstractAnimationCurve3dMotion<TTransform>
        where TTransform : Transform
    {
        public MoveAnimationCurve3dMotion(TTransform item, AnimationCurve3d animationCurve) : base(item, animationCurve)
        {
        }

        public override void OnUpdate(float t)
        {
            item.position = animationCurve.Evaluate(t);
        }
    }
}
