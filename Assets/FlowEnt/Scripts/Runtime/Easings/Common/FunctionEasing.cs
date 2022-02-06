using System;

namespace FriedSynapse.FlowEnt.Easings
{
    public class FunctionEasing : IEasing
    {
        public FunctionEasing(Func<float, float> getValueFunc)
        {
            GetValueFunc = getValueFunc;
        }

        private Func<float, float> GetValueFunc { get; }

        public float GetValue(float t)
            => GetValueFunc(t);
    }
}
