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
            private SplineFactory.SplineType type;

            [SerializeField]
            private bool normalise;

            // [SerializeField]
            // private bool preview;
            [SerializeField]
            private List<Vector3> points = new() { Vector3.zero, Vector3.zero };

            protected ISpline GetSpline()
                => SplineFactory.GetSpline(type, points, normalise);

#if UNITY_EDITOR
            //TODO this function is not called because this is not a monobehaviour...
            private void OnDrawGizmos()
            {
                // if (preview)
                // {
                //     GetSpline().DrawGizmo(Color.white, 2f);
                // }
            }
#endif
        }

        protected AbstractSplineMotion(TItem item, ISpline spline) : base(item)
        {
            this.spline = spline;
        }

        protected readonly ISpline spline;
    }
}