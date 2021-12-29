using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class EchoOptionsTests : AbstractEngineTests
    {
        #region Builder

        // [UnityTest]
        // public IEnumerator Builder()
        // {
        //     EchoOptions echoOptions = default;

        //     yield return CreateTester()
        //         .Act(() => echoOptions = Variables.EchoOptionsBuilder.Build())
        //         .Assert(() =>
        //         {
        //             Assert.AreEqual(Variables.EchoOptionsBuilder.Name, echoOptions.Name);
        //             Assert.AreEqual(Variables.EchoOptionsBuilder.AutoStart, echoOptions.AutoStart);
        //             Assert.AreEqual(Variables.EchoOptionsBuilder.SkipFrames, echoOptions.SkipFrames);
        //             Assert.AreEqual(Variables.EchoOptionsBuilder.Delay, echoOptions.Delay);
        //             Assert.AreEqual(Variables.EchoOptionsBuilder.TimeScale, echoOptions.TimeScale);
        //             Assert.AreEqual(Variables.EchoOptionsBuilder.Time, echoOptions.Time);
        //             Assert.AreEqual(EasingFactory.Create(Variables.EchoOptionsBuilder.Easing).GetType().FullName, echoOptions.Easing.GetType().FullName);
        //             if (Variables.EchoOptionsBuilder.IsLoopCountInfinite)
        //             {
        //                 Assert.AreEqual(null, echoOptions.LoopCount);
        //             }
        //             else
        //             {
        //                 Assert.AreEqual(Variables.EchoOptionsBuilder.LoopCount, echoOptions.LoopCount);
        //             }
        //             Assert.AreEqual(Variables.EchoOptionsBuilder.LoopType, echoOptions.LoopType);
        //         })
        //         .Run();
        // }

        #endregion

        #region Timeout

        [UnityTest]
        public IEnumerator Timeout_Default()
        {
            yield return CreateTester()
                .Act(() => new Echo()
                            .SetTimeout(TestTime)
                            .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Timeout_Constructor()
        {
            yield return CreateTester()
                .Act(() => new Echo(TestTime)
                            .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Timeout_WithOptions()
        {
            yield return CreateTester()
                .Act(() => new Echo()
                            .SetOptions(new EchoOptions().SetTimeout(TestTime))
                            .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Timeout_ZeroValue()
        {
            const float timeout = 0f;

            yield return CreateTester()
                .Act(() => new Echo()
                            .SetTimeout(timeout)
                            .Start())
                .AssertTime(timeout)
                .Run();
        }

        [Test]
        public void Time_NegativeValue()
        {
            const float timeout = -2f;

            Assert.Throws<ArgumentException>(() => new Echo(timeout));
            Assert.Throws<ArgumentException>(() => new Echo().SetTimeout(timeout));
            Assert.Throws<ArgumentException>(() => new Echo(new EchoOptions() { Timeout = timeout }));
            Assert.Throws<ArgumentException>(() => new Echo(new EchoOptions().SetTimeout(timeout)));
        }

        #endregion

        #region SetStopCondition

        [UnityTest]
        public IEnumerator SetStopCondition_Timeout()
        {
            yield return CreateTester()
                .Act(() => new Echo()
                            .SetStopCondition((time) => time > TestTime)
                            .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator SetStopCondition_TweenControl()
        {
            yield return CreateTester()
                .Act(() =>
                {
                    bool flag = false;
                    Tween tween = new Tween(TestTime).OnCompleted(() => flag = true).Start();

                    return new Echo()
                        .SetStopCondition((_) => flag)
                        .Start();
                })
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator SetStopCondition_MultiCondition()
        {
            yield return CreateTester()
                .Act(() => new Echo()
                            .SetStopCondition((time) => time > TestTime * 2)
                            .SetStopCondition((time) => time > TestTime)
                            .Start())
                .AssertTime(TestTime)
                .Run();
        }

        #endregion

        #region TimeScale

        [UnityTest]
        public IEnumerator TimeScale()
        {
            const float testTimeScale = 2f;

            yield return CreateTester()
                .Act(() => new Echo()
                            .SetTimeout(TestTime)
                            .SetTimeScale(testTimeScale)
                            .Start())
                .AssertTime(TestTime / testTimeScale)
                .Run();
        }

        [UnityTest]
        public IEnumerator TimeScale_WithOptions()
        {
            const float testTimeScale = 2f;

            yield return CreateTester()
                .Act(() => new Echo()
                            .SetOptions(new EchoOptions().SetTimeScale(testTimeScale))
                            .SetTimeout(TestTime)
                            .Start())
                .AssertTime(TestTime / testTimeScale)
                .Run();
        }

        [Test]
        public void TimeScale_NegativeValue()
        {
            const float testTimeScale = -1f;

            Assert.Throws<ArgumentException>(() => new Echo().SetTimeScale(testTimeScale));
            Assert.Throws<ArgumentException>(() => new Echo(new EchoOptions() { TimeScale = testTimeScale }));
            Assert.Throws<ArgumentException>(() => new Echo(new EchoOptions().SetTimeScale(testTimeScale)));
        }

        #endregion

        #region LoopCount

        [UnityTest]
        public IEnumerator LoopCount()
        {
            const int loopCount = 2;

            yield return CreateTester()
                .Act(() => new Echo(TestTime / loopCount)
                            .SetLoopCount(loopCount)
                            .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator LoopCount_WithOptions()
        {
            const int loopCount = 2;

            yield return CreateTester()
                .Act(() => new Echo()
                            .SetOptions(new EchoOptions().SetTimeout(TestTime / loopCount).SetLoopCount(loopCount))
                            .Start())
                .AssertTime(TestTime)
                .Run();
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
                    new Echo(QuarterTestTime)
                        .OnLoopCompleted((loopsLeft) =>
                        {
                            if (loopsLeft == null)
                            {
                                loopCountCounter++;
                            }
                        })
                        .SetLoopCount(loopCount)
                        .Start();

                    return new Echo(loopCountTries * QuarterTestTime).Start();
                })
                .Assert(() => Assert.AreEqual(loopCountTries, loopCountCounter))
                .AssertTime(loopCountTries * QuarterTestTime)
                .Run();
        }

        [Test]
        public void LoopCount_NegativeValue()
        {
            const int loopCountZero = 0;
            const int loopCountNegative = -1;

            Assert.Throws<ArgumentException>(() => new Echo().SetLoopCount(loopCountZero));
            Assert.Throws<ArgumentException>(() => new Echo(new EchoOptions() { LoopCount = loopCountZero }));
            Assert.Throws<ArgumentException>(() => new Echo(new EchoOptions().SetLoopCount(loopCountZero)));

            Assert.Throws<ArgumentException>(() => new Echo().SetLoopCount(loopCountNegative));
            Assert.Throws<ArgumentException>(() => new Echo(new EchoOptions() { LoopCount = loopCountNegative }));
            Assert.Throws<ArgumentException>(() => new Echo(new EchoOptions().SetLoopCount(loopCountNegative)));
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
                .Act(() => new Echo()
                            .SetSkipFrames(skipFrames)
                            .SetTimeout(TestTime)
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
                .Act(() => new Echo()
                            .SetOptions(new EchoOptions().SetSkipFrames(skipFrames))
                            .SetTimeout(TestTime)
                            .OnStarted(() => frameCount = Time.frameCount - frameCountStart)
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
                .Act(() => new Echo(1f, true)
                            .SetSkipFrames(skipFrames)
                            .SetTimeout(TestTime)
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
                .Act(() => new Echo()
                            .SetSkipFrames(skipFrames)
                            .SetTimeout(TestTime)
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
            Echo controlEcho = null;

            yield return CreateTester()
                .Act(() =>
                {
                    Echo echo = new Echo(time)
                            .SetSkipFrames(skipFrames)
                            .OnStarted(() => onStartedCalled = true)
                            .Start();

                    controlEcho = new Echo(time).OnCompleted(() => echo.Stop(true)).Start();

                    return echo;
                })
                .AssertTime((stopwatch) => FlowEntAssert.Time(time + controlEcho.OverDraft.Value, (float)stopwatch.Elapsed.TotalSeconds))
                .Assert(() => Assert.False(onStartedCalled))
                .Run();
        }

        #endregion

        #region Delay

        [UnityTest]
        public IEnumerator Delay()
        {
            yield return CreateTester()
                .Act(() => new Echo()
                            .SetDelay(HalfTestTime)
                            .SetTimeout(HalfTestTime)
                            .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_AutoStart()
        {
            yield return CreateTester()
                .Act(() => new Echo(1f, true)
                            .SetDelay(HalfTestTime)
                            .SetTimeout(HalfTestTime))
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_WithOptions()
        {
            yield return CreateTester()
                .Act(() => new Echo()
                            .SetOptions(new EchoOptions().SetDelay(HalfTestTime))
                            .SetTimeout(HalfTestTime)
                            .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_NegativeValue()
        {
            const float delay = -2f;

            yield return CreateTester()
                .Act(() => new Echo()
                            .SetDelay(delay)
                            .SetTimeout(TestTime)
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
            Echo controlEcho = null;

            yield return CreateTester()
                .Act(() =>
                {
                    Echo echo = new Echo(time)
                            .OnStarted(() => onStartedCalled = true)
                            .SetDelay(delay)
                            .Start();

                    controlEcho = new Echo(time).OnCompleted(() => echo.Stop(true)).Start();

                    return echo;
                })
                .AssertTime((stopwatch) => FlowEntAssert.Time(time + controlEcho.OverDraft.Value, (float)stopwatch.Elapsed.TotalSeconds))
                .Assert(() => Assert.False(onStartedCalled))
                .Run();
        }

        #endregion
    }
}