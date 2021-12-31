namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class TweenTests : AbstractAnimationTests<Tween>
    {
        protected override Tween CreateAnimation(float testTime)
            => new Tween(testTime);
    }
}
