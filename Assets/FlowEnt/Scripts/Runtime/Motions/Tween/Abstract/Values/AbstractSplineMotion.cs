using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractSplineMotion<TItem> : AbstractTweenMotion<TItem>
        where TItem : class
    {
        [Serializable]
        public abstract class AbstractBuilder : AbstractTweenMotionBuilder<TItem>
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private SplineFactory.SplineType type;
            [SerializeField]
            private bool normalise;
            // [SerializeField]
            // private bool preview;
            [SerializeField]
            private List<Vector3> points;
#pragma warning restore IDE0044, RCS1169

            protected ISpline GetSpline()
                => SplineFactory.GetSpline(type, points, normalise);

#if UNITY_EDITOR
#pragma warning disable IDE0051, RCS1213
            //TODO this function is not called because this is not a monobehaviour...
            private void OnDrawGizmos()
            {
                // if (preview)
                // {
                //     GetSpline().DrawGizmo(Color.white, 2f);
                // }
            }
#pragma warning restore IDE0051, RCS1213
#endif
        }

        protected AbstractSplineMotion(TItem item, ISpline spline) : base(item)
        {
            this.spline = spline;
        }

        protected readonly ISpline spline;
    }
}
