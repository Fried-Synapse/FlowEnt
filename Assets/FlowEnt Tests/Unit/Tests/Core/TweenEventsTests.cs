using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class TweenEventsTests : AbstractEngineTests
    {
        [UnityTest]
        public IEnumerator Builder()
        {
            TweenEvents tweenEvents = default;

            yield return CreateTester()
                .Act(() => tweenEvents = Variables.TweenEventsBuilder.Build())
                .Assert(() =>
                {
                    static void assert(UnityEventBase unityEvent, Delegate action) => Assert.AreEqual(unityEvent.GetPersistentEventCount() == 0, action == null);

                    assert(Variables.TweenEventsBuilder.OnStarting, tweenEvents.OnStartingEvent);
                    assert(Variables.TweenEventsBuilder.OnStarted, tweenEvents.OnStartedEvent);
                    assert(Variables.TweenEventsBuilder.OnUpdating, tweenEvents.OnUpdatingEvent);
                    assert(Variables.TweenEventsBuilder.OnUpdated, tweenEvents.OnUpdatedEvent);
                    assert(Variables.TweenEventsBuilder.OnLoopCompleted, tweenEvents.OnLoopCompletedEvent);
                    assert(Variables.TweenEventsBuilder.OnCompleted, tweenEvents.OnCompletedEvent);
                    assert(Variables.TweenEventsBuilder.OnCompleting, tweenEvents.OnCompletingEvent);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnStarting()
        {
            bool wasCalled = false;
            float deltaT = 0;
            float controlT = 0;
            const float expectedT = 0f;

            yield return CreateTester()
                .Act(() =>
                    new Tween(TestTime)
                        .OnStarting(() => { wasCalled = true; controlT = deltaT; })
                        .OnUpdated(t => deltaT = t)
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(wasCalled);
                    Assert.AreEqual(expectedT, controlT);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnStarted()
        {
            bool wasCalled = false;
            float deltaT = 0;
            float controlT = 0;
            const float expectedT = 0f;

            yield return CreateTester()
                .Act(() =>
                    new Tween(TestTime)
                        .OnStarted(() => { wasCalled = true; controlT = deltaT; })
                        .OnUpdated(t => deltaT = t)
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(wasCalled);
                    Assert.AreEqual(expectedT, controlT);
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
        public IEnumerator OnUpdated()
        {
            bool wasCalled = false;
            List<float> deltas = new List<float>();

            yield return CreateTester()
                .Act(() =>
                    new Tween(TestTime)
                        .OnUpdated(t => { wasCalled = true; deltas.Add(t); })
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
        public IEnumerator OnLoopCompleted()
        {
            const int expectedLoopCount = 2;
            int loopCount = 0;
            float deltaT = 0;
            float controlT = 0;
            const float expectedT = 1f;

            yield return CreateTester()
                .Act(() =>
                    new Tween(TestTime / expectedLoopCount)
                        .SetLoopCount(expectedLoopCount)
                        .OnLoopCompleted((_) => { loopCount++; controlT = deltaT; })
                        .OnUpdated(t => deltaT = t)
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expectedLoopCount, loopCount);
                    Assert.AreEqual(expectedT, controlT);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnLoopCompleted_Infinite()
        {
            const int expectedLoopCount = 2;
            int loopCount = 0;
            float deltaT = 0;
            float controlT = 0;
            const float expectedT = 1f;

            yield return CreateTester()
                .Act(() =>
                {
                    Tween tween = new Tween(TestTime / expectedLoopCount)
                        .SetLoopCount(expectedLoopCount)
                        .OnLoopCompleted((_) => { loopCount++; controlT = deltaT; })
                        .OnUpdated(t => deltaT = t)
                        .Start();
                    return new Tween(TestTime).OnCompleted(() => tween.Stop()).Start();
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expectedLoopCount, loopCount);
                    Assert.AreEqual(expectedT, controlT);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnCompleting()
        {
            bool wasCalled = false;
            float deltaT = 0f;
            float controlT = 0f;
            const float expectedT = 1f;

            yield return CreateTester()
                .Act(() =>
                    new Tween(TestTime)
                        .OnCompleting(() => { wasCalled = true; controlT = deltaT; })
                        .OnUpdated(t => deltaT = t)
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(wasCalled);
                    Assert.AreEqual(expectedT, controlT);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnCompleted()
        {
            bool wasCalled = false;
            float deltaT = 0f;
            float controlT = 0f;
            const float expectedT = 1f;

            yield return CreateTester()
                .Act(() =>
                    new Tween(TestTime)
                        .OnCompleted(() => { wasCalled = true; controlT = deltaT; })
                        .OnUpdated(t => deltaT = t)
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(wasCalled);
                    Assert.AreEqual(expectedT, controlT);
                })
                .Run();
        }
    }
}
