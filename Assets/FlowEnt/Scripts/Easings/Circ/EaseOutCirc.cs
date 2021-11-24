using System;

namespace FriedSynapse.FlowEnt.Easings
{
    public class EaseOutCirc : IEasing
    {
        public float GetValue(float t)
            => (float)Math.Sqrt(1 - Math.Pow(t - 1, 2));
    }
}