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
                .AssertTime(time * tweens * innerFlows)
                .Run();
        }

        #endregion

        #region LoopCount

        [UnityTest]
        public IEnumerator LoopCount()
        {
            const float time = 0.15f;
            const int tweens = 5;
            const int loopCount = 5;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow();

                    for (int i = 0; i < tweens; i++)
                    {
                        flow.Queue(new Tween().SetTime(time));
                    }

                    flow.SetLoopCount(loopCount);
                    return flow.Start();
                })
                .AssertTime(time * tweens * loopCount)
                .Run();
        }

        [UnityTest]
        public IEnumerator LoopCount_ForTween()
        {
            const float tweenTime = 0.15f;
            const int tweens = 5;
            const int loopCount = 5;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow();

                    for (int i = 0; i < tweens; i++)
                    {
                        int x = i;
                        flow.Queue(new Tween().SetTime(tweenTime).SetLoopCount(loopCount));
                    }

                    return flow.Start();
                })
                .AssertTime(tweenTime * tweens * loopCount)
                .Run();
        }

        [UnityTest]
        public IEnumerator LoopCount_WithOptions()
        {
            const float tweenTime = 0.15f;
            const int tweens = 5;
            const int loopCount = 5;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow();

                    for (int i = 0; i < tweens; i++)
                    {
                        flow.Queue(new Tween().SetTime(tweenTime));
                    }

                    flow.SetOptions(new FlowOptions().SetLoopCount(loopCount));
                    return flow.Start();
                })
                .AssertTime(tweenTime * tweens * loopCount)
                .Run();
        }

        [UnityTest]
        public IEnumerator LoopCount_NullValue()
        {
            const float tweenTime = 0.15f;
            int? loopCount = null;
            const int loopCountTries = 5;
            int loopCountCounter = 0;

            yield return CreateTester()
                .Act(() =>
                {
                    new Flow()
                        .Queue(new Tween().SetTime(tweenTime))
                        .OnLoopCompleted((loopsLeft) =>
                        {
                            if (loopsLeft == null)
                            {
                                loopCountCounter++;
                            }
                        })
                        .SetLoopCount(loopCount)
                        .Start();

                    return new Tween(loopCountTries * tweenTime).Start();
                })
                .Assert(() => Assert.AreEqual(loopCountTries, loopCountCounter))
                .AssertTime(loopCountTries * tweenTime)
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
            const float time = 2f;
            const float timeScale = 2f;

            yield return CreateTester()
                .Act(() => new Flow()
                            .Queue(new Tween().SetTime(time))
                            .SetTimeScale(timeScale)
                            .Start())
                .AssertTime(time / timeScale)
                .Run();
        }

        [UnityTest]
        public IEnumerator TimeScale_WithOptions()
        {
            const float time = 2f;
            const float timeScale = 2f;

            yield return CreateTester()
                .Act(() => new Flow()
                            .Queue(new Tween().SetTime(time))
                            .SetOptions(new FlowOptions().SetTimeScale(timeScale))
                            .Start())
                .AssertTime(time / timeScale)
                .Run();
        }

        [UnityTest]
        public IEnumerator TimeScale_ForTween()
        {
            const float time = 2f;
            const float timeScale = 2f;

            yield return CreateTester()
                .Act(() => new Flow()
                            .Queue(new Tween().SetTime(time).SetTimeScale(timeScale))
                            .Start())
                .AssertTime(time / timeScale)
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
            const float time = 2f;
            const int skipFrames = 20;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => new Flow()
                            .SetSkipFrames(skipFrames)
                            .Queue(new Tween().SetTime(time))
                            .OnStarted(() => frameCount = Time.frameCount - frameCountStart)
                            .Start())
                .Assert(() => Assert.AreEqual(skipFrames, frameCount))
                .Run();
        }

        [UnityTest]
        public IEnumerator SkipFrames_WithOptions()
        {
            const float time = 2f;
            const int skipFrames = 20;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => new Flow()
                            .Queue(new Tween().SetTime(time))
                            .SetOptions(new FlowOptions().SetSkipFrames(skipFrames))
                            .OnStarted(() => frameCount = Time.frameCount - frameCountStart)
                            .Start())
                .Assert(() => Assert.AreEqual(skipFrames, frameCount))
                .Run();
        }

        [UnityTest]
        public IEnumerator SkipFrames_ForTween()
        {
            const float time = 2f;
            const int skipFrames = 20;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => new Flow()
                            .Queue(new Tween()
                                    .SetTime(time)
                                    .SetSkipFrames(skipFrames)
                                    .OnStarted(() => frameCount = Time.frameCount - frameCountStart))
                            .Start())
                .Assert(() => Assert.AreEqual(skipFrames, frameCount))
                .Run();
        }

        [UnityTest]
        public IEnumerator SkipFrames_AutoStart()
        {
            const float time = 2f;
            const int skipFrames = 20;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => new Flow(true)
                            .SetSkipFrames(skipFrames)
                            .Queue(new Tween().SetTime(time))
                            .OnStarted(() => frameCount = Time.frameCount - frameCountStart))
                .Assert(() => Assert.AreEqual(skipFrames, frameCount))
                .Run();
        }

        [UnityTest]
        public IEnumerator SkipFrames_NegativeValue()
        {
            const float time = 2f;
            const int skipFrames = -20;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => new Flow()
                            .SetSkipFrames(skipFrames)
                            .Queue(new Tween().SetTime(time))
                            .OnStarted(() => frameCount = Time.frameCount - frameCountStart)
                            .Start())
                .Assert(() => Assert.AreEqual(0, frameCount))
                .Run();
        }

        [UnityTest]
        public IEnumerator SkipFrames_StopBeforeStart()
        {
            const int skipFrames = 10000;
            const float time = 0.1f;
            bool onStartedCalled = false;
            Tween controlTween = null;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow()
                            .SetSkipFrames(skipFrames)
                            .OnStarted(() => onStartedCalled = true)
                            .Queue(new Tween(time))
                            .Start();

                    controlTween = new Tween(time).OnCompleted(() => flow.Stop(true)).Start();

                    return flow;
                })
                .AssertTime((stopwatch) => FlowEntAssert.Time(time + controlTween.OverDraft.Value, (float)stopwatch.Elapsed.TotalSeconds))
                .Assert(() => Assert.False(onStartedCalled))
                .Run();
        }

        #endregion

        #region Delay

        [UnityTest]
        public IEnumerator Delay()
        {
            const float time = 0.5f;
            const float delay = 0.5f;

            yield return CreateTester()
                .Act(() => new Flow()
                            .Queue(new Tween().SetTime(time))
                            .SetDelay(delay)
                            .Start())
                .AssertTime(delay + time)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_WithOptions()
        {
            const float time = 0.5f;
            const float delay = 0.5f;

            yield return CreateTester()
                .Act(() => new Flow()
                            .Queue(new Tween().SetTime(time))
                            .SetOptions(new FlowOptions().SetDelay(delay))
                            .Start())
                .AssertTime(delay + time)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_ForTween()
        {
            const float time = 0.5f;
            const float delay = 0.5f;

            yield return CreateTester()
                .Act(() => new Flow()
                            .Queue(new Tween().SetDelay(delay).SetTime(time))
                            .Start())
                .AssertTime(delay + time)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_AutoStart()
        {
            const float time = 0.5f;
            const float delay = 0.5f;

            yield return CreateTester()
                .Act(() => new Flow(true)
                            .Queue(new Tween().SetTime(time))
                            .SetDelay(delay))
                .AssertTime(delay + time)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_NegativeValue()
        {
            const float time = 0.5f;
            const float delay = -0.5f;

            yield return CreateTester()
                .Act(() => new Flow()
                            .Queue(new Tween().SetTime(time))
                            .SetDelay(delay)
                            .Start())
                .AssertTime(time)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_StopBeforeStart()
        {
            const float delay = 1f;
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
