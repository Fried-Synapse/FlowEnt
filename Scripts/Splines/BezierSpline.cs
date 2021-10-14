//Special thanks to Saksun Young for figuring out the maths on this one
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
        private Vector3[] smoothPoints;
        private Vector3[] pointsThirds;
        private Vector3[] pointsTwoThirds;
        private Vector3[] pointsSixths;
        private int segmentCount;
        private int segment = -1;
        private int segmentPlusOne;
        private Vector3 startControl;
        private Vector3 endControl;
        private Vector3 endPoint;

        protected override void Init()
        {
            segmentCount = points.Length - 1;

            int count = points.Length;
            smoothPoints = new Vector3[count];
            pointsThirds = new Vector3[count];
            pointsTwoThirds = new Vector3[count];
            pointsSixths = new Vector3[count];
            smoothPoints[0] = points[0];
            smoothPoints[count - 1] = points[count - 1];
            for (int i = 0; i < count; i++)
            {
                pointsThirds[i] = points[i] / 3f;
                pointsTwoThirds[i] = points[i] * twoThirds;
                pointsSixths[i] = points[i] / 6f;
            }
            for (int i = 1; i < count - 1; i++)
            {
                smoothPoints[i] = pointsSixths[i - 1] + pointsTwoThirds[i] + pointsSixths[i + 1];
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

                startControl = pointsTwoThirds[segment] + pointsThirds[segmentPlusOne];
                endControl = pointsThirds[segment] + pointsTwoThirds[segmentPlusOne];
                endPoint = smoothPoints[segmentPlusOne];
            }

            float segmentT = segmentedT - segment;

            return BezierCurve.GetPoint(segmentT, smoothPoints[segment], startControl, endControl, endPoint);
        }
    }
}