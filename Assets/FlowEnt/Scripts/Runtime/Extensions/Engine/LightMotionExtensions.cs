using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Tween.Lights;

namespace FriedSynapse.FlowEnt
{
    public static class LightMotionExtensions
    {
        #region Intensity

        /// <summary>
        /// Applies a <see cref="IntensityMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<Light> Intensity(this TweenMotionProxy<Light> proxy, float value)
            => proxy.Apply(new IntensityMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="IntensityMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Light> IntensityTo(this TweenMotionProxy<Light> proxy, float to)
            => proxy.Apply(new IntensityMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="IntensityMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Light> IntensityTo(this TweenMotionProxy<Light> proxy, float from, float to)
            => proxy.Apply(new IntensityMotion(proxy.Item, from, to));

        #endregion

        #region Range

        /// <summary>
        /// Applies a <see cref="RangeMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<Light> Range(this TweenMotionProxy<Light> proxy, float value)
            => proxy.Apply(new RangeMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="RangeMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Light> RangeTo(this TweenMotionProxy<Light> proxy, float to)
            => proxy.Apply(new RangeMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="RangeMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Light> RangeTo(this TweenMotionProxy<Light> proxy, float from, float to)
            => proxy.Apply(new RangeMotion(proxy.Item, from, to));

        #endregion

        #region Color

        /// <summary>
        /// Applies a <see cref="ColorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<Light> Color(this TweenMotionProxy<Light> proxy, Color value)
            => proxy.Apply(new ColorMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="ColorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Light> ColorTo(this TweenMotionProxy<Light> proxy, Color to)
            => proxy.Apply(new ColorMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="ColorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Light> ColorTo(this TweenMotionProxy<Light> proxy, Color from, Color to)
            => proxy.Apply(new ColorMotion(proxy.Item, from, to));

        /// <summary>
        /// Applies a <see cref="ColorGradientMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="gradient"></param>
        public static TweenMotionProxy<Light> ColorTo(this TweenMotionProxy<Light> proxy, Gradient gradient)
            => proxy.Apply(new ColorGradientMotion(proxy.Item, gradient));

        #endregion

        #region ShadowRadius
#if !UNITY_WEBGL

        /// <summary>
        /// Applies a <see cref="ShadowRadiusMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<Light> ShadowRadius(this TweenMotionProxy<Light> proxy, float value)
            => proxy.Apply(new ShadowRadiusMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="ShadowRadiusMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Light> ShadowRadiusTo(this TweenMotionProxy<Light> proxy, float to)
            => proxy.Apply(new ShadowRadiusMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="ShadowRadiusMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Light> ShadowRadiusTo(this TweenMotionProxy<Light> proxy, float from, float to)
            => proxy.Apply(new ShadowRadiusMotion(proxy.Item, from, to));

#endif
        #endregion

        #region ShadowStrength

        /// <summary>
        /// Applies a <see cref="ShadowStrengthMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<Light> ShadowStrength(this TweenMotionProxy<Light> proxy, float value)
            => proxy.Apply(new ShadowStrengthMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="ShadowStrengthMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Light> ShadowStrengthTo(this TweenMotionProxy<Light> proxy, float to)
            => proxy.Apply(new ShadowStrengthMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="ShadowStrengthMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<Light> ShadowStrengthTo(this TweenMotionProxy<Light> proxy, float from, float to)
            => proxy.Apply(new ShadowStrengthMotion(proxy.Item, from, to));

        #endregion
    }
}