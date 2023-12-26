using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    /// <summary>
    /// Lerps an <see cref="ISpline" />.
    /// </summary>
    public class SplineValueMotion : AbstractEventMotion<Vector3>
    {
        [Serializable]
        public class Builder : AbstractEventMotionBuilder
        {
            [SerializeField]
            [UrlButton(UrlButtonAttribute.PredefinedType.Splines)]
            private SplineFactory.SplineType type;

            [SerializeField]
            private bool normalise;

            [SerializeField]
            private List<Vector3> points = new() { Vector3.zero, Vector3.zero };

            public override ITweenMotion Build()
                => new SplineValueMotion(SplineFactory.GetSpline(type, points, normalise), GetCallback());
        }

        public SplineValueMotion(ISpline spline, Action<Vector3> onUpdated) : base(onUpdated)
        {
            this.spline = spline;
        }

        private readonly ISpline spline;

        public override void OnUpdate(float t)
        {
            onUpdated(spline.GetPoint(t));
        }
    }
}