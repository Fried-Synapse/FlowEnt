using System;

namespace FriedSynapse.FlowEnt
{
    public class EaseInOutQuart : IEasing
    {
        public float GetValue(float t)
            => (float)(t < 0.5f ? 8 * t * t * t * t : 1 - (Math.Pow((-2 * t) + 2, 4) / 2));
    }
}