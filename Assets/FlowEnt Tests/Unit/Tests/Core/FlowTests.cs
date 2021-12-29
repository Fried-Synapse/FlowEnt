using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class FlowTests : AbstractEngineTests
    {
        [UnityTest]
        public IEnumerator Name()
        {
            const float time = 0.1f;
            const string name = "Billy";
            Flow flow = default;

            yield return CreateTester()
                .Arrange(() => flow = new Flow().Queue(new Tween(time)))
                .Act(() => flow.SetName(name).Start())
                .AssertTime(time)
                .Assert(() => Assert.AreEqual(name, flow.Name))
                .Run();
        }

        [UnityTest]
        public IEnumerator PlayState_Values()
        {
            bool isBuilding = false;
            bool isWaiting = false;
            bool isPlaying = false;
            bool isPaused = false;
            Flow flow = null;

            yield return CreateTester()
                .Act(() =>
                {
                    flow = new Flow()
                                   .SetDelay(HalfTestTime)
                                   .Queue(new Tween(TestTime));

                    isBuilding = flow.PlayState == PlayState.Building;

                    flow.Start();

                    Flow flowControl = new Flow()
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => isWaiting = flow.PlayState == PlayState.Waiting))
                                    .Queue(new Tween(QuarterTestTime))
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => isPlaying = flow.PlayState == PlayState.Playing))
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => flow.Pause()))
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => isPaused = flow.PlayState == PlayState.Paused))
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => flow.Resume()))
                                    .Start();
                    return flow;
                })
                .AssertTime(DoubleTestTime)
                .Assert(() =>
                {
                    Assert.True(isBuilding);
                    Assert.True(isWaiting);
                    Assert.True(isPlaying);
                    Assert.True(isPaused);
                    Assert.True(flow.PlayState == PlayState.Finished);
                })
                .Run($"Testing different states on {nameof(PlayState_Values)}", DoubleTestTime);
        }

        [UnityTest]
        public IEnumerator Start()
        {
            yield return CreateTester()
                .Act(() => new Flow().Queue(new Tween(TestTime)).Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator StartAsync()
        {
            Task task = null;

            IEnumerator customWaiter()
            {
                while (!task.IsCompleted)
                {
                    yield return null;
                }
            }

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow().Queue(new Tween(TestTime));
                    task = flow.StartAsync();
                    return flow;
                })
                .SetCustomWaiter(customWaiter)
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Start_WaitAsync()
        {
            Task task = null;

            IEnumerator customWaiter()
            {
                while (!task.IsCompleted)
                {
                    yield return null;
                }
            }

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow().Queue(new Tween(TestTime)).Start();
                    task = flow.AsAsync();
                    return flow;
                })
                .SetCustomWaiter(customWaiter)
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Start_Started()
        {
            Exception startException = null;
            Flow flow = null;

            yield return CreateTester()
                .Act(() =>
                {
                    flow = new Flow()
                            .Queue(new Tween(TestTime))
                            .Start();
                    try
                    {
                        flow.Start();
                    }
                    catch (Exception ex)
                    {
                        startException = ex;
                    }

                    return flow;
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(startException != null && startException is AnimationException animationException && animationException.Animation is Flow);
                    Assert.Throws<AnimationException>(() => flow.Start());
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator PauseResume()
        {
            Stopwatch stopwatch = new Stopwatch();

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow()
                                    .Queue(new Tween(HalfTestTime))
                                    .Start();
                    Flow flowControl = new Flow()
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => flow.Pause()))
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => flow.Resume()))
                                    .Start();
                    return flow;
                })
                .AssertTime(ThreeQuartersTestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Stop()
        {
            Stopwatch stopwatch = new Stopwatch();
            bool hasFinished = false;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow().Queue(new Tween(HalfTestTime))
                                        .OnCompleted(() => hasFinished = true)
                                        .Start();

                    return new Flow()
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => flow.Stop()))
                                    .Queue(new Tween(QuarterTestTime))
                                    .Start();
                })
                .Assert(() => Assert.AreEqual(false, hasFinished))
                .Run();
        }

        [UnityTest]
        public IEnumerator Stop_TriggerOnCompleted()
        {
            Stopwatch stopwatch = new Stopwatch();
            bool hasFinished = false;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow().Queue(new Tween(HalfTestTime))
                                        .OnCompleted(() => hasFinished = true)
                                        .Start();

                    return new Flow()
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => flow.Stop(true)))
                                    .Queue(new Tween(QuarterTestTime))
                                    .Start();
                })
                .Assert(() => Assert.AreEqual(true, hasFinished))
                .Run();
        }

        [UnityTest]
        public IEnumerator Conditional()
        {
            bool hasCompletedTrue = false;
            bool hasCompletedFalse = true;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .Conditional(() => true, flow => flow.Queue(new Tween(TestTime).OnCompleted(() => hasCompletedTrue = true)))
                                .Conditional(() => false, flow => flow.Queue(new Tween(TestTime).OnCompleted(() => hasCompletedFalse = false)))
                                .Start();
                })
                .AssertTime(TestTime)
                .Assert(() => Assert.True(hasCompletedTrue))
                .Assert(() => Assert.True(hasCompletedFalse))
                .Run();
        }

        #region Queue

        [UnityTest]
        public IEnumerator Queue()
        {
            bool hasCompleted = false;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .Queue(new Tween(TestTime).OnCompleted(() => hasCompleted = true))
                                .Start();
                })
                .AssertTime(TestTime)
                .Assert(() => Assert.True(hasCompleted))
                .Run();
        }

        [UnityTest]
        public IEnumerator QueueEmptyTween()
        {
            bool hasCompleted = false;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .Queue(new Tween(HalfTestTime))
                                .Queue(new Tween(0))
                                .Queue(new Tween(HalfTestTime).OnCompleted(() => hasCompleted = true))
                                .Start();
                })
                .AssertTime(TestTime)
                .Assert(() => Assert.True(hasCompleted))
                .Run();
        }

        [UnityTest]
        public IEnumerator QueueDelay()
        {
            bool hasCompleted = false;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .Queue(new Tween(QuarterTestTime))
                                .QueueDelay(QuarterTestTime)
                                .Queue(new Tween(QuarterTestTime).OnCompleted(() => hasCompleted = true))
                                .Start();
                })
                .AssertTime(ThreeQuartersTestTime)
                .Assert(() => Assert.True(hasCompleted))
                .Run();
        }

        [UnityTest]
        public IEnumerator QueueAwaiter()
        {
            float actualWaitTime = 0f;
            bool hasCompleted = false;
            float awaiterStartTime = -1;
            float awaiterLastUpdateTime = -2;
            float awaiterCompleteTimeTime = -3;
            float tweenStartTime = -4;

            yield return CreateTester()
                .Act(() =>
                {
                    bool wait = true;
                    Tween tween = new Tween(HalfTestTime);
                    tween.OnCompleted(() =>
                    {
                        wait = false;
                        actualWaitTime = HalfTestTime + tween.OverDraft.Value;
                    });
                    tween.Start();
                    return new Flow()
                                .QueueAwaiter(new CallbackFlowAwaiter(() => wait)
                                    .OnStarted(() => awaiterStartTime = Time.time)
                                    .OnUpdated((_) => awaiterLastUpdateTime = Time.time)
                                    .OnCompleted(() => awaiterCompleteTimeTime = Time.time))
                                .Queue(new Tween(HalfTestTime).OnStarted(() => tweenStartTime = Time.time).OnCompleted(() => hasCompleted = true))
                                .Start();
                })
                .AssertTime(() => actualWaitTime + HalfTestTime)
                .Assert(() =>
                {
                    Assert.True(hasCompleted);
                    Assert.LessOrEqual(awaiterStartTime, awaiterLastUpdateTime);
                    Assert.LessOrEqual(awaiterLastUpdateTime, awaiterCompleteTimeTime);
                    Assert.LessOrEqual(awaiterCompleteTimeTime, tweenStartTime);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator QueueCallbackAwaiter()
        {
            float actualWaitTime = 0f;
            bool hasCompleted = false;

            yield return CreateTester()
                .Act(() =>
                {
                    bool wait = true;
                    Tween tween = new Tween(HalfTestTime);
                    tween.OnCompleted(() =>
                    {
                        wait = false;
                        actualWaitTime = HalfTestTime + tween.OverDraft.Value;
                    });
                    tween.Start();
                    return new Flow()
                                .QueueAwaiter(() => wait)
                                .Queue(new Tween(HalfTestTime).OnCompleted(() => hasCompleted = true))
                                .Start();
                })
                .AssertTime(() => actualWaitTime + HalfTestTime)
                .Assert(() => Assert.True(hasCompleted))
                .Run();
        }

        [UnityTest]
        public IEnumerator QueueTaskAwaiter()
        {
            bool hasCompleted = false;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .QueueAwaiter(Task.Delay((int)(HalfTestTime * 1000)))
                                .Queue(new Tween(HalfTestTime).OnCompleted(() => hasCompleted = true))
                                .Start();
                })
                .AssertTime(() => TestTime)
                .Assert(() => Assert.True(hasCompleted))
                .Run();
        }

        #endregion

        #region QueueDeferred

        [UnityTest]
        public IEnumerator QueueDeferred()
        {
            int index = 0;
            int deferredIndex = 0;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .Queue(new Tween(QuarterTestTime).OnCompleted(() => index = 1))
                                .QueueDeferred(() => { deferredIndex = 2; return new Tween(QuarterTestTime); })
                                .Start();
                })
                .AssertTime(HalfTestTime)
                .Assert(() => Assert.Less(index, deferredIndex))
                .Run();
        }

        #endregion

        #region At

        [UnityTest]
        public IEnumerator At()
        {
            bool hasCompleted = false;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .At(0f, new Tween(TestTime).OnCompleted(() => hasCompleted = true))
                                .Start();
                })
                .AssertTime(TestTime)
                .Assert(() => Assert.True(hasCompleted))
                .Run();
        }

        #endregion

        #region AtDeferred

        [UnityTest]
        public IEnumerator AtDeferred()
        {
            int index = 0;
            int deferredIndex = 0;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .At(0f, new Tween(QuarterTestTime).OnCompleted(() => index = 1))
                                .AtDeferred(HalfTestTime, () => { deferredIndex = 2; return new Tween(QuarterTestTime); })
                                .Start();
                })
                .AssertTime(ThreeQuartersTestTime)
                .Assert(() => Assert.Less(index, deferredIndex))
                .Run();
        }

        #endregion

        [UnityTest]
        public IEnumerator Order()
        {
            const float expectedTime = QuarterTestTime * 2.5f;
            const float start1 = TestTime / 8f;
            const float start2 = TestTime / 5.7f;
            List<int> orderedSequence = new List<int>();

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .Queue(new Tween(QuarterTestTime).OnStarting(() => orderedSequence.Add(1)))
                                .Queue(new Echo(QuarterTestTime).OnStarting(() => orderedSequence.Add(4)))
                                .At(start1, new Echo(QuarterTestTime).OnStarting(() => orderedSequence.Add(2)))
                                .Queue(new Tween(QuarterTestTime).OnStarting(() => orderedSequence.Add(5)))
                                .At(start2, new Tween(QuarterTestTime).OnStarting(() => orderedSequence.Add(3)))
                                .Start();
                })
                .AssertTime(expectedTime)
                .Assert(() => Assert.IsTrue(orderedSequence.SequenceEqual(orderedSequence.OrderBy(v => v))))
                .Run();
        }

        [UnityTest]
        public IEnumerator Reset()
        {
            const int runs = 3;
            int runned = 0;
            Flow flow = null;

            yield return CreateTester()
                .Act(() =>
                {
                    flow = new Flow()
                        .Queue(new Tween(TestTime / runs))
                        .OnCompleted(() =>
                        {
                            runned++;
                            if (runned < runs)
                            {
                                flow.Reset().Start();
                            }
                        })
                        .Start();
                    return new Tween(TestTime).Start();
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(PlayState.Finished, flow.PlayState);
                    Assert.AreEqual(runs, runned);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ResetUsingFlow()
        {
            Flow flow = null;

            yield return CreateTester()
                .Act(() =>
                {
                    flow = new Flow()
                        .Queue(new Tween(TestTime / 4f))
                        .Queue(new Tween(TestTime / 4f));
                    return new Flow()
                        .Queue(flow)
                        .QueueDeferred(() => flow.Reset())
                        .Start();
                })
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(PlayState.Finished, flow.PlayState))
                .Run();
        }
    }
}
