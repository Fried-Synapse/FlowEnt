using System;

namespace FriedSynapse.FlowEnt
{
    public class EaseOutQuint : IEasing
    {
        public float GetValue(float t)
            => (float)(1 - Math.Pow(1 - t, 5));
    }
}