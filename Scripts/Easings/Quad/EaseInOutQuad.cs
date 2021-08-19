using System;

namespace FriedSynapse.FlowEnt
{
    public class EaseInOutQuad : IEasing
    {
        public float GetValue(float t)
            => (float)(t < 0.5 ? 2 * t * t : 1 - Math.Pow(-2 * t + 2, 2) / 2);
    }
}