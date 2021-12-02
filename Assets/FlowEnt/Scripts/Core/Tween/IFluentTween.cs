using System;
using FriedSynapse.FlowEnt.Easings;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    internal interface IFluentTweenEventable<T>
    {
        /// <summary>
        /// Adds an event called before the tween starts.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnStarting(Action callback);

        /// <summary>
        /// Adds an event called after the tween starts.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnStarted(Action callback);

        /// <summary>
        /// Adds an event called before the tween updates.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnUpdating(Action<float> callback);

        /// <summary>
        /// Adds an event called after the tween updates.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnUpdated(Action<float> callback);

        /// <summary>
        /// Adds an event called after a loop completed.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnLoopCompleted(Action<int?> callback);

        /// <summary>
        /// Adds an event called before the tween completes.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnCompleting(Action callback);

        /// <summary>
        /// Adds an event called after the tween completes.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnCompleted(Action callback);
    }

    internal interface IFluentTweenOptionable<T>
    {
        /// <summary>
        /// Sets the name of the tween.
        /// </summary>
        /// <param name="name"></param>
        T SetName(string name);

        /// <summary>
        /// Sets whether this tween should auto-start or not.
        /// </summary>
        /// <remarks>Auto-start will be slower than a true-start. See more at https://flowent.friedsynapse.com/tips#h.s5cucrg5qyjc</remarks>
        /// <param name="autoStart"></param>
        T SetAutoStart(bool autoStart);

        /// <summary>
        /// Sets the amount of frames you want to skip at when this tween is started.
        /// </summary>
        /// <param name="frames"></param>
        T SetSkipFrames(int frames);

        /// <summary>
        /// Sets the amount of time in seconds that you want to delay when this tween is started.
        /// </summary>
        /// <param name="time"></param>
        T SetDelay(float time);

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
        T SetEasing(IEasing easing, bool reverse = Tween.DefaultEasingReverse);

        /// <summary>
        /// Sets the easing of the tween using predefined values.
        /// </summary>
        /// <param name="easing"></param>
        /// <param name="reverse">Will apply a reverse on the easing.</param>
        T SetEasing(Easing easing, bool reverse = Tween.DefaultEasingReverse);

        /// <summary>
        /// Sets the easing of the tween using an animation curve.
        /// </summary>
        /// <param name="animationCurve"></param>
        /// <param name="reverse">Will apply a reverse on the easing.</param>
        T SetEasing(AnimationCurve animationCurve, bool reverse = Tween.DefaultEasingReverse);

        /// <summary>
        /// Sets the loop type of the tween.
        /// </summary>
        /// <param name="loopType"></param>
        T SetLoopType(LoopType loopType);

        /// <summary>
        /// Sets the amount of loops you want this tween to have. If you want infinite loops pass a null value.
        /// </summary>
        /// <param name="loopCount"></param>
        T SetLoopCount(int? loopCount);

        /// <summary>
        /// Sets the time scale for the current flow(and all it's animations).
        /// </summary>
        /// <param name="timeScale"></param>
        T SetTimeScale(float timeScale);
    }
}
