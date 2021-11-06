using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class BounceEasing : IEasing
    {
        private const int MinBounces = 1;
        private const int MaxBounces = 20;
        private const float MinBounciness = 0f;
        private const float MaxBounciness = 0.7f;

        public BounceEasing(int bounces, float bounciness = 0.5f)
        {
            if (bounces < MinBounces || MaxBounces < bounces)
            {
                throw new ArgumentException($"The number of bounces has to be between {MinBounces} and {MaxBounces}.");
            }

            if (bounciness < MinBounciness || MaxBounciness < bounciness)
            {
                throw new ArgumentException($"The bounciness has to be between {MinBounciness} and {MaxBounciness}.");
            }

            this.bounces = bounces;
            this.bounciness = bounciness;

            peaks = new float[bounces];
            peakSum = (Mathf.Pow(bounciness, bounces) - 1) / (bounciness - 1);
            for (int i = 0; i < bounces; i++)
            {
                peaks[i] = Mathf.Pow(bounciness, i);
            }

            times = new float[bounces];
            startTimes = new float[bounces];
            float segmentStartTime = 0f;
            for (int i = 0; i < bounces; i++)
            {
                float segmentTime = peaks[i] / peakSum;
                times[i] = segmentTime;
                startTimes[i] = segmentStartTime;
                segmentStartTime += segmentTime;
            }
        }

        private readonly int bounces;
        private readonly float bounciness;
        private readonly float[] peaks;
        private readonly float[] times;
        private readonly float[] startTimes;
        private readonly float peakSum;

        public float GetValue(float t)
        {
            float sum = t * peakSum;
            float top = Mathf.Log((1f - bounciness) * ((1f / (1f - bounciness)) - sum));
            int segment = (int)(top / Mathf.Log(bounciness));
            //HACK due to the fact that the result is flawed because of the lack of precision if we get a NaN we assume we're on the last bit.
            if (segment < 0 || bounces <= segment || float.IsNaN(top))
            {
                segment = bounces - 1;
            }
            float time = times[segment];
            float root = ((2 * t) - time - (2 * startTimes[segment])) / time;
            return peaks[segment] * (-(root * root) + 1);
        }
    }
}
