using System;

namespace FriedSynapse.FlowEnt
{
    public class EaseInCirc : IEasing
    {
        public float GetValue(float t)
            => (float)(1 - Math.Sqrt(1 - Math.Pow(t, 2)));
    }
}