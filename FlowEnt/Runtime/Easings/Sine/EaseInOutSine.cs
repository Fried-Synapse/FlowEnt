using System;

namespace FriedSynapse.FlowEnt.Easings
{
    public class EaseInOutSine : IEasing
    {
        public float GetValue(float t)
            => (float)-(Math.Cos(Math.PI * t) - 1) / 2;
    }
}