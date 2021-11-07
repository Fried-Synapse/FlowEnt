using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Easings
{
    public abstract class AbstractOscilationEasing : IEasing
    {
        protected const int MinReps = 1;
        protected const int MaxReps = 20;
        protected const int DefaultReps = 3;
        protected const bool DefaultReverse = false;
        protected const float MinIntensity = 0f;
        protected const float MaxIntensity = 0.7f;
        protected const float DefaultIntensity = 0.5f;

        protected AbstractOscilationEasing(int reps = DefaultReps, bool reverse = DefaultReverse, float intensity = DefaultIntensity)
        {
            if (reps < MinReps || MaxReps < reps)
            {
                throw new ArgumentException($"The number of {RepsName} has to be between {MinReps} and {MaxReps}.");
            }

            if (intensity < MinIntensity || MaxIntensity < intensity)
            {
                throw new ArgumentException($"The {IntensityName} has to be between {MinIntensity} and {MaxIntensity}.");
            }

            this.reps = reps;
            this.intensity = intensity;
            this.reverse = reverse;

            peaks = new float[reps];
            peakSum = (Mathf.Pow(intensity, reps) - 1) / (intensity - 1);
            for (int i = 0; i < reps; i++)
            {
                peaks[i] = Mathf.Pow(intensity, i);
            }

            times = new float[reps];
            startTimes = new float[reps];
            float segmentStartTime = 0f;
            for (int i = 0; i < reps; i++)
            {
                float segmentTime = peaks[i] / peakSum;
                times[i] = segmentTime;
                startTimes[i] = segmentStartTime;
                segmentStartTime += segmentTime;
            }
        }

        protected abstract string RepsName { get; }
        protected abstract string IntensityName { get; }

        private readonly int reps;
        private readonly bool reverse;
        private readonly float intensity;
        private readonly float[] peaks;
        private readonly float[] times;
        private readonly float[] startTimes;
        private readonly float peakSum;

        public abstract float GetValue(float t);
        protected (int, float) GetSegmentAndValue(float t)
        {
            t = reverse ? (1 - t) : t;
            float sum = t * peakSum;
            float top = Mathf.Log((1f - intensity) * ((1f / (1f - intensity)) - sum));
            int segment = (int)(top / Mathf.Log(intensity));
            //HACK due to the fact that the result is flawed because of the lack of precision if we get a NaN we assume we're on the last bit.
            if (segment < 0 || reps <= segment || float.IsNaN(top))
            {
                segment = reps - 1;
            }
            float time = times[segment];
            float root = ((2 * t) - time - (2 * startTimes[segment])) / time;
            return (segment, peaks[segment] * (-(root * root) + 1));
        }
    }
}
