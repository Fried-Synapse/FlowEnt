using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class TweenEventsTests : AbstractAnimationEventsTests<Tween>
    {
        protected override Tween CreateAnimation(float testTime)
            => new Tween(testTime);

        protected override float UpdatedControlOperation(float controlTracker, float t)
            => t;

        #region Builder

        [UnityTest]
        public IEnumerator Builder()
        {
            TweenEvents tweenEvents = default;

            yield return CreateTester()
                .Act(() => tweenEvents = Variables.Tween.Events.Build())
                .Assert(() =>
                {
                    static void assert(UnityEventBase unityEvent, Delegate action) => Assert.AreEqual(unityEvent.GetPersistentEventCount() == 0, action == null);

                    assert(Variables.Tween.Events.OnStarting, tweenEvents.OnStartingEvent);
                    assert(Variables.Tween.Events.OnStarted, tweenEvents.OnStartedEvent);
                    assert(Variables.Tween.Events.OnUpdating, tweenEvents.OnUpdatingEvent);
                    assert(Variables.Tween.Events.OnUpdated, tweenEvents.OnUpdatedEvent);
                    assert(Variables.Tween.Events.OnLoopCompleted, tweenEvents.OnLoopCompletedEvent);
                    assert(Variables.Tween.Events.OnCompleted, tweenEvents.OnCompletedEvent);
                    assert(Variables.Tween.Events.OnCompleting, tweenEvents.OnCompletingEvent);
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
                    new Tween(TestTime)
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
                    new Tween(TestTime)
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
                    new Tween(TestTime)
                        .OnUpdated(t => controlTracker = UpdatedControlOperation(controlTracker, t))
                        .OnCompleting(() => { wasCalled = true; control = controlTracker; })
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(wasCalled);
                    Assert.AreEqual(expected, control);
                })
                .Run();
        }
    }
}
