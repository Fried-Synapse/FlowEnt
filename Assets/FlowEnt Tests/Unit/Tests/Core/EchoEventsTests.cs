using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class EchoEventsTests : AbstractAnimationEventsTests<Echo>
    {
        protected override Echo CreateAnimation(float testTime)
            => new Echo(testTime);

        protected override float GetUnitValue(float currentChange, float previousValue, float fullUnitValue)
        {
            return previousValue + (currentChange / fullUnitValue);
        }

        #region Builder

        [UnityTest]
        public IEnumerator Builder()
        {
            EchoEvents echoEvents = default;

            yield return CreateTester()
                .Act(() => echoEvents = Variables.Echo.Events.Build())
                .Assert(() =>
                {
                    static void assert(UnityEventBase unityEvent, Delegate action) => Assert.AreEqual(unityEvent.GetPersistentEventCount() == 0, action == null);

                    assert(Variables.Echo.Events.OnStarting, echoEvents.OnStartingEvent);
                    assert(Variables.Echo.Events.OnStarted, echoEvents.OnStartedEvent);
                    assert(Variables.Echo.Events.OnUpdating, echoEvents.OnUpdatingEvent);
                    assert(Variables.Echo.Events.OnUpdated, echoEvents.OnUpdatedEvent);
                    assert(Variables.Echo.Events.OnLoopCompleted, echoEvents.OnLoopCompletedEvent);
                    assert(Variables.Echo.Events.OnCompleted, echoEvents.OnCompletedEvent);
                    assert(Variables.Echo.Events.OnCompleting, echoEvents.OnCompletingEvent);
                })
                .Run();
        }

        #endregion
    }
}
