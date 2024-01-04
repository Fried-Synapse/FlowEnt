using System.Collections;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class EchoEventsTests : AbstractAnimationEventsTests<Echo>
    {
        protected override Echo CreateAnimation(float testTime)
            => new (testTime);

        protected override float GetTotalTimeFromUpdate(float t, float previousValue, float loopTime)
            => previousValue + t;

        #region Builder

        [UnityTest]
        public IEnumerator Builder()
        {
            EchoEvents echoEvents = default;

            yield return CreateTester()
                .Act(() => echoEvents = Variables.Echo.Events.Build())
                .Assert(() =>
                {
                    Assert(Variables.Echo.Events.OnStarting, echoEvents.OnStartingEvent);
                    Assert(Variables.Echo.Events.OnStarted, echoEvents.OnStartedEvent);
                    Assert(Variables.Echo.Events.OnUpdating, echoEvents.OnUpdatingEvent);
                    Assert(Variables.Echo.Events.OnUpdated, echoEvents.OnUpdatedEvent);
                    Assert(Variables.Echo.Events.OnLoopCompleted, echoEvents.OnLoopCompletedEvent);
                    Assert(Variables.Echo.Events.OnCompleted, echoEvents.OnCompletedEvent);
                    Assert(Variables.Echo.Events.OnCompleting, echoEvents.OnCompletingEvent);
                })
                .Run();
        }

        #endregion
    }
}