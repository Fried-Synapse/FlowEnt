using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public static class TweenTestsValues
    {
        public static readonly float[] timeValues = { 0, -1f, 1f / 0f };
    }

    public class TweenOptionsTests : AbstractAnimationOptionsTests<Tween, TweenOptions>
    {
        protected override Tween CreateAnimation(float testTime)
            => new(testTime);

        protected override Tween CreateAnimation(float testTime, TweenOptions options)
            => new Tween().SetOptions(options).SetTime(testTime);

        #region Builder

        [UnityTest]
        public IEnumerator Builder()
        {
            TweenOptions tweenOptions = default;

            yield return CreateTester()
                .Act(() => tweenOptions = Variables.Tween.Options.Build())
                .Assert(() =>
                {
                    tweenOptions.Name.Should().Be(Variables.Tween.Options.Name);
                    tweenOptions.UpdateType.Should().Be(Variables.Tween.Options.UpdateType);
                    tweenOptions.AutoStart.Should().Be(Variables.Tween.Options.AutoStart);
                    tweenOptions.SkipFrames.Should().Be(Variables.Tween.Options.SkipFrames);
                    tweenOptions.Delay.Should().Be(Variables.Tween.Options.Delay);
                    tweenOptions.TimeScale.Should().Be(Variables.Tween.Options.TimeScale);
                    tweenOptions.Time.Should().Be(Variables.Tween.Options.Time);
                    tweenOptions.Easing.GetType().FullName.Should().Be(Variables.Tween.Options.Easing.GetType().FullName);
                    tweenOptions.LoopCount.Should().Be(Variables.Tween.Options.LoopCount);
                    tweenOptions.LoopType.Should().Be(Variables.Tween.Options.LoopType);
                })
                .Run();
        }

        #endregion

        #region Time

        [UnityTest]
        public IEnumerator Time_Default()
        {
            yield return CreateTester()
                .Act(() => CreateAnimation(TestTime)
                    .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Time_Constructor()
        {
            yield return CreateTester()
                .Act(() => CreateAnimation(TestTime)
                    .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Time_WithOptions()
        {
            yield return CreateTester()
                .Act(() => CreateAnimation(TestTime)
                    .SetOptions(new TweenOptions().SetTime(TestTime))
                    .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [Test]
        public void Time_Invalid([ValueSource(typeof(TweenTestsValues), nameof(TweenTestsValues.timeValues))] float time)
        {
            Func<Tween> act = () => new Tween(time);
            act.Should().Throw<ArgumentException>();
            act = () => new Tween().SetTime(time);
            act.Should().Throw<ArgumentException>();
            act = () => new Tween(new TweenOptions() { Time = time });
            act.Should().Throw<ArgumentException>();
            act = () => new Tween(new TweenOptions().SetTime(time));
            act.Should().Throw<ArgumentException>();
        }

        #endregion

        #region LoopType

        [UnityTest]
        public IEnumerator LoopType_PingPong()
        {
            List<float> ascending = new();
            List<float> descending = new();
            List<float> current = ascending;

            yield return CreateTester()
                .Act(() => new Tween(HalfTestTime)
                    .SetLoopType(LoopType.PingPong)
                    .SetLoopCount(2)
                    .OnUpdating(t => current.Add(t))
                    .OnLoopCompleted(_ => current = descending)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() => ascending.Should().BeInAscendingOrder())
                .Assert(() => descending.Should().BeInDescendingOrder())
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
                .Act(() => new Tween(TestTime)
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
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    smallerThanZero.Should().BeTrue();
                    biggerThanOne.Should().BeTrue();
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator Easing_Back()
        {
            bool smallerThanZero = false;
            bool biggerThanOne = false;

            yield return CreateTester()
                .Act(() => new Tween(TestTime)
                    .SetEasing(Easing.EaseInOutBack)
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
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    smallerThanZero.Should().BeTrue();
                    biggerThanOne.Should().BeTrue();
                })
                .Run();
        }

        #endregion
    }
}