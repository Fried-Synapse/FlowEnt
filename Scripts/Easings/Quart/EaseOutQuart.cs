using System;

namespace FlowEnt
{
    public class EaseOutQuart : IEasing
    {
        public float GetValue(float t)
            => (float)(1 - Math.Pow(1 - t, 4));
    }
}