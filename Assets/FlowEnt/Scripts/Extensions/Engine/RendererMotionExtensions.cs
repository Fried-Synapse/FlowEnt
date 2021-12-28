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
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> Alpha<TRenderer>(this TweenMotionProxy<TRenderer> tweenMotion, float value)
            where TRenderer : Renderer
            => tweenMotion.Apply(new AlphaMotion<TRenderer>(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="AlphaMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> AlphaTo<TRenderer>(this TweenMotionProxy<TRenderer> tweenMotion, float to)
            where TRenderer : Renderer
            => tweenMotion.Apply(new AlphaMotion<TRenderer>(tweenMotion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="AlphaMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> AlphaTo<TRenderer>(this TweenMotionProxy<TRenderer> tweenMotion, float from, float to)
            where TRenderer : Renderer
            => tweenMotion.Apply(new AlphaMotion<TRenderer>(tweenMotion.Item, from, to));

        #endregion

        #region Color

        /// <summary>
        /// Applies a <see cref="ColorMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> Color<TRenderer>(this TweenMotionProxy<TRenderer> tweenMotion, Color value)
            where TRenderer : Renderer
            => tweenMotion.Apply(new ColorMotion<TRenderer>(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="ColorMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> ColorTo<TRenderer>(this TweenMotionProxy<TRenderer> tweenMotion, Color to)
            where TRenderer : Renderer
            => tweenMotion.Apply(new ColorMotion<TRenderer>(tweenMotion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="ColorMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> ColorTo<TRenderer>(this TweenMotionProxy<TRenderer> tweenMotion, Color from, Color to)
            where TRenderer : Renderer
            => tweenMotion.Apply(new ColorMotion<TRenderer>(tweenMotion.Item, from, to));

        /// <summary>
        /// Applies a <see cref="ColorGradientMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="gradient"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> ColorTo<TRenderer>(this TweenMotionProxy<TRenderer> tweenMotion, Gradient gradient)
            where TRenderer : Renderer
            => tweenMotion.Apply(new ColorGradientMotion<TRenderer>(tweenMotion.Item, gradient));

        #endregion

        #region Material Float

        /// <summary>
        /// Applies a <see cref="MaterialFloatMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialFloat<TRenderer>(this TweenMotionProxy<TRenderer> tweenMotion, string propertyName, float value)
            where TRenderer : Renderer
            => tweenMotion.Apply(new MaterialFloatMotion<TRenderer>(tweenMotion.Item, propertyName, value));

        /// <summary>
        /// Applies a <see cref="MaterialFloatMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="propertyName"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialFloatTo<TRenderer>(this TweenMotionProxy<TRenderer> tweenMotion, string propertyName, float to)
            where TRenderer : Renderer
            => tweenMotion.Apply(new MaterialFloatMotion<TRenderer>(tweenMotion.Item, propertyName, null, to));

        /// <summary>
        /// Applies a <see cref="MaterialFloatMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="propertyName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialFloatTo<TRenderer>(this TweenMotionProxy<TRenderer> tweenMotion, string propertyName, float from, float to)
            where TRenderer : Renderer
            => tweenMotion.Apply(new MaterialFloatMotion<TRenderer>(tweenMotion.Item, propertyName, from, to));

        #endregion

        #region Material Alpha

        /// <summary>
        /// Applies a <see cref="MaterialAlphaMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialAlpha<TRenderer>(this TweenMotionProxy<TRenderer> tweenMotion, string propertyName, float value)
            where TRenderer : Renderer
            => tweenMotion.Apply(new MaterialAlphaMotion<TRenderer>(tweenMotion.Item, propertyName, value));

        /// <summary>
        /// Applies a <see cref="MaterialAlphaMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="propertyName"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialAlphaTo<TRenderer>(this TweenMotionProxy<TRenderer> tweenMotion, string propertyName, float to)
            where TRenderer : Renderer
            => tweenMotion.Apply(new MaterialAlphaMotion<TRenderer>(tweenMotion.Item, propertyName, null, to));

        /// <summary>
        /// Applies a <see cref="MaterialAlphaMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="propertyName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialAlphaTo<TRenderer>(this TweenMotionProxy<TRenderer> tweenMotion, string propertyName, float from, float to)
            where TRenderer : Renderer
            => tweenMotion.Apply(new MaterialAlphaMotion<TRenderer>(tweenMotion.Item, propertyName, from, to));

        #endregion

        #region Material Color

        /// <summary>
        /// Applies a <see cref="MaterialColorMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialColor<TRenderer>(this TweenMotionProxy<TRenderer> tweenMotion, string propertyName, Color value)
            where TRenderer : Renderer
            => tweenMotion.Apply(new MaterialColorMotion<TRenderer>(tweenMotion.Item, propertyName, value));

        /// <summary>
        /// Applies a <see cref="MaterialColorMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="propertyName"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialColorTo<TRenderer>(this TweenMotionProxy<TRenderer> tweenMotion, string propertyName, Color to)
            where TRenderer : Renderer
            => tweenMotion.Apply(new MaterialColorMotion<TRenderer>(tweenMotion.Item, propertyName, null, to));

        /// <summary>
        /// Applies a <see cref="MaterialColorMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="propertyName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialColorTo<TRenderer>(this TweenMotionProxy<TRenderer> tweenMotion, string propertyName, Color from, Color to)
            where TRenderer : Renderer
            => tweenMotion.Apply(new MaterialColorMotion<TRenderer>(tweenMotion.Item, propertyName, from, to));

        /// <summary>
        /// Applies a <see cref="MaterialColorGradientMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="propertyName"></param>
        /// <param name="gradient"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotionProxy<TRenderer> MaterialColorTo<TRenderer>(this TweenMotionProxy<TRenderer> tweenMotion, string propertyName, Gradient gradient)
            where TRenderer : Renderer
            => tweenMotion.Apply(new MaterialColorGradientMotion<TRenderer>(tweenMotion.Item, propertyName, gradient));

        #endregion
    }
}