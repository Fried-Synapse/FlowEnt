using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
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
                .Act(() => { flow = Variables.Flow.Build(); })
                .Assert(() =>
                {
                    IList updatableWrappers = flow.GetFieldValue<IList>("updatableWrappersQueue");
                    updatableWrappers.Count.Should().Be(2);
                    updatableWrappers[0].GetFieldValue<AbstractUpdatable>("updatable").Should().BeOfType<Tween>();
                    updatableWrappers[1].GetFieldValue<AbstractUpdatable>("updatable").Should().BeOfType<Echo>();
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
                .Assert(() => hasCompleted.Should().BeTrue())
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
                        .QueueDelay(QuarterTestTime)
                        .Queue(new Tween(QuarterTestTime))
                        .QueueDelay(QuarterTestTime)
                        .Queue(new Tween(QuarterTestTime).OnCompleted(() => hasCompleted = true))
                        .Start();
                })
                .AssertTime(TestTime)
                .Assert(() => hasCompleted.Should().BeTrue())
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
                        .Conditional(() => true,
                            flow => flow.Queue(new Tween(TestTime).OnCompleted(() => hasCompletedTrue = true)))
                        .Conditional(() => false,
                            flow => flow.Queue(new Tween(TestTime).OnCompleted(() => hasCompletedFalse = false)))
                        .Start();
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    hasCompletedTrue.Should().BeTrue();
                    hasCompletedFalse.Should().BeTrue();
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator QueueAwaiter()
        {
            float actualWaitTime = 0f;
            bool hasCompleted = false;
            float awaiterStartTime = -1;
            float awaiterLastUpdateTime = -2;
            float awaiterCompleteTime = -3;
            float tweenStartTime = -4;

            yield return CreateTester()
                .Act(() =>
                {
                    bool wait = true;
                    Tween tween = new(HalfTestTime);
                    tween.OnCompleted(() =>
                    {
                        wait = false;
                        actualWaitTime = HalfTestTime + tween.Overdraft ?? 0;
                    });
                    tween.Start();
                    return new Flow()
                        .QueueAwaiter(new CallbackFlowAwaiter(() => wait)
                            .OnStarted(() => awaiterStartTime = Time.time)
                            .OnUpdated(_ => awaiterLastUpdateTime = Time.time)
                            .OnCompleted(() => awaiterCompleteTime = Time.time))
                        .Queue(new Tween(HalfTestTime).OnStarted(() => tweenStartTime = Time.time)
                            .OnCompleted(() => hasCompleted = true))
                        .Start();
                })
                .AssertTime(() => actualWaitTime + HalfTestTime)
                .Assert(() =>
                {
                    hasCompleted.Should().BeTrue();
                    awaiterStartTime.Should().BeLessThan(awaiterLastUpdateTime);
                    awaiterLastUpdateTime.Should().BeLessThan(awaiterCompleteTime);
                    awaiterCompleteTime.Should().Be(tweenStartTime);
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
                    Tween tween = new(HalfTestTime);
                    tween.OnCompleted(() =>
                    {
                        wait = false;
                        actualWaitTime = HalfTestTime + (tween.Overdraft ?? 0);
                    });
                    tween.Start();
                    return new Flow()
                        .QueueAwaiter(() => wait)
                        .Queue(new Tween(HalfTestTime).OnCompleted(() => hasCompleted = true))
                        .Start();
                })
                .AssertTime(() => actualWaitTime + HalfTestTime)
                .Assert(() => hasCompleted.Should().BeTrue())
                .Run();
        }

        [UnityTest]
        public IEnumerator QueueTaskAwaiter()
        {
            bool hasCompleted = false;

            yield return CreateTester()
                .Act(() => new Flow()
                    .QueueAwaiter(async () => await new Tween(QuarterTestTime).StartAsync())
                    .QueueAwaiter(async () => await new Tween(QuarterTestTime).StartAsync())
                    .Queue(new Tween(HalfTestTime).OnCompleted(() => hasCompleted = true))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() => hasCompleted.Should().BeTrue())
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
                    hasCalledDeferred.Should().BeTrue();
                    hasFinishedDeferredAnimation.Should().BeTrue();
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
                .Assert(() => hasCompleted.Should().BeTrue())
                .Run();
        }

        [UnityTest]
        public IEnumerator AtList()
        {
            bool hasCompleted = false;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                        .At(0f, new List<AbstractAnimation>
                        {
                            null,
                            new Tween(TestTime).OnCompleted(() => hasCompleted = true)
                        })
                        .Start();
                })
                .AssertTime(TestTime)
                .Assert(() => hasCompleted.Should().BeTrue())
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
                    hasCalledDeferred.Should().BeTrue();
                    hasFinishedDeferredAnimation.Should().BeTrue();
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
            List<int> orderedSequence = new();

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
                .Assert(() => orderedSequence.Should().BeInAscendingOrder())
                .Run();
        }
    }
}