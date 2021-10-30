using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class BezierCurve : ISpline
    {
        /// <summary>
        /// Creates a bezier curve.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="startControl"></param>
        /// <param name="endControl"></param>
        /// <param name="endPoint"></param>
        public BezierCurve(Vector3 startPoint, Vector3 startControl, Vector3 endControl, Vector3 endPoint)
        {
            this.startPoint = startPoint;
            this.startControl = startControl;
            this.endControl = endControl;
            this.endPoint = endPoint;
        }

        private Vector3 startPoint;
        private Vector3 startControl;
        private Vector3 endControl;
        private Vector3 endPoint;

        public Vector3 GetPoint(float t)
        {
            return GetPoint(t, startPoint, startControl, endControl, endPoint);
        }

        public static Vector3 GetPoint(float t, Vector3 startPoint, Vector3 startControl, Vector3 endControl, Vector3 endPoint)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 p = uuu * startPoint;
            p += 3 * uu * t * startControl;
            p += 3 * u * tt * endControl;
            p += ttt * endPoint;

            return p;
        }
    }
}
