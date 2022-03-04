using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localPosition" /> value using an animation curve 3d.
    /// </summary>
    public class MoveLocalAnimationCurve3dMotion : AbstractAnimationCurve3dMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractAnimationCurve3dBuilder
        {
            public override ITweenMotion Build()
                => new MoveLocalAnimationCurve3dMotion(item, animationCurve);
        }

        public MoveLocalAnimationCurve3dMotion(Transform item, AnimationCurve3d animationCurve) : base(item, animationCurve)
        {
        }

        public override void OnUpdate(float t)
        {
            item.localPosition = animationCurve.Evaluate(t);
        }
    }
}
