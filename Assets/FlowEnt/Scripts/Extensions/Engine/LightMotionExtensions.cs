using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Lights;

namespace FriedSynapse.FlowEnt
{
    public static class LightMotionExtensions
    {
        #region Intensity

        /// <summary>
        /// Applies a <see cref="IntensityMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotion<Light> Intensity(this TweenMotion<Light> tweenMotion, float value)
            => tweenMotion.Apply(new IntensityMotion(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="IntensityMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotion<Light> IntensityTo(this TweenMotion<Light> tweenMotion, float to)
            => tweenMotion.Apply(new IntensityMotion(tweenMotion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="IntensityMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotion<Light> IntensityTo(this TweenMotion<Light> tweenMotion, float from, float to)
            => tweenMotion.Apply(new IntensityMotion(tweenMotion.Item, from, to));

        #endregion

        #region Color

        /// <summary>
        /// Applies a <see cref="ColorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotion<Light> Color(this TweenMotion<Light> tweenMotion, Color value)
            => tweenMotion.Apply(new ColorMotion(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="ColorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotion<Light> ColorTo(this TweenMotion<Light> tweenMotion, Color to)
            => tweenMotion.Apply(new ColorMotion(tweenMotion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="ColorMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotion<Light> ColorTo(this TweenMotion<Light> tweenMotion, Color from, Color to)
            => tweenMotion.Apply(new ColorMotion(tweenMotion.Item, from, to));

        /// <summary>
        /// Applies a <see cref="ColorGradientMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="gradient"></param>
        public static TweenMotion<Light> ColorTo(this TweenMotion<Light> tweenMotion, Gradient gradient)
            => tweenMotion.Apply(new ColorGradientMotion(tweenMotion.Item, gradient));

        #endregion
    }
}