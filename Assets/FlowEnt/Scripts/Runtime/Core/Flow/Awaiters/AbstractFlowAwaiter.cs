using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides Flow awaiters basic functionality
    /// </summary>
    public abstract class AbstractFlowAwaiter : AbstractUpdatable
    {
        internal override void StartInternal(float deltaTime = 0)
        {
            updateController.SubscribeToUpdate(this);
            onStarted?.Invoke();
        }

        /// <summary>
        /// Decides weather the flow should wait for this awaiter or not.
        /// </summary>
        /// <param name="deltaTime">Current frame delta time.</param>
        /// <returns>If True the flow will wait. Otherwise the flow will continue</returns>
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

        /// <summary>
        /// Adds an event called when the awaiter started waiting.
        /// </summary>
        /// <param name="callback">The event.</param>
        public AbstractFlowAwaiter OnStarted(Action callback)
        {
            onStarted += callback;
            return this;
        }

        /// <summary>
        /// Adds an event called when the awaiter is waiting.
        /// </summary>
        /// <param name="callback">The event.</param>
        public AbstractFlowAwaiter OnUpdated(Action<float> callback)
        {
            onUpdated += callback;
            return this;
        }

        /// <summary>
        /// Adds an event called when the awaiter has finished waiting.
        /// </summary>
        /// <param name="callback">The event.</param>
        public AbstractFlowAwaiter OnCompleted(Action callback)
        {
            onCompleted += callback;
            return this;
        }
    }
}
