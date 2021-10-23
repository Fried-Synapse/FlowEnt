using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class CatmullRomSpline : AbstractSpline
    {
        public CatmullRomSpline(List<Vector3> points) : base(points)
        {
            Init();
        }

        public CatmullRomSpline(params Vector3[] points) : base(points)
        {
            Init();
        }

        protected override int MinPoints => 4;
        private float segments;
        private void Init()
        {
            segments = points.Length - 3;
        }

        public override Vector3 GetPoint(float t)
        {
            float scaledT, segmentT;
            int segment;
            try
            {
                scaledT = t * segments;
                segment = (int)scaledT;
                if (segment == segments)
                {
                    segment--;
                }
                segmentT = scaledT - segment;
                return GetPoint(segmentT, points[segment], points[segment + 1], points[segment + 2], points[segment + 3]);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public static Vector3 GetPoint(float t, Vector3 startControl, Vector3 startPoint, Vector3 endPoint, Vector3 endControl)
        {
            float tt = t * t;
            float ttt = tt * t;

            float q1 = -ttt + (2f * tt) - t;
            float q2 = (3f * ttt) - (5f * tt) + 2f;
            float q3 = (-3f * ttt) + (4f * tt) + t;
            float q4 = ttt - tt;

            return 0.5f * ((startControl * q1) + (startPoint * q2) + (endPoint * q3) + (endControl * q4));
        }
    }
}
