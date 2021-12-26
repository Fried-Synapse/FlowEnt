using FriedSynapse.FlowEnt.Motions.UI.CanvasGroups;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class CanvasGroupMotionExtensions
    {
        /// <summary>
        /// Applies a <see cref="AlphaMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotion<CanvasGroup> Alpha(this TweenMotion<CanvasGroup> tweenMotion, float value)
            => tweenMotion.Apply(new AlphaMotion(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="AlphaMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotion<CanvasGroup> AlphaTo(this TweenMotion<CanvasGroup> tweenMotion, float to)
            => tweenMotion.Apply(new AlphaMotion(tweenMotion.Item, default, to));

        /// <summary>
        /// Applies a <see cref="AlphaMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotion<CanvasGroup> AlphaTo(this TweenMotion<CanvasGroup> tweenMotion, float from, float to)
            => tweenMotion.Apply(new AlphaMotion(tweenMotion.Item, from, to));
    }
}