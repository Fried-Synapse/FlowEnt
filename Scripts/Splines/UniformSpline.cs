using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class UniformSpline
    {
        public const int DefaultResolution = 100;
    }

    public class UniformSpline<TUniformableSpline> : UniformSpline, ISpline
        where TUniformableSpline : IUniformableSpline
    {
        public UniformSpline(TUniformableSpline uniformableSpline, int resolution = DefaultResolution)
        {
            this.uniformableSpline = uniformableSpline;
            this.resolution = resolution;
            this.resoltionStep = 1f / resolution;
            this.distances = new float[resolution - 1];
            Init();
        }
        private readonly TUniformableSpline uniformableSpline;
        private readonly int resolution;
        private readonly float resoltionStep;
        private readonly float[] distances;

        private void Init()
        {
            Vector3 start = uniformableSpline.GetPoint(0);
            Vector3 end;
            for ((int i, float endStep) = (0, resoltionStep); i < resolution - 1; i++, endStep += resoltionStep)
            {
                end = uniformableSpline.GetPoint(endStep);
                distances[i] = Vector3.Distance(start, end);
                start = end;
            }
        }

        public Vector3 GetPoint(float t)
        {
            return uniformableSpline.GetPoint(t);
        }
    }
}
