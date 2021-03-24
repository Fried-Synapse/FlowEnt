using System;

namespace FlowEnt
{
    public class EaseOutSine : IEasing
    {
        public float GetValue(float t)
            => (float)Math.Sin((t * Math.PI) / 2);
    }
}