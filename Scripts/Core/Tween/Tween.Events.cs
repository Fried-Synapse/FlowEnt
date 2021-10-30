using System;

namespace FriedSynapse.FlowEnt
{
    public partial class Tween
    {
        private Action onStarting;
        private Action<float> onUpdating;
        private Action onCompleting;

        /// <inheritdoc />
        public Tween OnStarting(Action callback)
        {
            onStarting += callback;
            return this;
        }

        /// <inheritdoc />
        public Tween OnStarted(Action callback)
        {
            onStarted += callback;
            return this;
        }

        /// <inheritdoc />
        public Tween OnUpdating(Action<float> callback)
        {
            onUpdating += callback;
            return this;
        }

        /// <inheritdoc />
        public Tween OnUpdated(Action<float> callback)
        {
            onUpdated += callback;
            return this;
        }

        /// <inheritdoc />
        public Tween OnLoopCompleted(Action<int?> callback)
        {
            onLoopCompleted += callback;
            return this;
        }

        /// <inheritdoc />
        public Tween OnCompleting(Action callback)
        {
            onCompleting += callback;
            return this;
        }

        /// <inheritdoc />
        public Tween OnCompleted(Action callback)
        {
            onCompleted += callback;
            return this;
        }
    }
}
