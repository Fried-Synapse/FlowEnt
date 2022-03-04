using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class EchoEventsTests : AbstractAnimationEventsTests<Echo>
    {
        protected override Echo CreateAnimation(float testTime)
            => new Echo(testTime);

        protected override float UpdatedControlOperation(float controlTracker, float t)
            => controlTracker + t;

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

        [UnityTest]
        public IEnumerator OnStarting()
        {
            bool wasCalled = false;
            float controlTracker = 0;
            float control = 0;
            const float expected = 0f;

            yield return CreateTester()
                .Act(() =>
                    CreateAnimation(TestTime)
                        .OnStarting(() => { wasCalled = true; control = controlTracker; })
                        .OnUpdated(t => controlTracker = UpdatedControlOperation(controlTracker, t))
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(wasCalled);
                    Assert.AreEqual(expected, control);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnUpdating()
        {
            bool wasCalled = false;
            List<float> deltas = new List<float>();

            yield return CreateTester()
                .Act(() =>
                    CreateAnimation(TestTime)
                        .OnUpdating(t => { wasCalled = true; deltas.Add(t); })
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(wasCalled);
                    Assert.Greater(deltas.Count, 0);
                    Assert.True(deltas.TrueForAll(t => 0 <= t && t <= 1));
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnCompleting()
        {
            bool wasCalled = false;
            float controlTracker = 0;
            float control = 0;
            const float expected = 1f;

            yield return CreateTester()
                .Act(() =>
                    CreateAnimation(TestTime)
                        .OnUpdated(t => controlTracker = UpdatedControlOperation(controlTracker, t))
                        .OnCompleting(() => { wasCalled = true; control = controlTracker; })
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(wasCalled);
                    Assert.GreaterOrEqual(expected, control);
                })
                .Run();
        }
    }
}
