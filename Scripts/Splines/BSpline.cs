//Special thanks to Saksun Young(https://github.com/saksunyoung) for figuring out the maths on this one.
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class BSpline : AbstractSpline
    {
        public const float DefaultGradient = 1f / 3f;
        public BSpline(List<Vector3> points, float gradient = DefaultGradient) : base(points)
        {
            this.gradient = gradient;
            Init();
        }

        public BSpline(params Vector3[] points) : base(points)
        {
            this.gradient = DefaultGradient;
            Init();
        }

        private Vector3[] smoothPoints;
        private Vector3[] pointsGradient;
        private Vector3[] pointsInverseGradient;
        private Vector3[] pointsHalfGradient;
        private int segmentCount;
        private int segment = -1;
        private int segmentPlusOne;
        private Vector3 startPoint;
        private Vector3 startControl;
        private Vector3 endControl;
        private Vector3 endPoint;
        private readonly float gradient;

        private void Init()
        {
            segmentCount = points.Length - 1;

            int count = points.Length;

            pointsGradient = new Vector3[count];
            pointsInverseGradient = new Vector3[count];
            pointsHalfGradient = new Vector3[count];

            float halfGradient = gradient / 2f;
            float inverseGradient = 1f - gradient;

            for (int i = 0; i < count; i++)
            {
                pointsGradient[i] = points[i] * gradient;
                pointsInverseGradient[i] = points[i] * inverseGradient;
                pointsHalfGradient[i] = points[i] * halfGradient;
            }

            smoothPoints = new Vector3[count];
            smoothPoints[0] = points[0];
            smoothPoints[count - 1] = points[count - 1];
            for (int i = 1; i < count - 1; i++)
            {
                smoothPoints[i] = pointsHalfGradient[i - 1] + pointsInverseGradient[i] + pointsHalfGradient[i + 1];
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

                startPoint = smoothPoints[segment];
                startControl = pointsInverseGradient[segment] + pointsGradient[segmentPlusOne];
                endControl = pointsGradient[segment] + pointsInverseGradient[segmentPlusOne];
                endPoint = smoothPoints[segmentPlusOne];
            }

            float segmentT = segmentedT - segment;

            return BezierCurve.GetPoint(segmentT, startPoint, startControl, endControl, endPoint);
        }
    }
}