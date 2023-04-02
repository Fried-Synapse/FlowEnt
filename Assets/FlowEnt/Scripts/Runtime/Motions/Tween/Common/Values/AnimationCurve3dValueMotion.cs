using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    /// <summary>
    /// Lerps a <see cref="AnimationCurve" /> value.
    /// </summary>
    public class AnimationCurve3dValueMotion : AbstractEventMotion<Vector3>
    {
        [Serializable]
        public class Builder : AbstractEventMotionBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private AnimationCurve3d animationCurve;

            public override ITweenMotion Build()
                => new AnimationCurve3dValueMotion(animationCurve, GetCallback());
#pragma warning restore IDE0044, RCS1169
        }

        public AnimationCurve3dValueMotion(AnimationCurve3d animationCurve, Action<Vector3> onUpdated) : base(onUpdated)
        {
            this.animationCurve = animationCurve;
        }

        private readonly AnimationCurve3d animationCurve;

        public override void OnUpdate(float t)
        {
            onUpdated(animationCurve.Evaluate(t));
        }
    }
}