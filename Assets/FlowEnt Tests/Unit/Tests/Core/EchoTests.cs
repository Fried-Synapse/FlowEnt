namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class EchoTests : AbstractAnimationTests<Echo>
    {
        protected override Echo CreateAnimation(float testTime)
            => new Echo(testTime);
    }
}
