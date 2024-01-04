using System.Collections;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class FlowEventsTests : AbstractAnimationEventsTests<Flow>
    {
        protected override Flow CreateAnimation(float testTime)
            => new Flow().Queue(new Tween(testTime));

        protected override float GetTotalTimeFromUpdate(float t, float previousValue, float loopTime)
            => previousValue + t;

        #region Builder

        [UnityTest]
        public IEnumerator Builder()
        {
            FlowEvents flowEvents = default;

            yield return CreateTester()
                .Act(() => flowEvents = Variables.Flow.Events.Build())
                .Assert(() =>
                {
                    Assert(Variables.Flow.Events.OnStarted, flowEvents.OnStartedEvent);
                    Assert(Variables.Flow.Events.OnUpdated, flowEvents.OnUpdatedEvent);
                    Assert(Variables.Flow.Events.OnLoopCompleted, flowEvents.OnLoopCompletedEvent);
                    Assert(Variables.Flow.Events.OnCompleted, flowEvents.OnCompletedEvent);
                })
                .Run();
        }

        #endregion
    }
}