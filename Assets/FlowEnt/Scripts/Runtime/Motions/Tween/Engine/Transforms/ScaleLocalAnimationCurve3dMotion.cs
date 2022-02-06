using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localScale" /> value using an animation curve 3d.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
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
