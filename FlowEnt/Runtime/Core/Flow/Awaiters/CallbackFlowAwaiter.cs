using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Flow awaiter that uses a callback to check if it needs to wait or not.
    /// </summary>
    public class CallbackFlowAwaiter : AbstractFlowAwaiter
    {
        public CallbackFlowAwaiter(Func<bool> waitCondition)
        {
            this.waitCondition = waitCondition;
        }
        private readonly Func<bool> waitCondition;

        protected override bool ShouldWait(float deltaTime) => waitCondition.Invoke();
    }
}
