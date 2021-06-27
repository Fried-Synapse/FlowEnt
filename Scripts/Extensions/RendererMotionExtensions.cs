using UnityEngine;
using FlowEnt.Motions.RendererMotions;

namespace FlowEnt
{
    public static class RendererMotionExtensions
    {
        #region Alpha

        public static TweenMotion<TRenderer> Alpha<TRenderer>(this TweenMotion<TRenderer> motion, float value)
            where TRenderer : Renderer
            => motion.Apply(new AlphaMotion<TRenderer>(motion.Item, value));

        public static TweenMotion<TRenderer> AlphaTo<TRenderer>(this TweenMotion<TRenderer> motion, float to)
            where TRenderer : Renderer
            => motion.Apply(new AlphaToMotion<TRenderer>(motion.Item, to));

        public static TweenMotion<TRenderer> AlphaTo<TRenderer>(this TweenMotion<TRenderer> motion, float from, float to)
            where TRenderer : Renderer
            => motion.Apply(new AlphaToMotion<TRenderer>(motion.Item, from, to));

        #endregion

        #region Color

        public static TweenMotion<TRenderer> Color<TRenderer>(this TweenMotion<TRenderer> motion, Color value)
            where TRenderer : Renderer
            => motion.Apply(new ColorMotion<TRenderer>(motion.Item, value));

        public static TweenMotion<TRenderer> ColorTo<TRenderer>(this TweenMotion<TRenderer> motion, Color to)
            where TRenderer : Renderer
            => motion.Apply(new ColorToMotion<TRenderer>(motion.Item, to));

        public static TweenMotion<TRenderer> ColorTo<TRenderer>(this TweenMotion<TRenderer> motion, Color from, Color to)
            where TRenderer : Renderer
            => motion.Apply(new ColorToMotion<TRenderer>(motion.Item, from, to));

        #endregion

    }
}