using System;

namespace FriedSynapse.FlowEnt.Easings
{
    public class EaseOutBack : IEasing
    {
        private const float C1 = 1.70158f;
        private const float C3 = C1 + 1;

        public float GetValue(float t)
            => (float)((float)1 + (C3 * Math.Pow(t - 1, 3)) + (C1 * Math.Pow(t - 1, 2)));
    }
}