using System;

namespace FriedSynapse.FlowEnt
{
    public class EaseInElastic : IEasing
    {
        private const float C4 = (float)((2 * Math.PI) / 3);

        public float GetValue(float t)
            => t == 0
                  ? 0f
                  : t == 1
                      ? 1f
                      : (float)(-Math.Pow(2, (10 * t) - 10) * Math.Sin(((t * 10) - 10.75) * C4));
    }
}