using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    public static class TransformMotionExtensions
    {
        public static TweenMotionProxy<MeshRenderer> LateAlphaTo(this TweenMotionProxy<MeshRenderer> motion, float value, float percentage)
            => motion.Apply(new LateAlphaMotion(motion.Item, value, percentage));
    }
}