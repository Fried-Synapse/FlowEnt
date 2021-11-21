using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Values
{
    public class SplineValueMotion : AbstractMotion
    {
        /// <summary>
        /// Lerps an <see cref="ISpline" />.
        /// </summary>
        public SplineValueMotion(ISpline spline, Action<Vector3> callback)
        {
            this.spline = spline;
            this.callback = callback;
        }

        private readonly ISpline spline;
        private readonly Action<Vector3> callback;

        public override void OnUpdate(float t)
        {
            callback(spline.GetPoint(t));
        }
    }
}