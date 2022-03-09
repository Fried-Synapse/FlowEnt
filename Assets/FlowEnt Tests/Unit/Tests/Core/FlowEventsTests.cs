using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class FlowEventsTests : AbstractAnimationEventsTests<Flow>
    {
        protected override Flow CreateAnimation(float testTime)
            => new Flow().Queue(new Tween(testTime));

        protected override float UpdatedControlOperation(float controlTracker, float t)
            => t;

        #region Builder

        [UnityTest]
        public IEnumerator Builder()
        {
            FlowEvents flowEvents = default;

            yield return CreateTester()
                .Act(() => flowEvents = Variables.Flow.Events.Build())
                .Assert(() =>
                {
                    static void assert(UnityEventBase unityEvent, Delegate action) => Assert.AreEqual(unityEvent.GetPersistentEventCount() == 0, action == null);

                    assert(Variables.Flow.Events.OnStarted, flowEvents.OnStartedEvent);
                    assert(Variables.Flow.Events.OnUpdated, flowEvents.OnUpdatedEvent);
                    assert(Variables.Flow.Events.OnLoopCompleted, flowEvents.OnLoopCompletedEvent);
                    assert(Variables.Flow.Events.OnCompleted, flowEvents.OnCompletedEvent);
                })
                .Run();
        }

        #endregion
    }
}
