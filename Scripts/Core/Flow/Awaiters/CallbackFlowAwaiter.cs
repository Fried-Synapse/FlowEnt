using System;

namespace FriedSynapse.FlowEnt
{
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
