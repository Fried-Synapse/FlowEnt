using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localScale" /> value using an animation curve 3d.
    /// </summary>
    public class ScaleLocalAnimationCurve3dMotion : AbstractAnimationCurve3dMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractBuilder
        {
            public override ITweenMotion Build()
                => new ScaleLocalAnimationCurve3dMotion(item, animationCurve);
        }

        public ScaleLocalAnimationCurve3dMotion(Transform item, AnimationCurve3d animationCurve) : base(item, animationCurve)
        {
        }

        public override void OnUpdate(float t)
        {
            item.localScale = animationCurve.Evaluate(t);
        }
    }
}
