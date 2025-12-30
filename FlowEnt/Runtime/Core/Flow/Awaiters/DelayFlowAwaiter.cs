namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Flow awaiter that uses a will delay for a certain amount of time.
    /// </summary>
    public class DelayFlowAwaiter : AbstractFlowAwaiter
    {
        public DelayFlowAwaiter(float delay)
        {
            this.delay = delay;
            delayLeft = delay;
        }
        private readonly float delay;
        private float delayLeft;
        public float Overdraft => delayLeft > 0 ? 0 : -delayLeft;

        protected override bool ShouldWait(float deltaTime)
        {
            delayLeft -= deltaTime;
            return delayLeft > 0;
        }

        protected override void ResetInternal()
        {
            base.ResetInternal();
            delayLeft = delay;
        }
    }
}
