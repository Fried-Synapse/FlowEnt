using FriedSynapse.FlowEnt.Motions.Tween.UI.CanvasGroups;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class CanvasGroupMotionExtensions
    {
        /// <summary>
        /// Applies a <see cref="AlphaMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<CanvasGroup> Alpha(this TweenMotionProxy<CanvasGroup> proxy, float value)
            => proxy.Apply(new AlphaMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="AlphaMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<CanvasGroup> AlphaTo(this TweenMotionProxy<CanvasGroup> proxy, float to)
            => proxy.Apply(new AlphaMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="AlphaMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<CanvasGroup> AlphaTo(this TweenMotionProxy<CanvasGroup> proxy, float from, float to)
            => proxy.Apply(new AlphaMotion(proxy.Item, from, to));
    }
}