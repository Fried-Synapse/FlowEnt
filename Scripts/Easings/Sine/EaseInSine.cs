using System;

namespace FlowEnt
{
    public class EaseInSine : IEasing
    {
        public float GetValue(float t)
            => (float)(1 - Math.Cos((t * Math.PI) / 2));
    }
}