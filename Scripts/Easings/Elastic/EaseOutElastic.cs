using System;

namespace FriedSynapse.FlowEnt
{
    public class EaseOutElastic : IEasing
    {
        private const float C4 = (float)((2 * Math.PI) / 3);

        public float GetValue(float t)
            => t == 0
                  ? 0f
                  : t == 1
                      ? 1f
                      : (float)(Math.Pow(2, -10 * t) * Math.Sin((t * 10 - 0.75) * C4) + 1);
    }
}