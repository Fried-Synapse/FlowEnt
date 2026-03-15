using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    /// <summary>
    /// Lerps a <see cref="AnimationCurve" /> value.
    /// </summary>
    public class AnimationCurveValueMotion : AbstractEventMotion<float>
    {
        [Serializable]
        public class Builder : AbstractEventMotionBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private AnimationCurve animationCurve;

            public override AbstractTweenMotion Build()
                => new AnimationCurveValueMotion(animationCurve, GetCallback());
#pragma warning restore IDE0044, RCS1169
        }

        public AnimationCurveValueMotion(AnimationCurve animationCurve, Action<float> onUpdated) : base(onUpdated)
        {
            this.animationCurve = animationCurve;
        }

        private readonly AnimationCurve animationCurve;

        public override void OnUpdate(float t)
        {
            onUpdated(animationCurve.Evaluate(t));
        }
    }
}