using System;

namespace FriedSynapse.FlowEnt
{
    internal interface IFluentFlowEventable<T>
    {
        /// <summary>
        /// Adds an event called when the flow started.
        /// </summary>
        /// <param name="callback">The event.</param>
        Flow OnStarted(Action callback);

        /// <summary>
        /// Adds an event called when the flow updated.
        /// </summary>
        /// <param name="callback">The event.</param>
        Flow OnUpdated(Action<float> callback);

        /// <summary>
        /// Adds an event called when a loop completed.
        /// </summary>
        /// <param name="callback">The event. The parameter represents the number of loops left. If there are infinite loops it'll send a null param.</param>
        Flow OnLoopCompleted(Action<int?> callback);

        /// <summary>
        /// Adds an event called when the flow completed.
        /// </summary>
        /// <param name="callback">The event.</param>
        Flow OnCompleted(Action callback);
    }

    internal interface IFluentFlowOptionable<T>
    {
        /// <summary>
        /// Sets whether this tween should auto-start or not.
        /// </summary>
        /// <remarks>Auto-start will be slower than a true-start. See more at https://flowent.friedsynapse.com/tips#h.s5cucrg5qyjc</remarks>
        /// <param name="autoStart"></param>
        T SetAutoStart(bool autoStart);

        /// <summary>
        /// Sets the amount of frames you want to skip at when this flow is started.
        /// </summary>
        /// <param name="frames"></param>
        T SetSkipFrames(int frames);

        /// <summary>
        /// Sets the amount of time in seconds that you want to delay when this flow is started.
        /// </summary>
        /// <param name="time"></param>
        T SetDelay(float time);

        /// <summary>
        /// Sets the amount of loops you want this flow to have. If you want infinite loops pass a null value.
        /// </summary>
        /// <remarks>
        /// All loops are reset. No ping-pong option for flows.
        /// </remarks>
        /// <param name="loopCount"></param>
        T SetLoopCount(int? loopCount);

        /// <summary>
        /// Sets the time scale for the current flow(and all it's animations).
        /// </summary>
        /// <param name="timeScale"></param>
        T SetTimeScale(float timeScale);
    }
}
