using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    public static class TransformMotionExtensions
    {
        public static TweenMotionProxy<Material> LateAlphaTo(this TweenMotionProxy<Material> motion, float to, float percentage)
            => motion.Apply(new LateAlphaMotion(motion.Item, to, percentage));
    }
}