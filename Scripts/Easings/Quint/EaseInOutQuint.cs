using System;

namespace FriedSynapse.FlowEnt
{
    public class EaseInOutQuint : IEasing
    {
        public float GetValue(float t)
            => (float)(t < 0.5f ? 16 * t * t * t * t * t : 1 - Math.Pow(-2 * t + 2, 5) / 2);
    }
}