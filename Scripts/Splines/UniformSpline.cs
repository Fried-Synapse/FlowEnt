using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class NormalisedSpline
    {
        public const int DefaultResolution = 100;
    }

    public class NormalisedSpline<TNormalisableSpline> : NormalisedSpline, ISpline
        where TNormalisableSpline : INormalisableSpline
    {
        public NormalisedSpline(TNormalisableSpline uniformableSpline, int resolution = DefaultResolution)
        {
            this.uniformableSpline = uniformableSpline;
            this.resolution = resolution;
            this.resoltionStep = 1f / resolution;
            this.distances = new float[resolution];
            this.distanceTravelled = new float[resolution];
            Init();
        }
        private readonly TNormalisableSpline uniformableSpline;
        private readonly int resolution;
        private readonly float resoltionStep;
        private readonly float[] distances;
        private readonly float[] distanceTravelled;
        private float distance;

        private void Init()
        {
            Vector3 start = uniformableSpline.GetPoint(0);
            Vector3 end;
            distanceTravelled[0] = 0;
            for ((int i, float endStep) = (0, resoltionStep); i < resolution - 1; i++, endStep += resoltionStep)
            {
                end = uniformableSpline.GetPoint(endStep);
                float stepDistance = Vector3.Distance(start, end);
                distances[i] = stepDistance;
                distance += stepDistance;
                distanceTravelled[i + 1] = distance;
                start = end;
            }
            distances[resolution - 1] = 0;
        }

        public Vector3 GetPoint(float t)
        {
            float distanceT = t * distance;







            float scaledT = t * resolution;
            int segment = (int)scaledT;
            if (segment == resolution)
            {
                return uniformableSpline.GetPoint(1);
            }
            float segmentT = scaledT - segment;
            float normalisedT = (distanceTravelled[segment] + (segmentT * distances[segment])) / distance;
            return uniformableSpline.GetPoint(normalisedT);
        }
    }
}
