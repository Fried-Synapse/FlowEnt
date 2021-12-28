using FriedSynapse.FlowEnt.Motions.Tween.UI.Graphics;
using UnityEngine;
using UnityEngine.UI;

namespace FriedSynapse.FlowEnt
{
    public static class GraphicMotionExtensions
    {
        #region Alpha

        /// <summary>
        /// Applies a <see cref="AlphaMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TGraphic"></typeparam>
        public static TweenMotionProxy<TGraphic> Alpha<TGraphic>(this TweenMotionProxy<TGraphic> proxy, float value)
            where TGraphic : Graphic
            => proxy.Apply(new AlphaMotion<TGraphic>(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="AlphaMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TGraphic"></typeparam>
        public static TweenMotionProxy<TGraphic> AlphaTo<TGraphic>(this TweenMotionProxy<TGraphic> proxy, float to)
            where TGraphic : Graphic
            => proxy.Apply(new AlphaMotion<TGraphic>(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="AlphaMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TGraphic"></typeparam>
        public static TweenMotionProxy<TGraphic> AlphaTo<TGraphic>(this TweenMotionProxy<TGraphic> proxy, float from, float to)
            where TGraphic : Graphic
            => proxy.Apply(new AlphaMotion<TGraphic>(proxy.Item, from, to));

        #endregion

        #region Color

        /// <summary>
        /// Applies a <see cref="ColorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TGraphic"></typeparam>
        public static TweenMotionProxy<TGraphic> Color<TGraphic>(this TweenMotionProxy<TGraphic> proxy, Color value)
            where TGraphic : Graphic
            => proxy.Apply(new ColorMotion<TGraphic>(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="ColorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TGraphic"></typeparam>
        public static TweenMotionProxy<TGraphic> ColorTo<TGraphic>(this TweenMotionProxy<TGraphic> proxy, Color to)
            where TGraphic : Graphic
            => proxy.Apply(new ColorMotion<TGraphic>(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="ColorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TGraphic"></typeparam>
        public static TweenMotionProxy<TGraphic> ColorTo<TGraphic>(this TweenMotionProxy<TGraphic> proxy, Color from, Color to)
            where TGraphic : Graphic
            => proxy.Apply(new ColorMotion<TGraphic>(proxy.Item, from, to));

        /// <summary>
        /// Applies a <see cref="ColorGradientMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="gradient"></param>
        /// <typeparam name="TGraphic"></typeparam>
        public static TweenMotionProxy<TGraphic> ColorTo<TGraphic>(this TweenMotionProxy<TGraphic> proxy, Gradient gradient)
            where TGraphic : Graphic
            => proxy.Apply(new ColorGradientMotion<TGraphic>(proxy.Item, gradient));

        #endregion
    }
}
