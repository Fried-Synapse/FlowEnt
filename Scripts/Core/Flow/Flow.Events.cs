using System;

namespace FriedSynapse.FlowEnt
{
    public partial class Flow
    {
        /// <inheritdoc />
        public Flow OnStarted(Action callback)
        {
            onStarted += callback;
            return this;
        }

        /// <inheritdoc />
        public Flow OnUpdated(Action<float> callback)
        {
            onUpdated += callback;
            return this;
        }

        /// <inheritdoc />
        public Flow OnLoopCompleted(Action<int?> callback)
        {
            onLoopCompleted += callback;
            return this;
        }

        /// <inheritdoc />
        public Flow OnCompleted(Action callback)
        {
            onCompleted += callback;
            return this;
        }
    }
}
