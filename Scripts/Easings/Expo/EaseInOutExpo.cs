using System;

namespace FriedSynapse.FlowEnt
{
    public class EaseInOutExpo : IEasing
    {
        public float GetValue(float t)
            => t == 0
                  ? 0f
                  : t == 1
                      ? 1f
                      : t < 0.5
                            ? (float)(Math.Pow(2, (20 * t) - 10) / 2)
                            : (float)((2 - Math.Pow(2, (-20 * t) + 10)) / 2);
    }
}