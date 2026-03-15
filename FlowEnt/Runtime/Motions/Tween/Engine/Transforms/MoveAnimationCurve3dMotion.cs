using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.position" /> value using an animation curve 3d.
    /// </summary>
    public class MoveAnimationCurve3dMotion : AbstractAnimationCurve3dMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractAnimationCurve3dBuilder
        {
            public override AbstractTweenMotion Build()
                => new MoveAnimationCurve3dMotion(item, animationCurve);
        }

        public MoveAnimationCurve3dMotion(Transform item, AnimationCurve3d animationCurve) : base(item, animationCurve)
        {
        }

        public override void OnUpdate(float t)
        {
            item.position = animationCurve.Evaluate(t);
        }
    }
}
