using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class EchoEventsTests : AbstractEngineTests
    {
        #region Builder

        // [UnityTest]
        // public IEnumerator Builder()
        // {
        //     EchoEvents echoEvents = default;

        //     yield return CreateTester()
        //         .Act(() => echoEvents = Variables.EchoEventsBuilder.Build())
        //         .Assert(() =>
        //         {
        //             static void assert(UnityEventBase unityEvent, Delegate action) => Assert.AreEqual(unityEvent.GetPersistentEventCount() == 0, action == null);

        //             assert(Variables.EchoEventsBuilder.OnStarting, echoEvents.OnStartingEvent);
        //             assert(Variables.EchoEventsBuilder.OnStarted, echoEvents.OnStartedEvent);
        //             assert(Variables.EchoEventsBuilder.OnUpdating, echoEvents.OnUpdatingEvent);
        //             assert(Variables.EchoEventsBuilder.OnUpdated, echoEvents.OnUpdatedEvent);
        //             assert(Variables.EchoEventsBuilder.OnLoopCompleted, echoEvents.OnLoopCompletedEvent);
        //             assert(Variables.EchoEventsBuilder.OnCompleted, echoEvents.OnCompletedEvent);
        //             assert(Variables.EchoEventsBuilder.OnCompleting, echoEvents.OnCompletingEvent);
        //         })
        //         .Run();
        // }

        #endregion

        [UnityTest]
        public IEnumerator OnStarting()
        {
            bool wasCalled = false;
            float deltaT = 0;
            float controlT = 0;
            const float expectedT = 0f;

            yield return CreateTester()
                .Act(() =>
                    new Echo(TestTime)
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
                    new Echo(TestTime)
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
                    new Echo(TestTime)
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
                    new Echo(TestTime)
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
                    new Echo(TestTime / expectedLoopCount)
                        .SetLoopCount(expectedLoopCount)
                        .OnUpdated(t => deltaT += t)
                        .OnLoopCompleted((_) => { loopCount++; controlT = deltaT; })
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expectedLoopCount, loopCount);
                    Assert.GreaterOrEqual(expectedT, controlT);
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
                    Echo echo = new Echo(TestTime / expectedLoopCount)
                        .SetLoopCount(expectedLoopCount)
                        .OnUpdated(t => deltaT += t)
                        .OnLoopCompleted((_) => { loopCount++; controlT = deltaT; })
                        .Start();
                    return new Echo(TestTime).OnCompleted(() => echo.Stop()).Start();
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expectedLoopCount, loopCount);
                    Assert.GreaterOrEqual(expectedT, controlT);
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
                    new Echo(TestTime)
                        .OnUpdated(t => deltaT += t)
                        .OnCompleting(() => { wasCalled = true; controlT = deltaT; })
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(wasCalled);
                    Assert.GreaterOrEqual(expectedT, controlT);
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
                    new Echo(TestTime)
                        .OnUpdated(t => deltaT += t)
                        .OnCompleted(() => { wasCalled = true; controlT = deltaT; })
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(wasCalled);
                    Assert.GreaterOrEqual(expectedT, controlT);
                })
                .Run();
        }
    }
}
