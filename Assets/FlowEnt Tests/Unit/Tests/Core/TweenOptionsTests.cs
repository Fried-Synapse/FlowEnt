using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FriedSynapse.FlowEnt.Easings;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class TweenOptionsTests : AbstractEngineTests
    {
        #region Builder

        [UnityTest]
        public IEnumerator Builder()
        {
            TweenOptions tweenOptions = default;

            yield return CreateTester()
                .Act(() => tweenOptions = Variables.TweenOptionsBuilder.Build())
                .Assert(() =>
                {
                    Assert.AreEqual(Variables.TweenOptionsBuilder.Name, tweenOptions.Name);
                    Assert.AreEqual(Variables.TweenOptionsBuilder.AutoStart, tweenOptions.AutoStart);
                    Assert.AreEqual(Variables.TweenOptionsBuilder.SkipFrames, tweenOptions.SkipFrames);
                    Assert.AreEqual(Variables.TweenOptionsBuilder.Delay, tweenOptions.Delay);
                    Assert.AreEqual(Variables.TweenOptionsBuilder.TimeScale, tweenOptions.TimeScale);
                    Assert.AreEqual(Variables.TweenOptionsBuilder.Time, tweenOptions.Time);
                    Assert.AreEqual(EasingFactory.Create(Variables.TweenOptionsBuilder.Easing).GetType().FullName, tweenOptions.Easing.GetType().FullName);
                    if (Variables.TweenOptionsBuilder.IsLoopCountInfinite)
                    {
                        Assert.AreEqual(null, tweenOptions.LoopCount);
                    }
                    else
                    {
                        Assert.AreEqual(Variables.TweenOptionsBuilder.LoopCount, tweenOptions.LoopCount);
                    }
                    Assert.AreEqual(Variables.TweenOptionsBuilder.LoopType, tweenOptions.LoopType);
                })
                .Run();
        }

        #endregion

        #region Time

        [UnityTest]
        public IEnumerator Time_Default()
        {
            const float time = 2f;

            yield return CreateTester()
                .Act(() => new Tween()
                            .SetTime(time)
                            .Start())
                .AssertTime(time)
                .Run();
        }

        [UnityTest]
        public IEnumerator Time_Constructor()
        {
            const float time = 2f;

            yield return CreateTester()
                .Act(() => new Tween(time)
                            .Start())
                .AssertTime(time)
                .Run();
        }

        [UnityTest]
        public IEnumerator Time_WithOptions()
        {
            const float time = 2f;

            yield return CreateTester()
                .Act(() => new Tween()
                            .SetOptions(new TweenOptions().SetTime(time))
                            .Start())
                .AssertTime(time)
                .Run();
        }

        [UnityTest]
        public IEnumerator Time_ZeroValue()
        {
            const float time = 0f;

            yield return CreateTester()
                .Act(() => new Tween()
                            .SetTime(time)
                            .Start())
                .AssertTime(time)
                .Run();
        }

        [Test]
        public void Time_NegativeValue()
        {
            const float time = -2f;

            Assert.Throws<ArgumentException>(() => new Tween(time));
            Assert.Throws<ArgumentException>(() => new Tween().SetTime(time));
            Assert.Throws<ArgumentException>(() => new Tween(new TweenOptions() { Time = time }));
            Assert.Throws<ArgumentException>(() => new Tween(new TweenOptions().SetTime(time)));
        }

        [Test]
        public void Time_InfinityValue()
        {
            const float infinity = 1f / 0f;

            Assert.Throws<ArgumentException>(() => new Tween(infinity));
            Assert.Throws<ArgumentException>(() => new Tween().SetTime(infinity));
            Assert.Throws<ArgumentException>(() => new Tween(new TweenOptions() { Time = infinity }));
            Assert.Throws<ArgumentException>(() => new Tween(new TweenOptions().SetTime(infinity)));
        }

        #endregion

        #region TimeScale

        [UnityTest]
        public IEnumerator TimeScale()
        {
            const float testTime = 2f;
            const float testTimeScale = 2f;

            yield return CreateTester()
                .Act(() => new Tween()
                            .SetTime(testTime)
                            .SetTimeScale(testTimeScale)
                            .Start())
                .AssertTime(testTime / testTimeScale)
                .Run();
        }

        [UnityTest]
        public IEnumerator TimeScale_WithOptions()
        {
            const float testTime = 2f;
            const float testTimeScale = 2f;

            yield return CreateTester()
                .Act(() => new Tween()
                            .SetOptions(new TweenOptions().SetTimeScale(testTimeScale))
                            .SetTime(testTime)
                            .Start())
                .AssertTime(testTime / testTimeScale)
                .Run();
        }

        [Test]
        public void TimeScale_NegativeValue()
        {
            const float testTimeScale = -1f;

            Assert.Throws<ArgumentException>(() => new Tween().SetTimeScale(testTimeScale));
            Assert.Throws<ArgumentException>(() => new Tween(new TweenOptions() { TimeScale = testTimeScale }));
            Assert.Throws<ArgumentException>(() => new Tween(new TweenOptions().SetTimeScale(testTimeScale)));
        }

        #endregion

        #region LoopCount

        [UnityTest]
        public IEnumerator LoopCount()
        {
            const int loopCount = 2;

            yield return CreateTester()
                .Act(() => new Tween(TestTime)
                            .SetLoopCount(loopCount)
                            .Start())
                .AssertTime(loopCount * TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator LoopCount_WithOptions()
        {
            const int loopCount = 2;

            yield return CreateTester()
                .Act(() => new Tween(TestTime)
                            .SetOptions(new TweenOptions().SetLoopCount(loopCount))
                            .Start())
                .AssertTime(loopCount * TestTime)
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
                    new Tween(tweenTime)
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

            Assert.Throws<ArgumentException>(() => new Tween().SetLoopCount(loopCountZero));
            Assert.Throws<ArgumentException>(() => new Tween(new TweenOptions() { LoopCount = loopCountZero }));
            Assert.Throws<ArgumentException>(() => new Tween(new TweenOptions().SetLoopCount(loopCountZero)));

            Assert.Throws<ArgumentException>(() => new Tween().SetLoopCount(loopCountNegative));
            Assert.Throws<ArgumentException>(() => new Tween(new TweenOptions() { LoopCount = loopCountNegative }));
            Assert.Throws<ArgumentException>(() => new Tween(new TweenOptions().SetLoopCount(loopCountNegative)));
        }

        #endregion

        #region LoopType

        [UnityTest]
        public IEnumerator LoopType_PingPong()
        {
            List<float> ascending = new List<float>();
            List<float> descending = new List<float>();
            List<float> current = ascending;

            yield return CreateTester()
                .Act(() => new Tween()
                            .SetLoopType(LoopType.PingPong)
                            .SetLoopCount(2)
                            .OnUpdating(t => current.Add(t))
                            .OnLoopCompleted(_ => current = descending)
                            .Start())
                .AssertTime(2)
                .Assert(() => Assert.IsTrue(ascending.SequenceEqual(ascending.OrderBy(v => v))))
                .Assert(() => Assert.IsTrue(descending.SequenceEqual(descending.OrderByDescending(v => v))))
                .Run();
        }

        #endregion

        #region Easing

        [UnityTest]
        public IEnumerator Easing_Elastic()
        {
            bool smallerThanZero = false;
            bool biggerThanOne = false;

            yield return CreateTester()
                .Act(() => new Tween()
                            .SetEasing(Easing.EaseInOutElastic)
                            .OnUpdating(t =>
                            {
                                if (t < 0)
                                {
                                    smallerThanZero = true;
                                }
                                if (t > 1)
                                {
                                    biggerThanOne = true;
                                }
                            })
                            .Start())
                .AssertTime(1)
                .Assert(() =>
                {
                    Assert.True(smallerThanZero);
                    Assert.True(biggerThanOne);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator Easing_Back()
        {
            bool smallerThanZero = false;
            bool biggerThanOne = false;

            yield return CreateTester()
                .Act(() => new Tween()
                            .SetEasing(Easing.EaseInOutElastic)
                            .OnUpdating(t =>
                            {
                                if (t < 0)
                                {
                                    smallerThanZero = true;
                                }
                                if (t > 1)
                                {
                                    biggerThanOne = true;
                                }
                            })
                            .Start())
                .AssertTime(1)
                .Assert(() =>
                {
                    Assert.True(smallerThanZero);
                    Assert.True(biggerThanOne);
                })
                .Run();
        }

        #endregion

        #region SkipFrames

        [UnityTest]
        public IEnumerator SkipFrames()
        {
            const float testTime = 2f;
            const int skipFrames = 20;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => new Tween()
                            .SetSkipFrames(skipFrames)
                            .SetTime(testTime)
                            .OnStarted(() => frameCount = Time.frameCount - frameCountStart)
                            .Start())
                .Assert(() => Assert.AreEqual(skipFrames, frameCount))
                .Run();
        }

        [UnityTest]
        public IEnumerator SkipFrames_WithOptions()
        {
            const float testTime = 2f;
            const int skipFrames = 20;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => new Tween()
                            .SetOptions(new TweenOptions().SetSkipFrames(skipFrames))
                            .SetTime(testTime)
                            .OnStarted(() => frameCount = Time.frameCount - frameCountStart)
                            .Start())
                .Assert(() => Assert.AreEqual(skipFrames, frameCount))
                .Run();
        }

        [UnityTest]
        public IEnumerator SkipFrames_AutoStart()
        {
            const float testTime = 2f;
            const int skipFrames = 20;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => new Tween(1f, true)
                            .SetSkipFrames(skipFrames)
                            .SetTime(testTime)
                            .OnStarted(() => frameCount = Time.frameCount - frameCountStart))
                .Assert(() => Assert.AreEqual(skipFrames, frameCount))
                .Run();
        }

        [UnityTest]
        public IEnumerator SkipFrames_NegativeValue()
        {
            const float testTime = 2f;
            const int skipFrames = -20;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => new Tween()
                            .SetSkipFrames(skipFrames)
                            .SetTime(testTime)
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
                    Tween tween = new Tween(time)
                            .SetSkipFrames(skipFrames)
                            .OnStarted(() => onStartedCalled = true)
                            .Start();

                    controlTween = new Tween(time).OnCompleted(() => tween.Stop(true)).Start();

                    return tween;
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
            const float testTime = 2f;
            const float delay = 2f;

            yield return CreateTester()
                .Act(() => new Tween()
                            .SetDelay(delay)
                            .SetTime(testTime)
                            .Start())
                .AssertTime(delay + testTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_AutoStart()
        {
            const float testTime = 2f;
            const float delay = 2f;

            yield return CreateTester()
                .Act(() => new Tween(1f, true)
                            .SetDelay(delay)
                            .SetTime(testTime))
                .AssertTime(delay + testTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_WithOptions()
        {
            const float testTime = 2f;
            const float delay = 2f;

            yield return CreateTester()
                .Act(() => new Tween()
                            .SetOptions(new TweenOptions().SetDelay(delay))
                            .SetTime(testTime)
                            .Start())
                .AssertTime(delay + testTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_NegativeValue()
        {
            const float testTime = 2f;
            const float delay = -2f;

            yield return CreateTester()
                .Act(() => new Tween()
                            .SetDelay(delay)
                            .SetTime(testTime)
                            .Start())
                .AssertTime(testTime)
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
                    Tween tween = new Tween(time)
                            .OnStarted(() => onStartedCalled = true)
                            .SetDelay(delay)
                            .Start();

                    controlTween = new Tween(time).OnCompleted(() => tween.Stop(true)).Start();

                    return tween;
                })
                .AssertTime((stopwatch) => FlowEntAssert.Time(time + controlTween.OverDraft.Value, (float)stopwatch.Elapsed.TotalSeconds))
                .Assert(() => Assert.False(onStartedCalled))
                .Run();
        }

        #endregion
    }
}