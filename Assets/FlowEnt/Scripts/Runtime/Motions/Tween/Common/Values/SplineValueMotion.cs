using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    public class SplineValueMotion : AbstractEventMotion<Vector3>
    {
        [Serializable]
        public class Builder : AbstractEventMotionBuilder
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

            public override ITweenMotion Build()
                => new SplineValueMotion(SplineFactory.GetSpline(type, points, normalise), GetCallback());

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
        /// <summary>
        /// Lerps an <see cref="ISpline" />.
        /// </summary>
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