using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public abstract class AbstractAnimationEventsTests<TAnimation> : AbstractEngineTests
        where TAnimation : AbstractAnimation
    {
        protected abstract TAnimation CreateAnimation(float testTime);
        protected abstract float GetTotalTimeFromUpdate(float t, float previousValue, float loopTime);

        protected static void Assert(UnityEventBase unityEvent, Delegate action)
        {
            if (unityEvent.GetPersistentEventCount() == 0)
            {
                action.Should().BeNull();
            }
            else
            {
                action.Should().NotBeNull();
            }
        }
        
        [UnityTest]
        public IEnumerator OnStarting()
        {
            bool wasCalled = false;
            float timeTracker = 0;
            float timeControl = 0;

            yield return CreateTester()
                .Act(() =>
                    CreateAnimation(TestTime)
                        .OnStarting(() =>
                        {
                            wasCalled = true;
                            timeControl = timeTracker;
                        })
                        .OnUpdated(t => timeTracker = GetTotalTimeFromUpdate(t, timeTracker, TestTime))
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    wasCalled.Should().BeTrue();
                    timeControl.Should().Be(0);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnStarted()
        {
            bool wasCalled = false;
            float timeTracker = 0;
            float timeControl = 0;

            yield return CreateTester()
                .Act(() =>
                    CreateAnimation(TestTime)
                        .OnStarted(() =>
                        {
                            wasCalled = true;
                            timeControl = timeTracker;
                        })
                        .OnUpdated(t => timeTracker = GetTotalTimeFromUpdate(t, timeTracker, TestTime))
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    wasCalled.Should().BeTrue();
                    timeControl.Should().Be(0);
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
                        .OnUpdating(t =>
                        {
                            wasCalled = true;
                            deltas.Add(t);
                        })
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    wasCalled.Should().BeTrue();
                    deltas.Should().HaveCountGreaterThan(0)
                        .And.AllSatisfy(t =>
                            t.Should().BeGreaterOrEqualTo(0)
                                .And.BeLessThanOrEqualTo(1));
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
                        .OnUpdated(t =>
                        {
                            wasCalled = true;
                            deltas.Add(t);
                        })
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    wasCalled.Should().BeTrue();
                    deltas.Should().HaveCountGreaterThan(0)
                        .And.AllSatisfy(t =>
                            t.Should().BeGreaterOrEqualTo(0)
                                .And.BeLessThanOrEqualTo(1));
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnLoopStarted()
        {
            const int expectedLoopCount = 2;
            int loopCount = 0;
            float timeTracker = 0;
            float timeControl = 0;
            const float time = TestTime / expectedLoopCount;

            yield return CreateTester()
                .Act(() =>
                    CreateAnimation(time)
                        .SetLoopCount(expectedLoopCount)
                        .OnUpdated(t => timeTracker = GetTotalTimeFromUpdate(t, timeTracker, time))
                        .OnLoopStarted((_) =>
                        {
                            loopCount++;
                            timeControl = timeTracker;
                        })
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    loopCount.Should().Be(expectedLoopCount);
                    timeControl.Should().BeApproximatelyTime(time * (expectedLoopCount - 1));
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnLoopCompleted()
        {
            const int expectedLoopCount = 2;
            int loopCount = 0;
            float timeTracker = 0;
            float timeControl = 0;
            const float time = TestTime / expectedLoopCount;

            yield return CreateTester()
                .Act(() =>
                    CreateAnimation(time)
                        .SetLoopCount(expectedLoopCount)
                        .OnUpdated(t => timeTracker = GetTotalTimeFromUpdate(t, timeTracker, time))
                        .OnLoopCompleted((_) =>
                        {
                            loopCount++;
                            timeControl = timeTracker;
                        })
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    loopCount.Should().Be(expectedLoopCount);
                    timeControl.Should().BeApproximatelyTime(TestTime);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnLoopCompleted_Infinite()
        {
            const int expectedLoopCount = 2;
            int loopCount = 0;
            float timeTracker = 0;
            float timeControl = 0;
            const float time = TestTime / expectedLoopCount;

            yield return CreateTester()
                .Act(() =>
                {
                    TAnimation animation = (TAnimation)CreateAnimation(time)
                        .SetLoopCount(expectedLoopCount)
                        .OnUpdated(t => timeTracker = GetTotalTimeFromUpdate(t, timeTracker, time))
                        .OnLoopCompleted((_) =>
                        {
                            loopCount++;
                            timeControl = timeTracker;
                        })
                        .Start();
                    return CreateAnimation(TestTime).OnCompleted(() => animation.Stop()).Start();
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    loopCount.Should().Be(expectedLoopCount);
                    timeControl.Should().BeApproximatelyTime(TestTime);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnCompleting()
        {
            bool wasCalled = false;
            float timeTracker = 0;
            float timeControl = 0;

            yield return CreateTester()
                .Act(() =>
                    CreateAnimation(TestTime)
                        .OnUpdated(t => timeTracker = GetTotalTimeFromUpdate(t, timeTracker, TestTime))
                        .OnCompleting(() =>
                        {
                            wasCalled = true;
                            timeControl = timeTracker;
                        })
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    wasCalled.Should().BeTrue();
                    timeControl.Should().BeApproximatelyTime(TestTime);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnCompleted()
        {
            bool wasCalled = false;
            float timeTracker = 0f;
            float timetimeControl = 0f;

            yield return CreateTester()
                .Act(() =>
                    CreateAnimation(TestTime)
                        .OnUpdated(t => timeTracker = GetTotalTimeFromUpdate(t, timeTracker, TestTime))
                        .OnCompleted(() =>
                        {
                            wasCalled = true;
                            timetimeControl = timeTracker;
                        })
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    wasCalled.Should().BeTrue();
                    timetimeControl.Should().BeApproximatelyTime(TestTime);
                })
                .Run();
        }
    }
}