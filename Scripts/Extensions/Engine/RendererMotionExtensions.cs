using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Renderers;

namespace FriedSynapse.FlowEnt
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

        /// <summary>
        /// Extension method that will tween a renderers color using a supplied gradient value that will be evaluated
        /// on each update loop of the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TweenMotion<TRenderer> ColorTo<TRenderer>(this TweenMotion<TRenderer> motion, Gradient gradient)
            where TRenderer : Renderer
            => motion.Apply(new ColorToGradientMotion<TRenderer>(motion.Item, gradient));

        #endregion

        #region Material Float

        public static TweenMotion<TRenderer> MaterialFloat<TRenderer>(this TweenMotion<TRenderer> motion, string propertyName, float value)
            where TRenderer : Renderer
            => motion.Apply(new MaterialFloatMotion<TRenderer>(motion.Item, propertyName, value));

        public static TweenMotion<TRenderer> MaterialFloatTo<TRenderer>(this TweenMotion<TRenderer> motion, string propertyName, float to)
            where TRenderer : Renderer
            => motion.Apply(new MaterialFloatToMotion<TRenderer>(motion.Item, propertyName, to));

        public static TweenMotion<TRenderer> MaterialFloatTo<TRenderer>(this TweenMotion<TRenderer> motion, string propertyName, float from, float to)
            where TRenderer : Renderer
            => motion.Apply(new MaterialFloatToMotion<TRenderer>(motion.Item, propertyName, from, to));

        #endregion

        #region Material Alpha

        public static TweenMotion<TRenderer> MaterialAlpha<TRenderer>(this TweenMotion<TRenderer> motion, string propertyName, float value)
            where TRenderer : Renderer
            => motion.Apply(new MaterialAlphaMotion<TRenderer>(motion.Item, propertyName, value));

        public static TweenMotion<TRenderer> MaterialAlphaTo<TRenderer>(this TweenMotion<TRenderer> motion, string propertyName, float to)
            where TRenderer : Renderer
            => motion.Apply(new MaterialAlphaToMotion<TRenderer>(motion.Item, propertyName, to));

        public static TweenMotion<TRenderer> MaterialAlphaTo<TRenderer>(this TweenMotion<TRenderer> motion, string propertyName, float from, float to)
            where TRenderer : Renderer
            => motion.Apply(new MaterialAlphaToMotion<TRenderer>(motion.Item, propertyName, from, to));

        #endregion

        #region Material Color

        public static TweenMotion<TRenderer> MaterialColor<TRenderer>(this TweenMotion<TRenderer> motion, string propertyName, Color value)
            where TRenderer : Renderer
            => motion.Apply(new MaterialColorMotion<TRenderer>(motion.Item, propertyName, value));

        public static TweenMotion<TRenderer> MaterialColorTo<TRenderer>(this TweenMotion<TRenderer> motion, string propertyName, Color to)
            where TRenderer : Renderer
            => motion.Apply(new MaterialColorToMotion<TRenderer>(motion.Item, propertyName, to));

        public static TweenMotion<TRenderer> MaterialColorTo<TRenderer>(this TweenMotion<TRenderer> motion, string propertyName, Color from, Color to)
            where TRenderer : Renderer
            => motion.Apply(new MaterialColorToMotion<TRenderer>(motion.Item, propertyName, from, to));

        /// <summary>
        /// Extension method that will tween a renderers color using a supplied gradient value which will be evaluated
        /// on each update loop of the tween and a material property name that will be used to access the materials color value.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TweenMotion<TRenderer> MaterialColorTo<TRenderer>(this TweenMotion<TRenderer> motion, string propertyName, Gradient gradient)
            where TRenderer : Renderer
            => motion.Apply(new MaterialColorToGradientMotion<TRenderer>(motion.Item, propertyName, gradient
        #endregion
    }
}