using System;

namespace FriedSynapse.FlowEnt
{
    public class CallbackFlowAwaiter : AbstractFlowAwaiter
    {
        public CallbackFlowAwaiter(Func<bool> callback)
        {
            this.callback = callback;
        }
        private readonly Func<bool> callback;

        internal override void UpdateInternal(float deltaTime)
        {
            if (callback.Invoke())
            {
                Complete();
            }
        }
    }
}
