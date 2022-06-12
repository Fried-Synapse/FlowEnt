using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriedSynapse.FlowEnt.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class FlowTests : AbstractAnimationTests<Flow>
    {
        protected override Flow CreateAnimation(float testTime)
            => new Flow().Queue(new Tween(testTime));

        [UnityTest]
        public IEnumerator Builder()
        {
            Flow flow = default;
            yield return CreateTester()
                .Act(() =>
                //HACK It think this returns something for some fucking reason
#pragma warning disable RCS1021 
                {
                    flow = Variables.Flow.Build();
                })
#pragma warning restore RCS1021
                .Assert(() =>
                {
                    IList updatableWrappers = flow.GetFieldValue<IList>("updatableWrappersQueue");
                    Assert.AreEqual(2, updatableWrappers.Count);
                    Assert.IsTrue(updatableWrappers[0].GetFieldValue<AbstractUpdatable>("updatable") is Tween);
                    Assert.IsTrue(updatableWrappers[1].GetFieldValue<AbstractUpdatable>("updatable") is Echo);
                })
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
                                .Queue(default(AbstractAnimation))
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
        public IEnumerator QueueConditional()
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
                        actualWaitTime = HalfTestTime + tween.Overdraft.Value;
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
                        actualWaitTime = HalfTestTime + tween.Overdraft.Value;
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
            bool hasCalledDeferred = false;
            bool hasFinishedDeferredAnimation = false;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                        .Queue(new Tween(HalfTestTime))
                        .QueueDeferred(() => null)
                        .QueueDeferred(() =>
                        {
                            hasCalledDeferred = true;
                            return new Tween(HalfTestTime).OnCompleted(() => hasFinishedDeferredAnimation = true);
                        })
                        .Start();
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(hasCalledDeferred);
                    Assert.True(hasFinishedDeferredAnimation);
                })
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
                        .At(0f, default(AbstractAnimation))
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
            bool hasCalledDeferred = false;
            bool hasFinishedDeferredAnimation = false;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                        .AtDeferred(QuarterTestTime, () => null)
                        .AtDeferred(HalfTestTime, () =>
                        {
                            hasCalledDeferred = true;
                            return new Tween(HalfTestTime).OnCompleted(() => hasFinishedDeferredAnimation = true);
                        })
                        .Start();
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(hasCalledDeferred);
                    Assert.True(hasFinishedDeferredAnimation);
                })
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
    }
}
