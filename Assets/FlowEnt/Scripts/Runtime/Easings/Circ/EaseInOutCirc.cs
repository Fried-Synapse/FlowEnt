using System;

namespace FriedSynapse.FlowEnt.Easings
{
    public class EaseInOutCirc : IEasing
    {
        public float GetValue(float t)
            => t < 0.5f
                  ? (float)((1 - Math.Sqrt(1 - Math.Pow(2 * t, 2))) / 2)
                  : (float)(Math.Sqrt(1 - Math.Pow((-2 * t) + 2, 2)) + 1) / 2;
    }
}