using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public abstract class AbstractAnimationEventsTests<TAnimation> : AbstractEngineTests
        where TAnimation : AbstractAnimation
    {
        protected abstract TAnimation CreateAnimation(float testTime);
        protected abstract float GetUnitValue(float currentChange, float previousValue, float fullUnitValue);

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
                        .OnUpdated(t => controlTracker = GetUnitValue(t, controlTracker, TestTime))
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
        public IEnumerator OnStarted()
        {
            bool wasCalled = false;
            float controlTracker = 0;
            float control = 0;
            const float expected = 0f;

            yield return CreateTester()
                .Act(() =>
                    CreateAnimation(TestTime)
                        .OnStarted(() => { wasCalled = true; control = controlTracker; })
                        .OnUpdated(t => controlTracker = GetUnitValue(t, controlTracker, TestTime))
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
        public IEnumerator OnUpdated()
        {
            bool wasCalled = false;
            List<float> deltas = new List<float>();

            yield return CreateTester()
                .Act(() =>
                    CreateAnimation(TestTime)
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
        public IEnumerator OnLoopStarted()
        {
            const int expectedLoopCount = 2;
            int loopCount = 0;
            float controlTracker = 0;
            float control = 0;
            const float expected = 1f;
            const float time = TestTime / expectedLoopCount;

            yield return CreateTester()
                .Act(() =>
                    CreateAnimation(time)
                        .SetLoopCount(expectedLoopCount)
                        .OnUpdated(t => controlTracker = GetUnitValue(t, controlTracker, time))
                        .OnLoopStarted((_) => { loopCount++; control = controlTracker; })
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expectedLoopCount, loopCount);
                    FlowEntAssert.AreEqual(expected, control);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnLoopCompleted()
        {
            const int expectedLoopCount = 2;
            int loopCount = 0;
            float controlTracker = 0;
            float control = 0;
            const float expected = 1f;
            const float time = TestTime / expectedLoopCount;

            yield return CreateTester()
                .Act(() =>
                    CreateAnimation(time)
                        .SetLoopCount(expectedLoopCount)
                        .OnUpdated(t => controlTracker = GetUnitValue(t, controlTracker, time))
                        .OnLoopCompleted((_) => { loopCount++; control = controlTracker; })
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expectedLoopCount, loopCount);
                    FlowEntAssert.AreEqual(expected, control);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnLoopCompleted_Infinite()
        {
            const int expectedLoopCount = 2;
            int loopCount = 0;
            float controlTracker = 0;
            float control = 0;
            const float expected = 1f;
            const float time = TestTime / expectedLoopCount;

            yield return CreateTester()
                .Act(() =>
                {
                    TAnimation animation = (TAnimation)CreateAnimation(time)
                        .SetLoopCount(expectedLoopCount)
                        .OnUpdated(t => controlTracker = GetUnitValue(t, controlTracker, time))
                        .OnLoopCompleted((_) => { loopCount++; control = controlTracker; })
                        .Start();
                    return CreateAnimation(TestTime).OnCompleted(() => animation.Stop()).Start();
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expectedLoopCount, loopCount);
                    FlowEntAssert.AreEqual(expected, control);
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
                        .OnUpdated(t => controlTracker = GetUnitValue(t, controlTracker, TestTime))
                        .OnCompleting(() => { wasCalled = true; control = controlTracker; })
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(wasCalled);
                    FlowEntAssert.AreEqual(expected, control);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnCompleted()
        {
            bool wasCalled = false;
            float controlTracker = 0f;
            float control = 0f;
            const float expected = 1f;

            yield return CreateTester()
                .Act(() =>
                    CreateAnimation(TestTime)
                        .OnUpdated(t => controlTracker = GetUnitValue(t, controlTracker, TestTime))
                        .OnCompleted(() => { wasCalled = true; control = controlTracker; })
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(wasCalled);
                    FlowEntAssert.AreEqual(expected, control);
                })
                .Run();
        }
    }
}
