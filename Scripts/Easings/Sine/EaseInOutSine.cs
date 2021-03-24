using System;

namespace FlowEnt
{
    public class EaseInOutSine : IEasing
    {
        public float GetValue(float t)
            => (float)-(Math.Cos(Math.PI * t) - 1) / 2;
    }
}