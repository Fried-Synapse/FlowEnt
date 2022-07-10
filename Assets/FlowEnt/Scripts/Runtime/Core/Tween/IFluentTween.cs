using System;
using FriedSynapse.FlowEnt.Easings;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    internal interface IFluentTweenOptionable<TTween> : IFluentAnimationOptionable<TTween>
    {
        /// <summary>
        /// Sets the amount of time in seconds that this tween will last.
        /// </summary>
        /// <param name="time"></param>
        TTween SetTime(float time);

        /// <summary>
        /// Sets the easing of the tween.
        /// </summary>
        /// <param name="easing"></param>
        /// <param name="reverse">Will apply a reverse on the easing.</param>
        TTween SetEasing(IEasing easing, bool reverse = TweenOptions.DefaultEasingReverse);

        /// <summary>
        /// Sets the easing of the tween using predefined values.
        /// </summary>
        /// <param name="easing"></param>
        /// <param name="reverse">Will apply a reverse on the easing.</param>
        TTween SetEasing(Easing easing, bool reverse = TweenOptions.DefaultEasingReverse);

        /// <summary>
        /// Sets the easing of the tween using an animation curve.
        /// </summary>
        /// <param name="animationCurve"></param>
        /// <param name="reverse">Will apply a reverse on the easing.</param>
        TTween SetEasing(AnimationCurve animationCurve, bool reverse = TweenOptions.DefaultEasingReverse);

        /// <summary>
        /// Sets the loop type of the tween.
        /// </summary>
        /// <param name="loopType"></param>
        TTween SetLoopType(LoopType loopType);
    }

    internal interface IFluentTweenEventable<TTween> : IFluentAnimationEventable<TTween>
    {
    }
}
