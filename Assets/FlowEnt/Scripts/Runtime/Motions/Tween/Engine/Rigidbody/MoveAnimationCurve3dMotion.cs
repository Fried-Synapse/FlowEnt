using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="Rigidbody.position" /> value using an animation curve 3d.
    /// </summary>
    /// <typeparam name="TRigidbody"></typeparam>
    public class MoveAnimationCurve3dMotion<TRigidbody> : AbstractAnimationCurve3dMotion<TRigidbody>
        where TRigidbody : Rigidbody
    {
        public MoveAnimationCurve3dMotion(TRigidbody item, AnimationCurve3d animationCurve) : base(item, animationCurve)
        {
        }

        public override void OnUpdate(float t)
        {
            item.position = animationCurve.Evaluate(t);
        }
    }
}
