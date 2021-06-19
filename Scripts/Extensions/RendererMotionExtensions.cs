using UnityEngine;
using FlowEnt.Motions.RendererMotions;

namespace FlowEnt
{
    public static class RendererMotionExtensions
    {
        public static MotionWrapper<TRenderer> Alpha<TRenderer>(this MotionWrapper<TRenderer> motion, float value)
            where TRenderer : Renderer
            => motion.Apply(new AlphaMotion<TRenderer>(motion.Item, value));

        public static MotionWrapper<TRenderer> AlphaTo<TRenderer>(this MotionWrapper<TRenderer> motion, float to)
            where TRenderer : Renderer
            => motion.Apply(new AlphaToMotion<TRenderer>(motion.Item, to));

        public static MotionWrapper<TRenderer> AlphaTo<TRenderer>(this MotionWrapper<TRenderer> motion, float from, float to)
            where TRenderer : Renderer
            => motion.Apply(new AlphaToMotion<TRenderer>(motion.Item, from, to));

        public static MotionWrapper<TRenderer> Color<TRenderer>(this MotionWrapper<TRenderer> motion, Color value)
            where TRenderer : Renderer
            => motion.Apply(new ColorMotion<TRenderer>(motion.Item, value));

        public static MotionWrapper<TRenderer> ColorTo<TRenderer>(this MotionWrapper<TRenderer> motion, Color to)
            where TRenderer : Renderer
            => motion.Apply(new ColorToMotion<TRenderer>(motion.Item, to));

        public static MotionWrapper<TRenderer> ColorTo<TRenderer>(this MotionWrapper<TRenderer> motion, Color from, Color to)
            where TRenderer : Renderer
            => motion.Apply(new ColorToMotion<TRenderer>(motion.Item, from, to));
    }
}