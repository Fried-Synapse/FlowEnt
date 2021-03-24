using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlowEnt
{
    public class BezierSpline : ISpline
    {
        private List<Vector3> Points { get; set; } = new List<Vector3>();
        private int SegmentCount { get; set; }
        private float SegmentLenght { get; set; }

        /// <summary>
        /// Creates a Bezier Spline. This is a spline based on the Bezier Curve. 
        /// </summary>
        /// <param name="points">The sequence of points that will create the spline.</param
        /// <remarks>
        /// If 2 points are sent p0 will be startPoint and startControl, and p1 will be endControl and endPoint.
        /// If 3 points are sent p0 will be startPoint, p1 will be startControl and endControl, and p2 will be endPoint.
        /// If 4 points are sent p0 will be startPoint, p1 will be startControl, p2 will be endControl, and p3 will be endPoint.
        /// If 5 or more points are sent it'll create a spline with a bezier movement between the points.
        /// </remarks>
        public BezierSpline(params Vector3[] points)
        {
            if (points == null)
            {
                throw new ArgumentException("Array cannot be null");
            }
            if (points.Length < 2)
            {
                throw new ArgumentException("Not enough points specified");
            }
            Init(new List<Vector3>(points));
        }

        /// <summary>
        /// Creates a Bezier Spline. This is a spline based on the Bezier Curve. 
        /// </summary>
        /// <param name="points">The sequence of points that will create the spline.</param
        /// <remarks>
        /// If 2 points are sent p0 will be startPoint and startControl, and p1 will be endControl and endPoint.
        /// If 3 points are sent p0 will be startPoint, p1 will be startControl and endControl, and p2 will be endPoint.
        /// If 4 points are sent p0 will be startPoint, p1 will be startControl, p2 will be endControl, and p3 will be endPoint.
        /// If 5 or more points are sent it'll create a spline with a bezier movement between the points.
        /// </remarks>
        public BezierSpline(List<Vector3> points)
        {
            if (points == null)
            {
                throw new ArgumentException("List cannot be null");
            }
            if (points.Count < 2)
            {
                throw new ArgumentException("Not enough points specified");
            }
            Init(points);
        }

        private void Init(List<Vector3> points)
        {
            if (points.Count == 2)
            {
                points.Add(points[1]);
                points.Insert(0, points[0]);
            }
            if (points.Count == 3)
            {
                points.Insert(1, points[1]);
            }
            Points = points;
            SegmentCount = Points.Count - 4;
            SegmentLenght = 1f / SegmentCount;
        }

        public Vector3 GetPoint(float t)
        {
            Vector3 startPoint;
            Vector3 startControl;
            Vector3 endControl;
            Vector3 endPoint;

            //if there are no segments we simply have a bezier case
            if (SegmentCount == 0)
            {
                startPoint = Points[0];
                startControl = Points[1];
                endControl = Points[2];
                endPoint = Points[3];
            }
            //otherwise we have to lerp through the points and apply bezier for every segment
            else
            {
                int segment = (int)(t / SegmentLenght);
                if (segment >= SegmentCount)
                {
                    segment = SegmentCount - 1;
                }

                float segmentT = (t - (SegmentLenght * segment)) / SegmentLenght;

                startPoint = Vector3.Lerp(Points[segment], Points[segment + 1], segmentT);
                startControl = Vector3.Lerp(Points[segment + 1], Points[segment + 2], segmentT);
                endControl = Vector3.Lerp(Points[segment + 2], Points[segment + 3], segmentT);
                endPoint = Vector3.Lerp(Points[segment + 3], Points[segment + 4], segmentT);
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