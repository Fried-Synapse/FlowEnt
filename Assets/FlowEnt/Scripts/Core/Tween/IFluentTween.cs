using System;
using FriedSynapse.FlowEnt.Easings;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    internal interface IFluentTweenOptionable<T> : IFluentAnimationOptionable<T>
    {
        /// <summary>
        /// Sets the amount of time in seconds that this tween will last.
        /// </summary>
        /// <param name="time"></param>
        T SetTime(float time);

        /// <summary>
        /// Sets the easing of the tween.
        /// </summary>
        /// <param name="easing"></param>
        /// <param name="reverse">Will apply a reverse on the easing.</param>
        T SetEasing(IEasing easing, bool reverse = TweenOptions.DefaultEasingReverse);

        /// <summary>
        /// Sets the easing of the tween using predefined values.
        /// </summary>
        /// <param name="easing"></param>
        /// <param name="reverse">Will apply a reverse on the easing.</param>
        T SetEasing(Easing easing, bool reverse = TweenOptions.DefaultEasingReverse);

        /// <summary>
        /// Sets the easing of the tween using an animation curve.
        /// </summary>
        /// <param name="animationCurve"></param>
        /// <param name="reverse">Will apply a reverse on the easing.</param>
        T SetEasing(AnimationCurve animationCurve, bool reverse = TweenOptions.DefaultEasingReverse);

        /// <summary>
        /// Sets the loop type of the tween.
        /// </summary>
        /// <param name="loopType"></param>
        T SetLoopType(LoopType loopType);
    }

    internal interface IFluentTweenEventable<T> : IFluentAnimationEventable<T>
    {
        /// <summary>
        /// Adds an event called before the animation starts.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnStarting(Action callback);

        /// <summary>
        /// Adds an event called before the animation updates.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnUpdating(Action<float> callback);

        /// <summary>
        /// Adds an event called before the animation completes.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnCompleting(Action callback);
    }
}
