using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class BezierSpline : AbstractSpline
    {
        private const float twoThirds = 2f / 3f;
        public BezierSpline(List<Vector3> points) : base(points)
        {
        }

        public BezierSpline(params Vector3[] points) : base(points)
        {
        }

        private Vector3 startPoint;
        private Vector3 startControl;
        private Vector3 endControl;
        private Vector3 endPoint;
        private int segmentCount;
        private float segmentLength;

        protected override void Init()
        {
            if (points.Length == 4)
            {
                return;
            }
            if (points.Length == 3)
            {
                points = new Vector3[] { points[0], points[1], points[1], points[2] };
                return;
            }
            if (points.Length == 2)
            {
                points = new Vector3[] { points[0], points[0], points[1], points[1] };
                return;
            }

            segmentCount = points.Length - 4;
            segmentLength = 1f / segmentCount;

            int count = points.Length;
            Vector3[] pointsCache = new Vector3[count];
            pointsCache[0] = points[0];
            pointsCache[count - 1] = points[count - 1];
            for (int i = 1; i < count - 1; i++)
            {
                pointsCache[i] = (points[i - 1] / 6f) + (points[i] * twoThirds) + (points[i + 1] / 6f);
            }
            points = pointsCache;
        }


        public override Vector3 GetPoint(float t)
        {
            //if there are no segments we simply have a bezier case
            if (segmentCount == 0)
            {
                startPoint = points[0];
                startControl = points[1];
                endControl = points[2];
                endPoint = points[3];
            }
            //otherwise we have to lerp through the points and apply bezier for every segment
            else
            {
                int segment = (int)(t / segmentLength);

                if (segment >= segmentCount)
                {
                    segment = segmentCount - 1;
                }

                float segmentT = (t - (segmentLength * segment)) / segmentLength;

                //Debug.Log($"{t} - {segment}, {segmentT}");

                startPoint = Vector3.LerpUnclamped(points[segment], points[segment + 1], segmentT);
                startControl = Vector3.LerpUnclamped(points[segment + 1], points[segment + 2], segmentT);
                endControl = Vector3.LerpUnclamped(points[segment + 2], points[segment + 3], segmentT);
                endPoint = Vector3.LerpUnclamped(points[segment + 3], points[segment + 4], segmentT);
            }
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