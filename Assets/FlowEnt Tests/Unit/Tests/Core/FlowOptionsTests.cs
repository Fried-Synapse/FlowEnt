using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class FlowOptionsTests : AbstractEngineTests
    {
        #region Time

        [UnityTest]
        public IEnumerator Time_ForTween()
        {
            const float time = 0.05f;
            const int tweens = 10;
            const int innerFlows = 10;
            const float testTime = time * tweens * innerFlows;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow();

                    for (int i = 0; i < innerFlows; i++)
                    {
                        Flow innerFlow = new Flow();
                        flow.Queue(innerFlow);
                        for (int j = 0; j < tweens; j++)
                        {
                            innerFlow.Queue(new Tween().SetTime(time));
                        }
                    }

                    return flow.Start();
                })
                .AssertTime(testTime)
                .Run($"Tweens inside flows inside flow on {nameof(Time_ForTween)}", testTime);
        }

        #endregion

        #region LoopCount

        [UnityTest]
        public IEnumerator LoopCount()
        {
            const int tweens = 3;
            const int loopCount = 3;
            const float testTime = QuarterTestTime * tweens * loopCount;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow();

                    for (int i = 0; i < tweens; i++)
                    {
                        flow.Queue(new Tween().SetTime(QuarterTestTime));
                    }

                    flow.SetLoopCount(loopCount);
                    return flow.Start();
                })
                .AssertTime(testTime)
                .Run($"Loop inside queued tweens on {nameof(LoopCount)}", testTime);
        }

        [UnityTest]
        public IEnumerator LoopCount_ForTween()
        {
            const int tweens = 3;
            const int loopCount = 3;
            const float testTime = QuarterTestTime * tweens * loopCount;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow();

                    for (int i = 0; i < tweens; i++)
                    {
                        int x = i;
                        flow.Queue(new Tween().SetTime(QuarterTestTime).SetLoopCount(loopCount));
                    }

                    return flow.Start();
                })
                .AssertTime(testTime)
                .Run($"Loop inside queued tweens on {nameof(LoopCount_ForTween)}", testTime);
        }

        [UnityTest]
        public IEnumerator LoopCount_WithOptions()
        {
            const int tweens = 3;
            const int loopCount = 3;
            const float testTime = QuarterTestTime * tweens * loopCount;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow();

                    for (int i = 0; i < tweens; i++)
                    {
                        flow.Queue(new Tween().SetTime(QuarterTestTime));
                    }

                    flow.SetOptions(new FlowOptions().SetLoopCount(loopCount));
                    return flow.Start();
                })
                .AssertTime(QuarterTestTime * tweens * loopCount)
                .Run($"Loop inside queued tweens on {nameof(LoopCount_WithOptions)}", testTime);
        }

        [UnityTest]
        public IEnumerator LoopCount_NullValue()
        {
            int? loopCount = null;
            const int loopCountTries = 4;
            int loopCountCounter = 0;

            yield return CreateTester()
                .Act(() =>
                {
                    new Flow()
                        .Queue(new Tween().SetTime(QuarterTestTime))
                        .OnLoopCompleted((loopsLeft) =>
                        {
                            if (loopsLeft == null)
                            {
                                loopCountCounter++;
                            }
                        })
                        .SetLoopCount(loopCount)
                        .Start();

                    return new Tween(TestTime).Start();
                })
                .Assert(() => Assert.AreEqual(loopCountTries, loopCountCounter))
                .AssertTime(TestTime)
                .Run();
        }

        [Test]
        public void LoopCount_NegativeValue()
        {
            const int loopCountZero = 0;
            const int loopCountNegative = -1;

            Assert.Throws<ArgumentException>(() => new Flow().SetLoopCount(loopCountZero));
            Assert.Throws<ArgumentException>(() => new Flow(new FlowOptions() { LoopCount = loopCountZero }));
            Assert.Throws<ArgumentException>(() => new Flow(new FlowOptions().SetLoopCount(loopCountZero)));

            Assert.Throws<ArgumentException>(() => new Flow().SetLoopCount(loopCountNegative));
            Assert.Throws<ArgumentException>(() => new Flow(new FlowOptions() { LoopCount = loopCountNegative }));
            Assert.Throws<ArgumentException>(() => new Flow(new FlowOptions().SetLoopCount(loopCountNegative)));
        }

        #endregion

        #region TimeScale

        [UnityTest]
        public IEnumerator TimeScale()
        {
            const float timeScale = 2f;

            yield return CreateTester()
                .Act(() => new Flow()
                            .Queue(new Tween().SetTime(TestTime))
                            .SetTimeScale(timeScale)
                            .Start())
                .AssertTime(TestTime / timeScale)
                .Run();
        }

        [UnityTest]
        public IEnumerator TimeScale_WithOptions()
        {
            const float timeScale = 2f;

            yield return CreateTester()
                .Act(() => new Flow()
                            .Queue(new Tween().SetTime(TestTime))
                            .SetOptions(new FlowOptions().SetTimeScale(timeScale))
                            .Start())
                .AssertTime(TestTime / timeScale)
                .Run();
        }

        [UnityTest]
        public IEnumerator TimeScale_ForTween()
        {
            const float timeScale = 2f;

            yield return CreateTester()
                .Act(() => new Flow()
                            .Queue(new Tween().SetTime(TestTime).SetTimeScale(timeScale))
                            .Start())
                .AssertTime(TestTime / timeScale)
                .Run();
        }

        [Test]
        public void TimeScale_NegativeValue()
        {
            const float timeScale = -1f;

            Assert.Throws<ArgumentException>(() => new Flow().SetTimeScale(timeScale));
            Assert.Throws<ArgumentException>(() => new Flow(new FlowOptions() { TimeScale = timeScale }));
            Assert.Throws<ArgumentException>(() => new Flow(new FlowOptions().SetTimeScale(timeScale)));
        }

        #endregion

        #region SkipFrames

        [UnityTest]
        public IEnumerator SkipFrames()
        {
            const int skipFrames = 20;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => new Flow()
                            .SetSkipFrames(skipFrames)
                            .Queue(new Tween().SetTime(TestTime))
                            .OnStarted(() => frameCount = Time.frameCount - frameCountStart)
                            .Start())
                .Assert(() => Assert.AreEqual(skipFrames, frameCount))
                .Run();
        }

        [UnityTest]
        public IEnumerator SkipFrames_WithOptions()
        {
            const int skipFrames = 20;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => new Flow()
                            .Queue(new Tween().SetTime(TestTime))
                            .SetOptions(new FlowOptions().SetSkipFrames(skipFrames))
                            .OnStarted(() => frameCount = Time.frameCount - frameCountStart)
                            .Start())
                .Assert(() => Assert.AreEqual(skipFrames, frameCount))
                .Run();
        }

        [UnityTest]
        public IEnumerator SkipFrames_ForTween()
        {
            const int skipFrames = 20;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => new Flow()
                            .Queue(new Tween()
                                    .SetTime(TestTime)
                                    .SetSkipFrames(skipFrames)
                                    .OnStarted(() => frameCount = Time.frameCount - frameCountStart))
                            .Start())
                .Assert(() => Assert.AreEqual(skipFrames, frameCount))
                .Run();
        }

        [UnityTest]
        public IEnumerator SkipFrames_AutoStart()
        {
            const int skipFrames = 20;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => new Flow(true)
                            .SetSkipFrames(skipFrames)
                            .Queue(new Tween().SetTime(TestTime))
                            .OnStarted(() => frameCount = Time.frameCount - frameCountStart))
                .Assert(() => Assert.AreEqual(skipFrames, frameCount))
                .Run();
        }

        [UnityTest]
        public IEnumerator SkipFrames_NegativeValue()
        {
            const int skipFrames = -20;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => new Flow()
                            .SetSkipFrames(skipFrames)
                            .Queue(new Tween().SetTime(TestTime))
                            .OnStarted(() => frameCount = Time.frameCount - frameCountStart)
                            .Start())
                .Assert(() => Assert.AreEqual(0, frameCount))
                .Run();
        }

        [UnityTest]
        public IEnumerator SkipFrames_StopBeforeStart()
        {
            const int skipFrames = 10000;
            bool onStartedCalled = false;
            Tween controlTween = null;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow()
                            .SetSkipFrames(skipFrames)
                            .OnStarted(() => onStartedCalled = true)
                            .Queue(new Tween(QuarterTestTime))
                            .Start();

                    controlTween = new Tween(QuarterTestTime).OnCompleted(() => flow.Stop(true)).Start();

                    return flow;
                })
                .AssertTime((stopwatch) => FlowEntAssert.Time(QuarterTestTime + controlTween.OverDraft.Value, (float)stopwatch.Elapsed.TotalSeconds))
                .Assert(() => Assert.False(onStartedCalled))
                .Run();
        }

        #endregion

        #region Delay

        [UnityTest]
        public IEnumerator Delay()
        {
            yield return CreateTester()
                .Act(() => new Flow()
                            .Queue(new Tween().SetTime(HalfTestTime))
                            .SetDelay(HalfTestTime)
                            .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_WithOptions()
        {
            yield return CreateTester()
                .Act(() => new Flow()
                            .Queue(new Tween().SetTime(HalfTestTime))
                            .SetOptions(new FlowOptions().SetDelay(HalfTestTime))
                            .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_ForTween()
        {
            yield return CreateTester()
                .Act(() => new Flow()
                            .Queue(new Tween().SetDelay(HalfTestTime).SetTime(HalfTestTime))
                            .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_AutoStart()
        {
            yield return CreateTester()
                .Act(() => new Flow(true)
                            .Queue(new Tween().SetTime(HalfTestTime))
                            .SetDelay(HalfTestTime))
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_NegativeValue()
        {
            const float delay = -2;

            yield return CreateTester()
                .Act(() => new Flow()
                            .Queue(new Tween().SetTime(TestTime))
                            .SetDelay(delay)
                            .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_StopBeforeStart()
        {
            const float delay = TestTime;
            const float time = delay / 2;
            bool onStartedCalled = false;
            Tween controlTween = null;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow()
                            .OnStarted(() => onStartedCalled = true)
                            .Queue(new Tween(time))
                            .SetDelay(delay)
                            .Start();

                    controlTween = new Tween(time).OnCompleted(() => flow.Stop(true)).Start();

                    return flow;
                })
                .AssertTime((stopwatch) => FlowEntAssert.Time(time + controlTween.OverDraft.Value, (float)stopwatch.Elapsed.TotalSeconds))
                .Assert(() => Assert.False(onStartedCalled))
                .Run();
        }

        #endregion

    }
}
