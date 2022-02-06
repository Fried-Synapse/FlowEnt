//used https://github.com/BSVino/MathForGameDevelopers/blob/numerical-cubic-spline/math/spline.h
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class CubicSpline : AbstractSpline
    {
        /// <summary>
        /// Creates a cubic spline.
        /// </summary>
        /// <param name="points">The sequence of points that will create the spline.</param>
        public CubicSpline(List<Vector3> points) : base(points)
        {
            Init();
        }

        /// <summary>
        /// Creates a cubic spline.
        /// </summary>
        /// <param name="points">The sequence of points that will create the spline.</param>
        public CubicSpline(params Vector3[] points) : base(points)
        {
            Init();
        }

        private int pointsCount;
        private int segmentsCount;
        private Vector3[][] coefficients;
        private float[] lengths;

        private void Init()
        {
            pointsCount = points.Length;
            segmentsCount = pointsCount - 1;
            coefficients = new Vector3[pointsCount][];
            lengths = new float[segmentsCount];

            Vector3[] a = new Vector3[pointsCount];
            for (int i = 1; i <= segmentsCount - 1; i++)
            {
                a[i] = 3 * (points[i + 1] - (2 * points[i]) + points[i - 1]);
            }

            float[] l = new float[pointsCount];
            float[] mu = new float[pointsCount];
            Vector3[] z = new Vector3[pointsCount];

            l[0] = l[segmentsCount] = 1;
            mu[0] = 0;
            z[0] = z[segmentsCount] = Vector3.zero;
            coefficients[segmentsCount] = new Vector3[4];
            coefficients[segmentsCount][2] = Vector3.zero;

            for (int i = 1; i <= segmentsCount - 1; i++)
            {
                l[i] = 4 - mu[i - 1];
                mu[i] = 1 / l[i];
                z[i] = (a[i] - z[i - 1]) / l[i];
            }

            for (int i = 0; i < segmentsCount; i++)
            {
                coefficients[i] = new Vector3[4];
                coefficients[i][0] = points[i];
            }
            coefficients[segmentsCount][0] = points[segmentsCount];

            const float third = 1.0f / 3.0f;
            for (int j = segmentsCount - 1; j >= 0; j--)
            {
                coefficients[j][2] = z[j] - (mu[j] * coefficients[j + 1][2]);
                coefficients[j][3] = third * (coefficients[j + 1][2] - coefficients[j][2]);
                coefficients[j][1] = points[j + 1] - points[j] - coefficients[j][2] - coefficients[j][3];
            }

            for (int k = 0; k < segmentsCount; k++)
            {
                lengths[k] = Integrate(k, 1);
            }
        }

        private float Integrate(int spline, float t)
        {
            const int n = 16;
            float h = t / n;
            float XI0 = ArcLengthIntegrand(spline, t);
            float XI1 = 0;
            float XI2 = 0;

            for (int i = 0; i < n; i++)
            {
                float X = i * h;
                if (i % 2 == 0)
                {
                    XI2 += ArcLengthIntegrand(spline, X);
                }
                else
                {
                    XI1 += ArcLengthIntegrand(spline, X);
                }
            }

            return (float)(h * (XI0 + (2 * XI2) + (4 * XI1)) * (1.0f / 3));
        }

        private float ArcLengthIntegrand(int spline, float t)
        {
            float tt = t * t;

            Vector3 dv = coefficients[spline][1] + (2 * coefficients[spline][2] * t) + (3 * coefficients[spline][3] * tt);
            float xx = dv.x * dv.x;
            float yy = dv.y * dv.y;
            float zz = dv.z * dv.z;

            return Mathf.Sqrt(xx + yy + zz);
        }

        public override Vector3 GetPoint(float t)
        {
            float scaledT = t * segmentsCount;
            int segment = (int)scaledT;
            float x = (float)(scaledT - segment);
            float xx = x * x;
            float xxx = x * xx;

            return coefficients[segment][0] + (coefficients[segment][1] * x) + (coefficients[segment][2] * xx) + (coefficients[segment][3] * xxx);
        }
    }
}
