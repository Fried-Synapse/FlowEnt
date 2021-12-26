using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Renderers;

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
        public static TweenMotion<TRenderer> Alpha<TRenderer>(this TweenMotion<TRenderer> tweenMotion, float value)
            where TRenderer : Renderer
            => tweenMotion.Apply(new AlphaMotion<TRenderer>(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="AlphaMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotion<TRenderer> AlphaTo<TRenderer>(this TweenMotion<TRenderer> tweenMotion, float to)
            where TRenderer : Renderer
            => tweenMotion.Apply(new AlphaMotion<TRenderer>(tweenMotion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="AlphaMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotion<TRenderer> AlphaTo<TRenderer>(this TweenMotion<TRenderer> tweenMotion, float from, float to)
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
        public static TweenMotion<TRenderer> Color<TRenderer>(this TweenMotion<TRenderer> tweenMotion, Color value)
            where TRenderer : Renderer
            => tweenMotion.Apply(new ColorMotion<TRenderer>(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="ColorMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotion<TRenderer> ColorTo<TRenderer>(this TweenMotion<TRenderer> tweenMotion, Color to)
            where TRenderer : Renderer
            => tweenMotion.Apply(new ColorMotion<TRenderer>(tweenMotion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="ColorMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotion<TRenderer> ColorTo<TRenderer>(this TweenMotion<TRenderer> tweenMotion, Color from, Color to)
            where TRenderer : Renderer
            => tweenMotion.Apply(new ColorMotion<TRenderer>(tweenMotion.Item, from, to));

        /// <summary>
        /// Applies a <see cref="ColorGradientMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="gradient"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotion<TRenderer> ColorTo<TRenderer>(this TweenMotion<TRenderer> tweenMotion, Gradient gradient)
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
        public static TweenMotion<TRenderer> MaterialFloat<TRenderer>(this TweenMotion<TRenderer> tweenMotion, string propertyName, float value)
            where TRenderer : Renderer
            => tweenMotion.Apply(new MaterialFloatMotion<TRenderer>(tweenMotion.Item, propertyName, value));

        /// <summary>
        /// Applies a <see cref="MaterialFloatMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="propertyName"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotion<TRenderer> MaterialFloatTo<TRenderer>(this TweenMotion<TRenderer> tweenMotion, string propertyName, float to)
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
        public static TweenMotion<TRenderer> MaterialFloatTo<TRenderer>(this TweenMotion<TRenderer> tweenMotion, string propertyName, float from, float to)
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
        public static TweenMotion<TRenderer> MaterialAlpha<TRenderer>(this TweenMotion<TRenderer> tweenMotion, string propertyName, float value)
            where TRenderer : Renderer
            => tweenMotion.Apply(new MaterialAlphaMotion<TRenderer>(tweenMotion.Item, propertyName, value));

        /// <summary>
        /// Applies a <see cref="MaterialAlphaMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="propertyName"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotion<TRenderer> MaterialAlphaTo<TRenderer>(this TweenMotion<TRenderer> tweenMotion, string propertyName, float to)
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
        public static TweenMotion<TRenderer> MaterialAlphaTo<TRenderer>(this TweenMotion<TRenderer> tweenMotion, string propertyName, float from, float to)
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
        public static TweenMotion<TRenderer> MaterialColor<TRenderer>(this TweenMotion<TRenderer> tweenMotion, string propertyName, Color value)
            where TRenderer : Renderer
            => tweenMotion.Apply(new MaterialColorMotion<TRenderer>(tweenMotion.Item, propertyName, value));

        /// <summary>
        /// Applies a <see cref="MaterialColorMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="propertyName"></param>
        /// <param name="to"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotion<TRenderer> MaterialColorTo<TRenderer>(this TweenMotion<TRenderer> tweenMotion, string propertyName, Color to)
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
        public static TweenMotion<TRenderer> MaterialColorTo<TRenderer>(this TweenMotion<TRenderer> tweenMotion, string propertyName, Color from, Color to)
            where TRenderer : Renderer
            => tweenMotion.Apply(new MaterialColorMotion<TRenderer>(tweenMotion.Item, propertyName, from, to));

        /// <summary>
        /// Applies a <see cref="MaterialColorGradientMotion{TRenderer}" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="propertyName"></param>
        /// <param name="gradient"></param>
        /// <typeparam name="TRenderer"></typeparam>
        public static TweenMotion<TRenderer> MaterialColorTo<TRenderer>(this TweenMotion<TRenderer> tweenMotion, string propertyName, Gradient gradient)
            where TRenderer : Renderer
            => tweenMotion.Apply(new MaterialColorGradientMotion<TRenderer>(tweenMotion.Item, propertyName, gradient));

        #endregion
    }
}