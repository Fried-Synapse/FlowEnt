using System;

namespace FriedSynapse.FlowEnt.Easings
{
    public class EaseInSine : IEasing
    {
        public float GetValue(float t)
            => (float)(1 - Math.Cos((t * Math.PI) / 2));
    }
}