namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class FlowEventsTests : AbstractAnimationEventsTests<Flow>
    {
        protected override Flow CreateAnimation(float testTime)
            => new Flow().Queue(new Tween(testTime));

        protected override float UpdatedControlOperation(float controlTracker, float t)
            => t;
    }
}
