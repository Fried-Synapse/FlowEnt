using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class CurveBuilder : AbstractBuilder<ICurve>, IGizmoDrawer
    {
        public enum CurveType
        {
            BezierCurve,
            _,
            LinearSpline,
            BSpline,
            CatmullRomSpline,
            CubicSpline,
        }

        [Serializable]
        private class BezierPoints
        {
            [SerializeField]
            internal Vector3 startPoint;

            [SerializeField]
            internal Vector3 startControl;

            [SerializeField]
            internal Vector3 endControl;

            [SerializeField]
            internal Vector3 endPoint;
        }

        [SerializeField]
        [UrlButton(UrlButtonAttribute.PredefinedType.Curves)]
        private CurveType type;

        [SerializeField, Tooltip("Normalised curves are more expensive performance wise")]
        private bool normalise;

        [SerializeField]
        private BezierPoints bezierPoints;

        [SerializeField]
        private List<Vector3> splinePoints = new() { Vector3.zero, Vector3.zero };

        public override ICurve Build()
        {
            ICurve result = type switch
            {
                CurveType.BezierCurve
                    => new BezierCurve(bezierPoints.startPoint, bezierPoints.startControl, bezierPoints.endControl, bezierPoints.endPoint),
                CurveType.LinearSpline => new LinearSpline(splinePoints),
                CurveType.BSpline => new BSpline(splinePoints),
                CurveType.CatmullRomSpline => new CatmullRomSpline(splinePoints),
                CurveType.CubicSpline => new CubicSpline(splinePoints),
                _ => throw new NotImplementedException(),
            };

            if (normalise)
            {
                result = new NormalisedCurve(result);
            }

            return result;
        }

#if UNITY_EDITOR
        void IGizmoDrawer.OnGizmosDrawing(GizmoOptions options)
        {
            FlowEntGizmos.DrawCurve(Build(), options);
        }
#endif
    }
}