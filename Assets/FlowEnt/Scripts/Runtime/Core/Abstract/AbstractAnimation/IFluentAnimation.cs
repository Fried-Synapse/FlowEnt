using System;

namespace FriedSynapse.FlowEnt
{
    internal interface IFluentAnimationOptionable<TAnimation>
    {
        /// <summary>
        /// Sets the name of the animation.
        /// </summary>
        /// <param name="name"></param>
        TAnimation SetName(string name);

        /// <summary>
        /// Sets the update type for the animation.
        /// </summary>
        /// <param name="updateType"></param>
        TAnimation SetUpdateType(UpdateType updateType);

        /// <summary>
        /// Sets whether this animation should auto-start or not.
        /// </summary>
        /// <remarks>Auto-start will be slower than a true-start. See more at https://flowent.friedsynapse.com/tips#h.s5cucrg5qyjc</remarks>
        /// <param name="autoStart"></param>
        TAnimation SetAutoStart(bool autoStart);

        /// <summary>
        /// Sets the amount of frames you want to skip at when this animation is started.
        /// </summary>
        /// <param name="frames"></param>
        TAnimation SetSkipFrames(int frames);

        /// <summary>
        /// Sets the amount of time in seconds that you want to delay when this animation is started.
        /// </summary>
        /// <param name="time"></param>
        TAnimation SetDelay(float time);

        /// <summary>
        /// Sets the amount of loops you want this animation to have. If you want infinite loops pass a null value.
        /// </summary>
        /// <remarks>
        /// Flows only have reset loop types.
        /// </remarks>
        /// <param name="loopCount"></param>
        TAnimation SetLoopCount(int? loopCount);

        /// <summary>
        /// Sets the time scale for the current animation.
        /// </summary>
        /// <param name="timeScale"></param>
        TAnimation SetTimeScale(float timeScale);
    }

    internal interface IFluentAnimationEventable<T>
    {
        /// <summary>
        /// Adds an event called when the animation is starting.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnStarting(Action callback);

        /// <summary>
        /// Adds an event called when the animation has started.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnStarted(Action callback);

        /// <summary>
        /// Adds an event called when the animation is updating.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnUpdating(Action<float> callback);

        /// <summary>
        /// Adds an event called when the animation has updated.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnUpdated(Action<float> callback);

        /// <summary>
        /// Adds an event called when the animation loop has started.
        /// </summary>
        /// <param name="callback">The event. The parameter represents the number of loops left. If there are infinite loops it'll send a null param.</param>
        T OnLoopStarted(Action<int?> callback);

        /// <summary>
        /// Adds an event called when the animation loop has completed.
        /// </summary>
        /// <param name="callback">The event. The parameter represents the number of loops left. If there are infinite loops it'll send a null param.</param>
        T OnLoopCompleted(Action<int?> callback);

        /// <summary>
        /// Adds an event called when the animation is completing.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnCompleting(Action callback);

        /// <summary>
        /// Adds an event called when the animation has completed.
        /// </summary>
        /// <param name="callback">The event.</param>
        T OnCompleted(Action callback);
    }
}
