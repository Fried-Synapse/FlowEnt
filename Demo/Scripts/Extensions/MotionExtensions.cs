using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    public static class TransformMotionExtensions
    {
        public static TweenMotion<MeshRenderer> LateAlphaTo(this TweenMotion<MeshRenderer> motion, float value, float percentage)
            => motion.Apply(new LateAlphaToMotion(motion.Item, value, percentage));
    }
}