using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public abstract class AbstractAnimationOptionsTests<TAnimation, TAnimationOptions> : AbstractEngineTests
        where TAnimation : AbstractAnimation
        where TAnimationOptions : AbstractAnimationOptions, new()
    {
        protected abstract TAnimation CreateAnimation(float testTime);
        protected abstract TAnimation CreateAnimation(float testTime, TAnimationOptions options);
        private TAnimation CreateAnimationInternal(float testTime, AbstractAnimationOptions options)
            => CreateAnimation(testTime, (TAnimationOptions)options);

        #region TimeScale

        private IEnumerator UpdateTypeInternal(UpdateType type, Func<float, AbstractAnimation> getAnimation, Func<float> getDeltaTime)
        {
            List<float> values = new List<float>();
            UpdateTracker updateTracker = default;

            yield return CreateTester()
                .Arrange(() => updateTracker = GameObject.AddComponent<UpdateTracker>())
                .SetActDelay(1)
                .Act(() => getAnimation(HalfTestTime).OnUpdated(_ => values.Add(getDeltaTime())).Start())
                .AssertTime(HalfTestTime)
                .Assert(() =>
                {
                    Assert.GreaterOrEqual(values.Count, 5);
                    foreach (float item in values)
                    {
                        Assert.Contains(item, updateTracker.Values[type]);
                    }
                })
                .Run();
        }

        private IEnumerator UpdateTypeNormal(UpdateType type, Func<float> getDeltaTime)
            => UpdateTypeInternal(type, (time) => CreateAnimation(time).SetUpdateType(type), getDeltaTime);

        [UnityTest]
        public IEnumerator UpdateType_Update()
            => UpdateTypeNormal(UpdateType.Update, () => Time.deltaTime);

        [UnityTest]
        public IEnumerator UpdateType_SmoothUpdate()
            => UpdateTypeNormal(UpdateType.SmoothUpdate, () => Time.smoothDeltaTime);

        [UnityTest]
        public IEnumerator UpdateType_LateUpdate()
            => UpdateTypeNormal(UpdateType.LateUpdate, () => Time.deltaTime);

        [UnityTest]
        public IEnumerator UpdateType_SmoothLateUpdate()
            => UpdateTypeNormal(UpdateType.SmoothLateUpdate, () => Time.smoothDeltaTime);

        [UnityTest]
        public IEnumerator UpdateType_FixedUpdate()
            => UpdateTypeNormal(UpdateType.FixedUpdate, () => Time.fixedDeltaTime);

        private IEnumerator UpdateTypeOptions(UpdateType type, Func<float> getDeltaTime)
            => UpdateTypeInternal(type, (time) => CreateAnimation(time, (TAnimationOptions)new TAnimationOptions().SetUpdateType(type)), getDeltaTime);

        [UnityTest]
        public IEnumerator UpdateType_OptionsUpdate()
            => UpdateTypeOptions(UpdateType.Update, () => Time.deltaTime);

        [UnityTest]
        public IEnumerator UpdateType_OptionsSmoothUpdate()
            => UpdateTypeOptions(UpdateType.SmoothUpdate, () => Time.smoothDeltaTime);

        [UnityTest]
        public IEnumerator UpdateType_OptionsLateUpdate()
            => UpdateTypeOptions(UpdateType.LateUpdate, () => Time.deltaTime);

        [UnityTest]
        public IEnumerator UpdateType_OptionsSmoothLateUpdate()
            => UpdateTypeOptions(UpdateType.SmoothLateUpdate, () => Time.smoothDeltaTime);

        [UnityTest]
        public IEnumerator UpdateType_OptionsFixedUpdate()
            => UpdateTypeOptions(UpdateType.FixedUpdate, () => Time.fixedDeltaTime);

        #endregion

        #region TimeScale

        [UnityTest]
        public IEnumerator TimeScale()
        {
            const float testTimeScale = 2f;

            yield return CreateTester()
                .Act(() => CreateAnimation(TestTime)
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
                .Act(() => CreateAnimationInternal(TestTime, new TAnimationOptions().SetTimeScale(testTimeScale))
                            .Start())
                .AssertTime(TestTime / testTimeScale)
                .Run();
        }

        [Test]
        public void TimeScale_NegativeValue()
        {
            const float testTimeScale = -1f;

            Assert.Throws<ArgumentException>(() => CreateAnimation(TestTime).SetTimeScale(testTimeScale));
            Assert.Throws<ArgumentException>(() => CreateAnimationInternal(TestTime, new TAnimationOptions() { TimeScale = testTimeScale }));
            Assert.Throws<ArgumentException>(() => CreateAnimationInternal(TestTime, new TAnimationOptions().SetTimeScale(testTimeScale)));
        }

        #endregion

        #region LoopCount

        [UnityTest]
        public IEnumerator LoopCount()
        {
            const int loopCount = 2;

            yield return CreateTester()
                .Act(() => CreateAnimation(HalfTestTime)
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
                .Act(() => CreateAnimationInternal(HalfTestTime, new TAnimationOptions().SetLoopCount(loopCount))
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
                    CreateAnimation(QuarterTestTime)
                        .OnLoopCompleted((loopsLeft) =>
                        {
                            if (loopsLeft == null)
                            {
                                loopCountCounter++;
                            }
                        })
                        .SetLoopCount(loopCount)
                        .Start();

                    return CreateAnimation(loopCountTries * QuarterTestTime).Start();
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

            Assert.Throws<ArgumentException>(() => CreateAnimation(TestTime).SetLoopCount(loopCountZero));
            Assert.Throws<ArgumentException>(() => CreateAnimationInternal(TestTime, new TAnimationOptions() { LoopCount = loopCountZero }));
            Assert.Throws<ArgumentException>(() => CreateAnimationInternal(TestTime, new TAnimationOptions().SetLoopCount(loopCountZero)));

            Assert.Throws<ArgumentException>(() => CreateAnimation(TestTime).SetLoopCount(loopCountNegative));
            Assert.Throws<ArgumentException>(() => CreateAnimationInternal(TestTime, new TAnimationOptions() { LoopCount = loopCountNegative }));
            Assert.Throws<ArgumentException>(() => CreateAnimationInternal(TestTime, new TAnimationOptions().SetLoopCount(loopCountNegative)));
        }

        #endregion

        #region SkipFrames

        [UnityTest]
        public IEnumerator SkipFrames()
        {
            const int skipFrames = 10;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => CreateAnimation(HalfTestTime)
                            .SetSkipFrames(skipFrames)
                            .OnStarted(() => frameCount = Time.frameCount - frameCountStart)
                            .Start())
                .Assert(() => Assert.AreEqual(skipFrames, frameCount))
                .Run();
        }

        [UnityTest]
        public IEnumerator SkipFrames_WithOptions()
        {
            const int skipFrames = 10;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => CreateAnimationInternal(HalfTestTime, new TAnimationOptions().SetSkipFrames(skipFrames))
                            .OnStarted(() => frameCount = Time.frameCount - frameCountStart)
                            .Start())
                .Assert(() => Assert.AreEqual(skipFrames, frameCount))
                .Run();
        }

        [UnityTest]
        public IEnumerator SkipFrames_AutoStart()
        {
            const int skipFrames = 10;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => CreateAnimation(HalfTestTime)
                            .SetAutoStart(true)
                            .SetSkipFrames(skipFrames)
                            .OnStarted(() => frameCount = Time.frameCount - frameCountStart))
                .Assert(() => Assert.AreEqual(skipFrames, frameCount))
                .Run();
        }

        [UnityTest]
        public IEnumerator SkipFrames_NegativeValue()
        {
            const int skipFrames = -10;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => CreateAnimation(HalfTestTime)
                            .SetSkipFrames(skipFrames)
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
            TAnimation controlAnimation = null;

            yield return CreateTester()
                .Act(() =>
                {
                    TAnimation animation = CreateAnimation(QuarterTestTime)
                            .SetSkipFrames(skipFrames)
                            .OnStarted(() => onStartedCalled = true)
                            .Start() as TAnimation;

                    controlAnimation = CreateAnimation(QuarterTestTime).OnCompleted(() => animation.Stop(true)).Start() as TAnimation;

                    return animation;
                })
                .AssertTime((stopwatch) => FlowEntAssert.Time(QuarterTestTime + controlAnimation.Overdraft.Value, (float)stopwatch.Elapsed.TotalSeconds))
                .Assert(() => Assert.False(onStartedCalled))
                .Run();
        }

        #endregion

        #region Delay

        [UnityTest]
        public IEnumerator Delay()
        {
            yield return CreateTester()
                .Act(() => CreateAnimation(HalfTestTime)
                            .SetDelay(HalfTestTime)
                            .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_AutoStart()
        {
            yield return CreateTester()
                .Act(() => CreateAnimation(HalfTestTime)
                            .SetAutoStart(true)
                            .SetDelay(HalfTestTime))
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_WithOptions()
        {
            yield return CreateTester()
                .Act(() => CreateAnimationInternal(HalfTestTime, new TAnimationOptions().SetDelay(HalfTestTime))
                            .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_NegativeValue()
        {
            const float delay = -2f;

            yield return CreateTester()
                .Act(() => CreateAnimation(TestTime)
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
            TAnimation controlAnimation = null;

            yield return CreateTester()
                .Act(() =>
                {
                    TAnimation animation = CreateAnimation(time)
                            .OnStarted(() => onStartedCalled = true)
                            .SetDelay(delay)
                            .Start() as TAnimation;

                    controlAnimation = CreateAnimation(time).OnCompleted(() => animation.Stop(true)).Start() as TAnimation;

                    return animation;
                })
                .AssertTime((stopwatch) => FlowEntAssert.Time(time + controlAnimation.Overdraft.Value, (float)stopwatch.Elapsed.TotalSeconds))
                .Assert(() => Assert.False(onStartedCalled))
                .Run();
        }

        #endregion
    }
}