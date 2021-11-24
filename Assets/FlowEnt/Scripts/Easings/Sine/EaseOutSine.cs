using System;

namespace FriedSynapse.FlowEnt.Easings
{
    public class EaseOutSine : IEasing
    {
        public float GetValue(float t)
            => (float)Math.Sin((t * Math.PI) / 2);
    }
}