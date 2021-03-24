using System;

namespace FlowEnt
{
    public class EaseInOutBack : IEasing
    {
        private const float C1 = 1.70158f;
        private const float C2 = C1 * 1.525f;

        public float GetValue(float t)
            => t < 0.5f
                  ? (float)(Math.Pow(2 * t, 2) * ((C2 + 1) * 2 * t - C2)) / 2
                  : (float)(Math.Pow(2 * t - 2, 2) * ((C2 + 1) * (t * 2 - 2) + C2) + 2) / 2;
    }
}