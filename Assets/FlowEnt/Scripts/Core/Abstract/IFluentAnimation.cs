using System;

namespace FriedSynapse.FlowEnt
{
    public interface IFluentAnimationEventable<T>
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
