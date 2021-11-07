using UnityEngine;

namespace FriedSynapse.FlowEnt.Easings
{
    public class AnimationCurveEasing : IEasing
    {
        public AnimationCurveEasing(AnimationCurve animationCurve)
        {
            AnimationCurve = animationCurve;
        }

        private AnimationCurve AnimationCurve { get; }

        public float GetValue(float t)
            => AnimationCurve.Evaluate(t);
    }
}
