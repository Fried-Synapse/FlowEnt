using System;

namespace FlowEnt
{
    public class EaseInOutCubic : IEasing
    {
        public float GetValue(float t)
            => (float)(t < 0.5 ? 4 * t * t * t : 1 - Math.Pow(-2 * t + 2, 3) / 2);
    }
}