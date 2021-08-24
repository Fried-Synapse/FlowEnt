using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class LinearSpline : ISpline
    {
        private List<Vector3> Points { get; set; } = new List<Vector3>();
        private List<float> DistanceRatios { get; set; } = new List<float>();
        private List<float> SummedDistanceRatios { get; set; } = new List<float>();

        /// <summary>
        /// Creates a linear spline. Uncurvy.
        /// </summary>
        /// <param name="points">The sequence of points that will create the spline.</param>
        public LinearSpline(params Vector3[] points)
        {
            Init(new List<Vector3>(points));
        }

        /// <summary>
        /// Creates a linear spline. Uncurvy.
        /// </summary>
        /// <param name="points">The sequence of points that will create the spline.</param>
        public LinearSpline(List<Vector3> points)
        {
            Init(points);
        }

        private void Init(List<Vector3> points)
        {
            Points = points;
            float distance = 0;
            List<float> distances = new List<float>();
            for (int i = 0; i < Points.Count - 1; i++)
            {
                float segmentDistance = Vector3.Distance(Points[i], Points[i + 1]);
                distance += segmentDistance;
                distances.Add(segmentDistance);
            }

            float summedDistance = 0;
            for (int i = 0; i < distances.Count; i++)
            {
                float segmentDistance = distances[i] / distance;
                DistanceRatios.Add(segmentDistance);
                SummedDistanceRatios.Add(summedDistance);
                summedDistance += segmentDistance;
            }
        }

        public Vector3 GetPoint(float t)
        {
            int segment = 0;
            for (int i = 1; i < SummedDistanceRatios.Count; i++, segment++)
            {
                if (t < SummedDistanceRatios[i])
                {
                    break;
                }
            }

            float segmentT = (t - SummedDistanceRatios[segment]) / DistanceRatios[segment];

            return Vector3.LerpUnclamped(Points[segment], Points[segment + 1], segmentT);
        }
    }
}