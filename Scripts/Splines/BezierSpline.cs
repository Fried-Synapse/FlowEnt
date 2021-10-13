using System;
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
        Vector3[] smoothPoints;
        private int segmentCount;
        private int segment = -1;
        private int segmentPlusOne;
        private Vector3 startControl;
        private Vector3 endControl;
        private Vector3 endPoint;

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

            segmentCount = points.Length - 1;

            int count = points.Length;
            smoothPoints = new Vector3[count];
            smoothPoints[0] = points[0];
            smoothPoints[count - 1] = points[count - 1];
            for (int i = 1; i < count - 1; i++)
            {
                smoothPoints[i] = (points[i - 1] / 6f) + (points[i] * twoThirds) + (points[i + 1] / 6f);
            }
        }

        public override Vector3 GetPoint(float t)
        {
            float segmentedT = t * segmentCount;
            int currentSegment = Mathf.FloorToInt(segmentedT);
            if (currentSegment != segment)
            {
                segment = currentSegment;
                segmentPlusOne = Math.Min(segment + 1, segmentCount);

                startControl = (points[segment] * twoThirds) + (points[segmentPlusOne] / 3f);
                endControl = (points[segment] / 3f) + (points[segmentPlusOne] * twoThirds);
                endPoint = smoothPoints[segmentPlusOne];
            }

            float segmentT = segmentedT - segment;

            return GetPoint(segmentT, smoothPoints[segment], startControl, endControl, endPoint);
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