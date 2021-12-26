using FriedSynapse.FlowEnt.Motions.UI.Graphics;
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
        /// <param name="motion"></param>
        /// <param name="value"></param>
        /// <typeparam name="TGraphic"></typeparam>
        public static TweenMotion<TGraphic> Alpha<TGraphic>(this TweenMotion<TGraphic> motion, float value)
            where TGraphic : Graphic
            => motion.Apply(new AlphaMotion<TGraphic>(motion.Item, value));

        /// <summary>
        /// Applies a <see cref="AlphaMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="to"></param>
        /// <typeparam name="TGraphic"></typeparam>
        public static TweenMotion<TGraphic> AlphaTo<TGraphic>(this TweenMotion<TGraphic> motion, float to)
            where TGraphic : Graphic
            => motion.Apply(new AlphaMotion<TGraphic>(motion.Item, default, to));

        /// <summary>
        /// Applies a <see cref="AlphaMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TGraphic"></typeparam>
        public static TweenMotion<TGraphic> AlphaTo<TGraphic>(this TweenMotion<TGraphic> motion, float from, float to)
            where TGraphic : Graphic
            => motion.Apply(new AlphaMotion<TGraphic>(motion.Item, from, to));

        #endregion

        #region Color

        /// <summary>
        /// Applies a <see cref="ColorMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="value"></param>
        /// <typeparam name="TGraphic"></typeparam>
        public static TweenMotion<TGraphic> Color<TGraphic>(this TweenMotion<TGraphic> motion, Color value)
            where TGraphic : Graphic
            => motion.Apply(new ColorMotion<TGraphic>(motion.Item, value));

        /// <summary>
        /// Applies a <see cref="ColorMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="to"></param>
        /// <typeparam name="TGraphic"></typeparam>
        public static TweenMotion<TGraphic> ColorTo<TGraphic>(this TweenMotion<TGraphic> motion, Color to)
            where TGraphic : Graphic
            => motion.Apply(new ColorMotion<TGraphic>(motion.Item, default, to));

        /// <summary>
        /// Applies a <see cref="ColorMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TGraphic"></typeparam>
        public static TweenMotion<TGraphic> ColorTo<TGraphic>(this TweenMotion<TGraphic> motion, Color from, Color to)
            where TGraphic : Graphic
            => motion.Apply(new ColorMotion<TGraphic>(motion.Item, from, to));

        /// <summary>
        /// Applies a <see cref="ColorGradientMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="gradient"></param>
        /// <typeparam name="TGraphic"></typeparam>
        public static TweenMotion<TGraphic> ColorTo<TGraphic>(this TweenMotion<TGraphic> motion, Gradient gradient)
            where TGraphic : Graphic
            => motion.Apply(new ColorGradientMotion<TGraphic>(motion.Item, gradient));

        #endregion
    }
}
