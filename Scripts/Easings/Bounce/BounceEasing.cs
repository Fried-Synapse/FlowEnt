using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class BounceEasing : IEasing
    {
        public BounceEasing(int bounces, float bounciness = 1f)
        {
            if (bounces < 1)
            {
                throw new ArgumentException("The number of bounces has to be greater than 1.");
            }

            if (bounciness < 0 || bounciness > 1)
            {
                throw new ArgumentException("The bounciness has to be between 0 and 1.");
            }

            this.bounces = bounces;

            peaks = new float[bounces];
            times = new float[bounces];
            startTimes = new float[bounces];
            bouncesSum = bounces * (1 + bounces) / 2;
            float segmentStartTime = 0f;
            for (int i = 0; i < bounces; i++)
            {
                float segmentTime = (bounces - i) / (float)bouncesSum;
                peaks[i] = (1 - segmentStartTime) * Mathf.Pow(bounciness, i);
                times[i] = segmentTime;
                startTimes[i] = segmentStartTime;
                segmentStartTime += segmentTime;
            }
        }

        private readonly int bounces;
        private readonly float[] peaks;
        private readonly float[] times;
        private readonly float[] startTimes;
        private readonly int bouncesSum;

        public float GetValue(float t)
        {
            float sum = (1 - t) * bouncesSum;
            int segment = bounces - (int)((-1f + Mathf.Sqrt(1f + (8f * sum))) / 2f) - 1;
            if (segment < 0)
            {
                segment = 0;
            }

            float time = times[segment];
            float root = ((2 * t) - time - (2 * startTimes[segment])) / time;
            return peaks[segment] * (-(root * root) + 1);
        }
    }
}
