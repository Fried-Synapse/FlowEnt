using System;
using UnityEngine;

namespace FlowEnt.Motions.Values
{
    public class SplineValueMotion : AbstractMotion
    {
        public SplineValueMotion(ISpline spline, Action<Vector3> callback)
        {
            this.spline = spline;
            this.callback = callback;
        }

        private readonly ISpline spline;
        private readonly int to;
        private readonly Action<Vector3> callback;

        public override void OnUpdate(float t)
        {
            callback(spline.GetPoint(t));
        }
    }
}