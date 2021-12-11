using System;

namespace FriedSynapse.FlowEnt
{
    internal interface IFluentAnimationOptionable<T>
    {
        /// <summary>
        /// Sets the name of the animation.
        /// </summary>
        /// <param name="name"></param>
        T SetName(string name);

        /// <summary>
        /// Sets whether this animation should auto-start or not.
        /// </summary>
        /// <remarks>Auto-start will be slower than a true-start. See more at https://flowent.friedsynapse.com/tips#h.s5cucrg5qyjc</remarks>
        /// <param name="autoStart"></param>
        T SetAutoStart(bool autoStart);

        /// <summary>
        /// Sets the amount of frames you want to skip at when this animation is started.
        /// </summary>
        /// <param name="frames"></param>
        T SetSkipFrames(int frames);

        /// <summary>
        /// Sets the amount of time in seconds that you want to delay when this animation is started.
        /// </summary>
        /// <param name="time"></param>
        T SetDelay(float time);

        /// <summary>
        /// Sets the amount of loops you want this animation to have. If you want infinite loops pass a null value.
        /// </summary>
        /// <remarks>
        /// Flows only have reset loop types.
        /// </remarks>
        /// <param name="loopCount"></param>
        T SetLoopCount(int? loopCount);

        /// <summary>
        /// Sets the time scale for the current animation.
        /// </summary>
        /// <param name="timeScale"></param>
        T SetTimeScale(float timeScale);
    }

    internal interface IFluentAnimationEventable<T>
    {
        /// <summary>
        /// Adds an event called when the animation started.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnStarted(Action callback);

        /// <summary>
        /// Adds an event called when the animation updated.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnUpdated(Action<float> callback);

        /// <summary>
        /// Adds an event called when a loop completed.
        /// </summary>
        /// <param name="callback">The event. The parameter represents the number of loops left. If there are infinite loops it'll send a null param.</param>
        T OnLoopCompleted(Action<int?> callback);

        /// <summary>
        /// Adds an event called when the animation completed.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnCompleted(Action callback);
    }
}
