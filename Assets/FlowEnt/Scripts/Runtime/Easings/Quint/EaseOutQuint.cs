using System;

namespace FriedSynapse.FlowEnt.Easings
{
    public class EaseOutQuint : IEasing
    {
        public float GetValue(float t)
            => (float)(1 - Math.Pow(1 - t, 5));
    }
}