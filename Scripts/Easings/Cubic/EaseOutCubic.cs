using System;

namespace FlowEnt
{
    public class EaseOutCubic : IEasing
    {
        public float GetValue(float t)
            => (float)(1 - Math.Pow(1 - t, 3));
    }
}