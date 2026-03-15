using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Tween.SpriteRenderers;

namespace FriedSynapse.FlowEnt
{
    public static class SpriteRendererMotionExtensions
    {
        #region Color

        /// <summary>
        /// Applies a <see cref="ColorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<SpriteRenderer> Color(this TweenMotionProxy<SpriteRenderer> proxy, Color value)
            => proxy.Apply(new ColorMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="ColorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<SpriteRenderer> ColorTo(this TweenMotionProxy<SpriteRenderer> proxy, Color to)
            => proxy.Apply(new ColorMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="ColorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<SpriteRenderer> ColorTo(this TweenMotionProxy<SpriteRenderer> proxy, Color from, Color to)
            => proxy.Apply(new ColorMotion(proxy.Item, from, to));

        /// <summary>
        /// Applies a <see cref="ColorGradientMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="gradient"></param>
        public static TweenMotionProxy<SpriteRenderer> ColorTo(this TweenMotionProxy<SpriteRenderer> proxy, Gradient gradient)
            => proxy.Apply(new ColorGradientMotion(proxy.Item, gradient));

        #endregion

        #region Size

        /// <summary>
        /// Applies a <see cref="SizeMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<SpriteRenderer> Size(this TweenMotionProxy<SpriteRenderer> proxy, Vector2 value)
            => proxy.Apply(new SizeMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="SizeMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<SpriteRenderer> SizeTo(this TweenMotionProxy<SpriteRenderer> proxy, Vector2 to)
            => proxy.Apply(new SizeMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="SizeMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<SpriteRenderer> SizeTo(this TweenMotionProxy<SpriteRenderer> proxy, Vector2 from, Vector2 to)
            => proxy.Apply(new SizeMotion(proxy.Item, from, to));

        #endregion
    }
}