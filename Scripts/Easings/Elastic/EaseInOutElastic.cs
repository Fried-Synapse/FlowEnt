using System;

namespace FriedSynapse.FlowEnt.Easings
{
    public class EaseInOutElastic : IEasing
    {
        private const float C5 = (float)((2f * Math.PI) / 4.5f);

        public float GetValue(float t)
            => t == 0
                  ? 0f
                  : t == 1
                      ? 1f
                      : t < 0.5f
                          ? (float)(-(Math.Pow(2, (20 * t) - 10) * Math.Sin(((20 * t) - 11.125) * C5)) / 2)
                          : ((float)(Math.Pow(2, (-20 * t) + 10) * Math.Sin(((20 * t) - 11.125) * C5)) / 2) + 1;
    }
}