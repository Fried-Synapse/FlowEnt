using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public static partial class AbstractAnimationTestsValues
    {
        public static readonly int[] loopCountValues = { 0, -1 };
    }

    public abstract class AbstractAnimationOptionsTests<TAnimation, TAnimationOptions> : AbstractEngineTests
        where TAnimation : AbstractAnimation
        where TAnimationOptions : AbstractAnimationOptions, new()
    {
        protected abstract TAnimation CreateAnimation(float testTime);
        protected abstract TAnimation CreateAnimation(float testTime, TAnimationOptions options);

        private TAnimation CreateAnimationInternal(float testTime, AbstractAnimationOptions options)
            => CreateAnimation(testTime, (TAnimationOptions)options);

        #region Conditional

        [UnityTest]
        public IEnumerator Conditional(
            [ValueSource(typeof(AbstractAnimationTestsValues), nameof(AbstractAnimationTestsValues.stopValues))]
            bool isDefaultTime)
        {
            yield return CreateTester()
                .Act(() => CreateAnimation(TestTime)
                    .Conditional(() => !isDefaultTime, a => a.SetTimeScale(2))
                    .Start())
                .AssertTime(isDefaultTime ? TestTime : HalfTestTime)
                .Run();
        }

        #endregion

        #region TimeScale

        protected static readonly UpdateType[] updateTypes = ((UpdateType[])Enum.GetValues(typeof(UpdateType)))
            .Where(state => state != UpdateType.Custom) // Exclude the Ended state
            .ToArray();

        private float GetDeltaTime(UpdateType type)
            => type switch
            {
                UpdateType.Update => Time.deltaTime,
                UpdateType.SmoothUpdate => Time.smoothDeltaTime,
                UpdateType.UnscaledUpdate => Time.unscaledDeltaTime,
                UpdateType.LateUpdate => Time.deltaTime,
                UpdateType.SmoothLateUpdate => Time.smoothDeltaTime,
                UpdateType.UnscaledLateUpdate => Time.unscaledDeltaTime,
                UpdateType.FixedUpdate => Time.fixedDeltaTime,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

        private IEnumerator UpdateTypeInternal(UpdateType type, Func<float, AbstractAnimation> getAnimation,
            Func<float> getDeltaTime)
        {
            List<float> values = new();
            UpdateTracker updateTracker = default;
            float initialTimeScale = Time.timeScale;

            yield return CreateTester()
                .Arrange(() =>
                {
                    updateTracker = GameObject.AddComponent<UpdateTracker>();
                    switch (type)
                    {
                        case UpdateType.UnscaledUpdate:
                        case UpdateType.UnscaledLateUpdate:
                            Time.timeScale = 0;
                            break;
                    }
                })
                .SetActDelay(1)
                .Act(() => getAnimation(HalfTestTime).OnUpdated(_ => { values.Add(getDeltaTime()); }).Start())
                .AssertTime(HalfTestTime)
                .Assert(() =>
                {
                    switch (type)
                    {
                        case UpdateType.UnscaledUpdate:
                        case UpdateType.UnscaledLateUpdate:
                            Time.timeScale.Should().Be(0);
                            break;
                    }

                    values.Should().HaveCountGreaterThan(5)
                        .And.AllSatisfy(item => updateTracker.Values[type].Should().Contain(item));
                })
                .Abrogate(() =>
                {
                    Object.Destroy(updateTracker);
                    switch (type)
                    {
                        case UpdateType.UnscaledUpdate:
                        case UpdateType.UnscaledLateUpdate:
                            Time.timeScale = initialTimeScale;
                            break;
                    }
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator UpdateTypeNormal([ValueSource(nameof(updateTypes))] UpdateType type)
            => UpdateTypeInternal(type, time => CreateAnimation(time).SetUpdateType(type), () => GetDeltaTime(type));

        [UnityTest]
        public IEnumerator UpdateTypeOptions([ValueSource(nameof(updateTypes))] UpdateType type)
            => UpdateTypeInternal(type,
                time => CreateAnimation(time, (TAnimationOptions)new TAnimationOptions().SetUpdateType(type)),
                () => GetDeltaTime(type));

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

            Action act = () => CreateAnimation(TestTime).SetTimeScale(testTimeScale);
            act.Should().Throw<ArgumentException>();
            act = () => CreateAnimationInternal(TestTime, new TAnimationOptions() { TimeScale = testTimeScale });
            act.Should().Throw<ArgumentException>();
            act = () => CreateAnimationInternal(TestTime, new TAnimationOptions().SetTimeScale(testTimeScale));
            act.Should().Throw<ArgumentException>();
        }

        #endregion

        #region LoopCount

        [UnityTest]
        public IEnumerator LoopCount()
        {
            const int loopCount = 10;

            yield return CreateTester()
                .Act(() => CreateAnimation(TestTime / loopCount)
                    .SetLoopCount(loopCount)
                    .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator LoopCount_WithOptions()
        {
            const int loopCount = 10;

            yield return CreateTester()
                .Act(() => CreateAnimationInternal(TestTime / loopCount, new TAnimationOptions().SetLoopCount(loopCount))
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
                .Assert(() => loopCountCounter.Should().Be(loopCountTries))
                .AssertTime(loopCountTries * QuarterTestTime)
                .Run();
        }

        [Test]
        public void LoopCount_Invalid(
            [ValueSource(typeof(AbstractAnimationTestsValues), nameof(AbstractAnimationTestsValues.loopCountValues))]
            int loopCount)
        {
            Action act = () => CreateAnimation(TestTime).SetLoopCount(loopCount);
            act.Should().Throw<ArgumentException>();
            act = () => CreateAnimationInternal(TestTime, new TAnimationOptions { LoopCount = loopCount });
            act.Should().Throw<ArgumentException>();
            act = () => CreateAnimationInternal(TestTime, new TAnimationOptions().SetLoopCount(loopCount));
            act.Should().Throw<ArgumentException>();
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
                .Assert(() => frameCount.Should().Be(skipFrames))
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
                .Assert(() => frameCount.Should().Be(skipFrames))
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
                .Assert(() => frameCount.Should().Be(skipFrames))
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
                .Assert(() => frameCount.Should().Be(0))
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
                .AssertTime(stopwatch => stopwatch.Seconds.Should()
                    .BeApproximatelyTime(QuarterTestTime + controlAnimation.Overdraft.Value))
                .Assert(() => onStartedCalled.Should().BeFalse())
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
                .AssertTime(stopwatch => stopwatch.Seconds.Should()
                    .BeApproximatelyTime(time + controlAnimation.Overdraft.Value))
                .Assert(() => onStartedCalled.Should().BeFalse())
                .Run();
        }

        #endregion

        #region Delay Until

        [UnityTest]
        public IEnumerator DelayUntil()
        {
            bool flag = false;

            yield return CreateTester()
                .Arrange(() => CreateAnimation(HalfTestTime).OnCompleted(() => flag = true).Start())
                .Act(() => CreateAnimation(HalfTestTime)
                    .SetDelayUntil(() => flag)
                    .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator DelayUntil_AutoStart()
        {
            bool flag = false;

            yield return CreateTester()
                .Arrange(() => CreateAnimation(HalfTestTime).OnCompleted(() => flag = true).Start())
                .Act(() => CreateAnimation(HalfTestTime)
                    .SetAutoStart(true)
                    .SetDelayUntil(() => flag))
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator DelayUntil_WithOptions()
        {
            bool flag = false;

            yield return CreateTester()
                .Arrange(() => CreateAnimation(HalfTestTime).OnCompleted(() => flag = true).Start())
                .Act(() => CreateAnimationInternal(HalfTestTime, new TAnimationOptions().SetDelayUntil(() => flag))
                    .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator DelayUntil_NullCallback()
        {
            yield return CreateTester()
                .Act(() => CreateAnimation(TestTime)
                    .SetDelayUntil(null)
                    .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator DelayUntil_StopBeforeStart()
        {
            bool flag = false;
            const float time = TestTime / 2;
            bool onStartedCalled = false;
            TAnimation controlAnimation = null;

            yield return CreateTester()
                .Arrange(() => CreateAnimation(TestTime).OnCompleted(() => flag = true).Start())
                .Act(() =>
                {
                    TAnimation animation = CreateAnimation(time)
                        .OnStarted(() => onStartedCalled = true)
                        .SetDelayUntil(() => flag)
                        .Start() as TAnimation;

                    controlAnimation = CreateAnimation(time).OnCompleted(() => animation.Stop(true)).Start() as TAnimation;

                    return animation;
                })
                .AssertTime(stopwatch => stopwatch.Seconds.Should()
                    .BeApproximatelyTime(time + controlAnimation.Overdraft.Value))
                .Assert(() => onStartedCalled.Should().BeFalse())
                .Run();
        }

        #endregion

        [UnityTest]
        public IEnumerator DelayBothSequence()
        {
            bool flag = false;
            yield return CreateTester()
                .Arrange(() => new Tween(TwoThirdsTestTime).OnCompleted(() => flag = true).Start())
                .Act(() => CreateAnimation(ThirdTestTime)
                    .SetDelay(ThirdTestTime)
                    .SetDelayUntil(() => flag)
                    .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator DelayBothParallel()
        {
            bool flag = false;
            yield return CreateTester()
                .Arrange(() => new Tween(HalfTestTime).OnCompleted(() => flag = true).Start())
                .Act(() => CreateAnimation(HalfTestTime)
                    .SetDelay(HalfTestTime)
                    .SetDelayUntil(() => flag)
                    .Start())
                .AssertTime(TestTime)
                .Run();
        }
    }
}