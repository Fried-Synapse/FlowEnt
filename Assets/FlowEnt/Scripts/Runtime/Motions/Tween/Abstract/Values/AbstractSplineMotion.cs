using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractSplineMotion<TItem> : AbstractTweenMotion<TItem>
    {
        [Serializable]
        public abstract class AbstractSplineBuilder : AbstractBuilder
        {
            [SerializeField]
            [UrlButton(UrlButtonAttribute.PredefinedType.Splines)]
            private SplineFactory.SplineType type;

            [SerializeField]
            private bool normalise;

            [SerializeField]
            private List<Vector3> points = new() { Vector3.zero, Vector3.zero };

            protected ISpline GetSpline()
                => SplineFactory.GetSpline(type, points, normalise);
        }

        protected AbstractSplineMotion(TItem item, ISpline spline) : base(item)
        {
            this.spline = spline;
        }

        protected readonly ISpline spline;
    }
}