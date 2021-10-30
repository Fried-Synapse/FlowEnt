using System;

namespace FriedSynapse.FlowEnt
{
    public partial class Flow
    {
        /// <summary>
        /// Adds an event called when the flow started.
        /// </summary>
        /// <param name="callback">The event.</param>
        public Flow OnStarted(Action callback)
        {
            onStarted += callback;
            return this;
        }

        /// <summary>
        /// Adds an event called when the flow updated.
        /// </summary>
        /// <param name="callback">The event.</param>
        public Flow OnUpdated(Action<float> callback)
        {
            onUpdated += callback;
            return this;
        }

        /// <summary>
        /// Adds an event called when a loop completed.
        /// </summary>
        /// <param name="callback">The event. The parameter represents the number of loops left. If there are infinite loops it'll send a null param.</param>
        public Flow OnLoopCompleted(Action<int?> callback)
        {
            onLoopCompleted += callback;
            return this;
        }

        /// <summary>
        /// Adds an event called when the flow completed.
        /// </summary>
        /// <param name="callback">The event.</param>
        public Flow OnCompleted(Action callback)
        {
            onCompleted += callback;
            return this;
        }
    }
}
