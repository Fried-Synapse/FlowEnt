using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    /// <summary>
    /// Lerps a <see cref="AnimationCurve" /> value.
    /// </summary>
    public class AnimationCurve2dValueMotion : AbstractEventMotion<Vector2>
    {
        [Serializable]
        public class Builder : AbstractEventMotionBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private AnimationCurve2d animationCurve;

            public override ITweenMotion Build()
                => new AnimationCurve2dValueMotion(animationCurve, GetCallback());
#pragma warning restore IDE0044, RCS1169
        }

        public AnimationCurve2dValueMotion(AnimationCurve2d animationCurve, Action<Vector2> onUpdated) : base(onUpdated)
        {
            this.animationCurve = animationCurve;
        }

        private readonly AnimationCurve2d animationCurve;

        public override void OnUpdate(float t)
        {
            onUpdated(animationCurve.Evaluate(t));
        }
    }
}