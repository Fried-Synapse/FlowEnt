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
        public IEnumerator PlayState_Values()
        {
            const float time = 1f;
            const float delay = 1f;
            bool isBuilding = false;
            bool isWaiting = false;
            bool isPlaying = false;
            bool isPaused = false;
            Flow flow = null;

            yield return CreateTester()
                .Act(() =>
                {
                    flow = new Flow()
                                   .SetDelay(delay)
                                   .Queue(new Tween(time));

                    isBuilding = flow.PlayState == PlayState.Building;

                    flow.Start();

                    Flow flowControl = new Flow()
                                    .Queue(new Tween(delay / 2f).OnCompleted(() => isWaiting = flow.PlayState == PlayState.Waiting))
                                    .Queue(new Tween(delay / 2f))
                                    .Queue(new Tween(time / 4f).OnCompleted(() => isPlaying = flow.PlayState == PlayState.Playing))
                                    .Queue(new Tween(time / 4f).OnCompleted(() => flow.Pause()))
                                    .Queue(new Tween(time / 4f).OnCompleted(() => isPaused = flow.PlayState == PlayState.Paused))
                                    .Queue(new Tween(time / 4f).OnCompleted(() => flow.Resume()))
                                    .Start();
                    return flow;
                })
                .AssertTime(delay + (time * 1.5f))
                .Assert(() =>
                {
                    Assert.True(isBuilding);
                    Assert.True(isWaiting);
                    Assert.True(isPlaying);
                    Assert.True(isPaused);
                    Assert.True(flow.PlayState == PlayState.Finished);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator Start()
        {
            const float time = 1f;

            yield return CreateTester()
                .Act(() => new Flow().Queue(new Tween(time)).Start())
                .AssertTime(time)
                .Run();
        }

        [UnityTest]
        public IEnumerator StartAsync()
        {
            const float time = 1f;
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
                    Flow flow = new Flow().Queue(new Tween(time));
                    task = flow.StartAsync();
                    return flow;
                })
                .SetCustomWaiter(customWaiter)
                .AssertTime(time)
                .Run();
        }

        [UnityTest]
        public IEnumerator Start_WaitAsync()
        {
            const float time = 1f;
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
                    Flow flow = new Flow().Queue(new Tween(time)).Start();
                    task = flow.AsAsync();
                    return flow;
                })
                .SetCustomWaiter(customWaiter)
                .AssertTime(time)
                .Run();
        }

        [UnityTest]
        public IEnumerator Start_Started()
        {
            const float time = 1f;
            Exception startException = null;
            Flow flow = null;

            yield return CreateTester()
                .Act(() =>
                {
                    flow = new Flow()
                            .Queue(new Tween(time))
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
                .AssertTime(time)
                .Assert(() =>
                {
                    Assert.True(startException != null && startException is FlowException);
                    Assert.Throws<FlowException>(() => flow.Start());
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator PauseResume()
        {
            const float time = 2f;
            Stopwatch stopwatch = new Stopwatch();

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow()
                                    .Queue(new Tween(time))
                                    .Start();
                    Flow flowControl = new Flow()
                                    .Queue(new Tween(time / 2f).OnCompleted(() => flow.Pause()))
                                    .Queue(new Tween(time / 2f).OnCompleted(() => flow.Resume()))
                                    .Start();
                    return flow;
                })
                .AssertTime(time * 1.5f)
                .Run();
        }

        [UnityTest]
        public IEnumerator Stop()
        {
            const float testTime = 1f;
            Stopwatch stopwatch = new Stopwatch();
            bool hasFinished = false;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow().Queue(new Tween(testTime))
                                        .OnCompleted(() => hasFinished = true)
                                        .Start();

                    return new Flow()
                                    .Queue(new Tween(testTime / 2f).OnCompleted(() => flow.Stop()))
                                    .Queue(new Tween(testTime))
                                    .Start();
                })
                .Assert(() => Assert.AreEqual(false, hasFinished))
                .Run();
        }

        [UnityTest]
        public IEnumerator Stop_TriggerOnCompleted()
        {
            const float testTime = 1f;
            Stopwatch stopwatch = new Stopwatch();
            bool hasFinished = false;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow().Queue(new Tween(testTime))
                                        .OnCompleted(() => hasFinished = true)
                                        .Start();

                    return new Flow()
                                    .Queue(new Tween(testTime / 2f).OnCompleted(() => flow.Stop(true)))
                                    .Queue(new Tween(testTime))
                                    .Start();
                })
                .Assert(() => Assert.AreEqual(true, hasFinished))
                .Run();
        }

        [UnityTest]
        public IEnumerator Conditional()
        {
            const float time = 1f;
            bool hasCompletedTrue = false;
            bool hasCompletedFalse = true;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .Conditional(() => true, flow => flow.Queue(new Tween(time).OnCompleted(() => hasCompletedTrue = true)))
                                .Conditional(() => false, flow => flow.Queue(new Tween(time).OnCompleted(() => hasCompletedFalse = false)))
                                .Start();
                })
                .AssertTime(time)
                .Assert(() => Assert.True(hasCompletedTrue))
                .Assert(() => Assert.True(hasCompletedFalse))
                .Run();
        }

        #region Queue

        [UnityTest]
        public IEnumerator Queue()
        {
            const float time = 1f;
            bool hasCompleted = false;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .Queue(new Tween(time).OnCompleted(() => hasCompleted = true))
                                .Start();
                })
                .AssertTime(time)
                .Assert(() => Assert.True(hasCompleted))
                .Run();
        }

        [UnityTest]
        public IEnumerator QueueEmptyTwen()
        {
            const float time = 1f;
            bool hasCompleted = false;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .Queue(new Tween(time / 2))
                                .Queue(new Tween(0))
                                .Queue(new Tween(time / 2).OnCompleted(() => hasCompleted = true))
                                .Start();
                })
                .AssertTime(time)
                .Assert(() => Assert.True(hasCompleted))
                .Run();
        }

        [UnityTest]
        public IEnumerator QueueDelay()
        {
            const float time = 0.5f;
            const float delay = 0.3f;
            bool hasCompleted = false;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .Queue(new Tween(time))
                                .QueueDelay(delay)
                                .Queue(new Tween(time).OnCompleted(() => hasCompleted = true))
                                .Start();
                })
                .AssertTime((time * 2) + delay)
                .Assert(() => Assert.True(hasCompleted))
                .Run();
        }

        [UnityTest]
        public IEnumerator QueueAwaiter()
        {
            const float time = 0.3f;
            const float waitTime = 0.5f;
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
                    Tween tween = new Tween(waitTime);
                    tween.OnCompleted(() =>
                    {
                        wait = false;
                        actualWaitTime = waitTime + tween.OverDraft.Value;
                    });
                    tween.Start();
                    return new Flow()
                                .QueueAwaiter(new CallbackFlowAwaiter(() => wait)
                                    .OnStarted(() => awaiterStartTime = Time.time)
                                    .OnUpdated((_) => awaiterLastUpdateTime = Time.time)
                                    .OnCompleted(() => awaiterCompleteTimeTime = Time.time))
                                .Queue(new Tween(time).OnStarted(() => tweenStartTime = Time.time).OnCompleted(() => hasCompleted = true))
                                .Start();
                })
                .AssertTime(() => actualWaitTime + time)
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
            const float time = 0.3f;
            const float waitTime = 0.5f;
            float actualWaitTime = 0f;
            bool hasCompleted = false;

            yield return CreateTester()
                .Act(() =>
                {
                    bool wait = true;
                    Tween tween = new Tween(waitTime);
                    tween.OnCompleted(() =>
                    {
                        wait = false;
                        actualWaitTime = waitTime + tween.OverDraft.Value;
                    });
                    tween.Start();
                    return new Flow()
                                .QueueAwaiter(() => wait)
                                .Queue(new Tween(time).OnCompleted(() => hasCompleted = true))
                                .Start();
                })
                .AssertTime(() => actualWaitTime + time)
                .Assert(() => Assert.True(hasCompleted))
                .Run();
        }

        [UnityTest]
        public IEnumerator QueueTaskAwaiter()
        {
            const float time = 0.3f;
            const float waitTime = 0.5f;
            bool hasCompleted = false;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .QueueAwaiter(Task.Delay((int)(waitTime * 1000)))
                                .Queue(new Tween(time).OnCompleted(() => hasCompleted = true))
                                .Start();
                })
                .AssertTime(() => waitTime + time)
                .Assert(() => Assert.True(hasCompleted))
                .Run();
        }

        #endregion

        #region QueueDeferred

        [UnityTest]
        public IEnumerator QueueDeferred()
        {
            const float time = 0.5f;
            int index = 0;
            int defferedIndex = 0;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .Queue(new Tween(time).OnCompleted(() => index = 1))
                                .QueueDeferred(() => { defferedIndex = 2; return new Tween(time); })
                                .Start();
                })
                .AssertTime(time * 2)
                .Assert(() => Assert.Less(index, defferedIndex))
                .Run();
        }

        #endregion

        #region At

        [UnityTest]
        public IEnumerator At()
        {
            const float time = 1f;
            bool hasCompleted = false;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .At(0f, new Tween(time).OnCompleted(() => hasCompleted = true))
                                .Start();
                })
                .AssertTime(time)
                .Assert(() => Assert.True(hasCompleted))
                .Run();
        }

        #endregion

        #region AtDeferred

        [UnityTest]
        public IEnumerator AtDeferred()
        {
            const float time = 0.5f;
            const float defferedTime = 1f;
            int index = 0;
            int defferedIndex = 0;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .At(0f, new Tween(time).OnCompleted(() => index = 1))
                                .AtDeferred(defferedTime, () => { defferedIndex = 2; return new Tween(time); })
                                .Start();
                })
                .AssertTime(time + defferedTime)
                .Assert(() => Assert.Less(index, defferedIndex))
                .Run();
        }

        #endregion

        [UnityTest]
        public IEnumerator Order()
        {
            const float time = 1f;
            const float expectedTime = 2.5f;
            List<int> orderedSequence = new List<int>();

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .Queue(new Tween(time).OnStarting(() => orderedSequence.Add(1)))
                                .Queue(new Tween(time).OnStarting(() => orderedSequence.Add(4)))
                                .At(0.5f, new Tween(time).OnStarting(() => orderedSequence.Add(2)))
                                .Queue(new Tween(time).OnStarting(() => orderedSequence.Add(5)))
                                .At(0.7f, new Tween(time).OnStarting(() => orderedSequence.Add(3)))
                                .Start();
                })
                .AssertTime(expectedTime)
                .Assert(() => Assert.IsTrue(orderedSequence.SequenceEqual(orderedSequence.OrderBy(v => v))))
                .Run();
        }



        [UnityTest]
        public IEnumerator Reset()
        {
            const float testTime = 1f;
            const int runs = 3;
            int runned = 0;
            Flow flow = null;

            yield return CreateTester()
                .Act(() =>
                {
                    flow = new Flow()
                        .Queue(new Tween(testTime / runs))
                        .OnCompleted(() =>
                        {
                            runned++;
                            if (runned < runs)
                            {
                                flow.Reset().Start();
                            }
                        })
                        .Start();
                    return new Tween(testTime).Start();
                })
                .AssertTime(testTime)
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
            const float testTime = 1f;
            Flow flow = null;

            yield return CreateTester()
                .Act(() =>
                {
                    flow = new Flow()
                        .Queue(new Tween(testTime / 4f))
                        .Queue(new Tween(testTime / 4f));
                    return new Flow()
                        .Queue(flow)
                        .QueueDeferred(() => flow.Reset())
                        .Start();
                })
                .AssertTime(testTime)
                .Assert(() => Assert.AreEqual(PlayState.Finished, flow.PlayState))
                .Run();
        }
    }
}
