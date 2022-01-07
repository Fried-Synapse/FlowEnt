using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    public static class TransformMotionExtensions
    {
        public static TweenMotionProxy<Material> LateAlphaTo(this TweenMotionProxy<Material> motion, float value, float percentage)
            => motion.Apply(new LateAlphaMotion(motion.Item, value, percentage));
    }
}