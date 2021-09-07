using System;

namespace FriedSynapse.FlowEnt
{
    public abstract class AbstractFlowAwaiter : AbstractUpdatable
    {
        internal override void StartInternal(float deltaTime = 0)
        {
            updateController.SubscribeToUpdate(this);
            onStarted?.Invoke();
        }

        protected abstract bool ShouldWait(float deltaTime);
        internal override void UpdateInternal(float deltaTime)
        {
            if (ShouldWait(deltaTime))
            {
                onUpdated?.Invoke(deltaTime);
            }
            else
            {
                onCompleted?.Invoke();
                updateController.UnsubscribeFromUpdate(this);
                ((Flow)updateController).CompleteUpdatable(this);
            }
        }

        public AbstractFlowAwaiter OnStarted(Action callback)
        {
            onStarted += callback;
            return this;
        }

        public AbstractFlowAwaiter OnUpdated(Action<float> callback)
        {
            onUpdated += callback;
            return this;
        }

        public AbstractFlowAwaiter OnCompleted(Action callback)
        {
            onCompleted += callback;
            return this;
        }
    }
}
