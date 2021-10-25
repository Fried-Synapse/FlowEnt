using System;

namespace FriedSynapse.FlowEnt
{
    public partial class Tween
    {
        private Action onStarting;
        private Action<float> onUpdating;
        private Action onCompleting;

        public Tween OnStarting(Action callback)
        {
            onStarting += callback;
            return this;
        }

        public Tween OnStarted(Action callback)
        {
            onStarted += callback;
            return this;
        }

        public Tween OnUpdating(Action<float> callback)
        {
            onUpdating += callback;
            return this;
        }

        public Tween OnUpdated(Action<float> callback)
        {
            onUpdated += callback;
            return this;
        }

        public Tween OnLoopCompleted(Action<int?> callback)
        {
            onLoopCompleted += callback;
            return this;
        }

        public Tween OnCompleting(Action callback)
        {
            onCompleting += callback;
            return this;
        }

        public Tween OnCompleted(Action callback)
        {
            onCompleted += callback;
            return this;
        }
    }
}
