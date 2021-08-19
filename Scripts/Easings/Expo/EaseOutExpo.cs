using System;

namespace FriedSynapse.FlowEnt
{
    public class EaseOutExpo : IEasing
    {
        public float GetValue(float t)
            => (float)(t == 1 ? 1 : 1 - Math.Pow(2, -10 * t));
    }
}