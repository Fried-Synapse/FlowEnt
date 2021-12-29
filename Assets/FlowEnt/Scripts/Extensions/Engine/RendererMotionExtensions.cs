using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Tween.Renderers;

namespace FriedSynapse.FlowEnt
{
    public static class RendererMotionExtensions
    {
        #region Alpha

        /// <summary>
        /// Applies a <see cref="AlphaMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> Alpha<TRenderer>(this TweenMotionProxy<TRenderer> proxy, float value)
            where TRenderer : Renderer
            => proxy.Apply(new AlphaMotion<TRenderer>(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="AlphaMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> AlphaTo<TRenderer>(this TweenMotionProxy<TRenderer> proxy, float to)
            where TRenderer : Renderer
            => proxy.Apply(new AlphaMotion<TRenderer>(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="AlphaMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> AlphaTo<TRenderer>(this TweenMotionProxy<TRenderer> proxy, float from, float to)
            where TRenderer : Renderer
            => proxy.Apply(new AlphaMotion<TRenderer>(proxy.Item, from, to));

        #endregion

        #region Color

        /// <summary>
        /// Applies a <see cref="ColorMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> Color<TRenderer>(this TweenMotionProxy<TRenderer> proxy, Color value)
            where TRenderer : Renderer
            => proxy.Apply(new ColorMotion<TRenderer>(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="ColorMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> ColorTo<TRenderer>(this TweenMotionProxy<TRenderer> proxy, Color to)
            where TRenderer : Renderer
            => proxy.Apply(new ColorMotion<TRenderer>(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="ColorMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> ColorTo<TRenderer>(this TweenMotionProxy<TRenderer> proxy, Color from, Color to)
            where TRenderer : Renderer
            => proxy.Apply(new ColorMotion<TRenderer>(proxy.Item, from, to));

        /// <summary>
        /// Applies a <see cref="ColorGradientMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="gradient"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> ColorTo<TRenderer>(this TweenMotionProxy<TRenderer> proxy, Gradient gradient)
            where TRenderer : Renderer
            => proxy.Apply(new ColorGradientMotion<TRenderer>(proxy.Item, gradient));

        #endregion

        #region Material Float

        /// <summary>
        /// Applies a <see cref="MaterialFloatMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialFloat<TRenderer>(this TweenMotionProxy<TRenderer> proxy, string propertyName, float value)
            where TRenderer : Renderer
            => proxy.Apply(new MaterialFloatMotion<TRenderer>(proxy.Item, propertyName, value));

        /// <summary>
        /// Applies a <see cref="MaterialFloatMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialFloatTo<TRenderer>(this TweenMotionProxy<TRenderer> proxy, string propertyName, float to)
            where TRenderer : Renderer
            => proxy.Apply(new MaterialFloatMotion<TRenderer>(proxy.Item, propertyName, default, to));

        /// <summary>
        /// Applies a <see cref="MaterialFloatMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialFloatTo<TRenderer>(this TweenMotionProxy<TRenderer> proxy, string propertyName, float from, float to)
            where TRenderer : Renderer
            => proxy.Apply(new MaterialFloatMotion<TRenderer>(proxy.Item, propertyName, from, to));

        #endregion

        #region Material Alpha

        /// <summary>
        /// Applies a <see cref="MaterialAlphaMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialAlpha<TRenderer>(this TweenMotionProxy<TRenderer> proxy, string propertyName, float value)
            where TRenderer : Renderer
            => proxy.Apply(new MaterialAlphaMotion<TRenderer>(proxy.Item, propertyName, value));

        /// <summary>
        /// Applies a <see cref="MaterialAlphaMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialAlphaTo<TRenderer>(this TweenMotionProxy<TRenderer> proxy, string propertyName, float to)
            where TRenderer : Renderer
            => proxy.Apply(new MaterialAlphaMotion<TRenderer>(proxy.Item, propertyName, default, to));

        /// <summary>
        /// Applies a <see cref="MaterialAlphaMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialAlphaTo<TRenderer>(this TweenMotionProxy<TRenderer> proxy, string propertyName, float from, float to)
            where TRenderer : Renderer
            => proxy.Apply(new MaterialAlphaMotion<TRenderer>(proxy.Item, propertyName, from, to));

        #endregion

        #region Material Color

        /// <summary>
        /// Applies a <see cref="MaterialColorMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialColor<TRenderer>(this TweenMotionProxy<TRenderer> proxy, string propertyName, Color value)
            where TRenderer : Renderer
            => proxy.Apply(new MaterialColorMotion<TRenderer>(proxy.Item, propertyName, value));

        /// <summary>
        /// Applies a <see cref="MaterialColorMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialColorTo<TRenderer>(this TweenMotionProxy<TRenderer> proxy, string propertyName, Color to)
            where TRenderer : Renderer
            => proxy.Apply(new MaterialColorMotion<TRenderer>(proxy.Item, propertyName, default, to));

        /// <summary>
        /// Applies a <see cref="MaterialColorMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialColorTo<TRenderer>(this TweenMotionProxy<TRenderer> proxy, string propertyName, Color from, Color to)
            where TRenderer : Renderer
            => proxy.Apply(new MaterialColorMotion<TRenderer>(proxy.Item, propertyName, from, to));

        /// <summary>
        /// Applies a <see cref="MaterialColorGradientMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="propertyName"></param>
        /// <param name="gradient"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialColorTo<TRenderer>(this TweenMotionProxy<TRenderer> proxy, string propertyName, Gradient gradient)
            where TRenderer : Renderer
            => proxy.Apply(new MaterialColorGradientMotion<TRenderer>(proxy.Item, propertyName, gradient));

        #endregion
    }
}