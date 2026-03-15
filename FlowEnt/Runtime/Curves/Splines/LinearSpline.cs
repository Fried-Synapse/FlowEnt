using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class LinearSpline : AbstractSpline
    {
        /// <summary>
        /// Creates a linear spline. Uncurvy.
        /// </summary>
        /// <param name="points">The sequence of points that will create the spline.</param>
        public LinearSpline(params Vector3[] points) : base(points)
        {
            Init();
        }

        /// <summary>
        /// Creates a linear spline. Uncurvy.
        /// </summary>
        /// <param name="points">The sequence of points that will create the spline.</param>
        public LinearSpline(List<Vector3> points) : base(points)
        {
            Init();
        }

        private float[] distanceRatios;
        private float[] summedDistanceRatios;

        private void Init()
        {
            int count = points.Length;
            float distance = 0;
            float[] distances = new float[count - 1];
            distanceRatios = new float[count - 1];
            summedDistanceRatios = new float[count - 1];
            for (int i = 0; i < count - 1; i++)
            {
                float segmentDistance = Vector3.Distance(points[i], points[i + 1]);
                distance += segmentDistance;
                distances[i] = segmentDistance;
            }

            float summedDistance = 0;
            for (int i = 0; i < count - 1; i++)
            {
                float segmentDistance = distances[i] / distance;
                distanceRatios[i] = segmentDistance;
                summedDistanceRatios[i] = summedDistance;
                summedDistance += segmentDistance;
            }
        }

        public override Vector3 GetPoint(float t)
        {
            int segment = 0;
            for (int i = 1; i < summedDistanceRatios.Length; i++, segment++)
            {
                if (t < summedDistanceRatios[i])
                {
                    break;
                }
            }

            float segmentT = (t - summedDistanceRatios[segment]) / distanceRatios[segment];

            return Vector3.LerpUnclamped(points[segment], points[segment + 1], segmentT);
        }
    }
}