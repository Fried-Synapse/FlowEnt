using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class TweenOptionsTests : AbstractAnimationOptionsTests<Tween, TweenOptions>
    {
        protected override Tween CreateAnimation(float testTime)
            => new Tween(testTime);

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
                    Assert.AreEqual(Variables.Tween.Options.Name, tweenOptions.Name);
                    Assert.AreEqual(Variables.Tween.Options.UpdateType, tweenOptions.UpdateType);
                    Assert.AreEqual(Variables.Tween.Options.AutoStart, tweenOptions.AutoStart);
                    Assert.AreEqual(Variables.Tween.Options.SkipFrames, tweenOptions.SkipFrames);
                    Assert.AreEqual(Variables.Tween.Options.Delay, tweenOptions.Delay);
                    Assert.AreEqual(Variables.Tween.Options.TimeScale, tweenOptions.TimeScale);
                    Assert.AreEqual(Variables.Tween.Options.Time, tweenOptions.Time);
                    Assert.AreEqual(Variables.Tween.Options.Easing.GetType().FullName, tweenOptions.Easing.GetType().FullName);
                    Assert.AreEqual(Variables.Tween.Options.LoopCount, tweenOptions.LoopCount);
                    Assert.AreEqual(Variables.Tween.Options.LoopType, tweenOptions.LoopType);
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
        public void Time_ZeroValue()
        {
            const float time = 0f;

            Assert.Throws<ArgumentException>(() => new Tween(time));
            Assert.Throws<ArgumentException>(() => new Tween().SetTime(time));
            Assert.Throws<ArgumentException>(() => new Tween(new TweenOptions() { Time = time }));
            Assert.Throws<ArgumentException>(() => new Tween(new TweenOptions().SetTime(time)));
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

        #region LoopType

        [UnityTest]
        public IEnumerator LoopType_PingPong()
        {
            List<float> ascending = new List<float>();
            List<float> descending = new List<float>();
            List<float> current = ascending;

            yield return CreateTester()
                .Act(() => new Tween(HalfTestTime)
                            .SetLoopType(LoopType.PingPong)
                            .SetLoopCount(2)
                            .OnUpdating(t => current.Add(t))
                            .OnLoopCompleted(_ => current = descending)
                            .Start())
                .AssertTime(TestTime)
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
                    Assert.True(smallerThanZero);
                    Assert.True(biggerThanOne);
                })
                .Run();
        }

        #endregion
    }
}