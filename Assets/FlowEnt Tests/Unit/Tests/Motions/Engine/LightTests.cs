using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class LightTests : AbstractEngineTests<Light>
    {
        #region Intensity

        [UnityTest]
        public IEnumerator Intensity()
        {
            const float value = 0.75f;

            yield return CreateTester()
                .Arrange(() => Component.intensity = 0f)
                .Act(() => Component.Tween(TestTime).Intensity(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, Component.intensity))
                .Run();
        }

        [UnityTest]
        public IEnumerator IntensityTo()
        {
            const float to = 0.75f;

            yield return CreateTester()
                .Arrange(() => Component.intensity = 0f)
                .Act(() => Component.Tween(TestTime).IntensityTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Component.intensity))
                .Run();
        }

        [UnityTest]
        public IEnumerator IntensityFromTo()
        {
            const float from = 0.25f;
            const float to = 0.75f;
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Component.intensity = 0f)
                .Act(() => Component.Tween(TestTime).IntensityTo(from, to)
                                    .OnUpdated(_ => startingValue ??= Component.intensity)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, Component.intensity);
                })
                .Run();
        }

        #endregion

        #region Range

        [UnityTest]
        public IEnumerator Range()
        {
            const float value = 0.75f;

            yield return CreateTester()
                .Arrange(() => Component.range = 0f)
                .Act(() => Component.Tween(TestTime).Range(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, Component.range))
                .Run();
        }

        [UnityTest]
        public IEnumerator RangeTo()
        {
            const float to = 0.75f;

            yield return CreateTester()
                .Arrange(() => Component.range = 0f)
                .Act(() => Component.Tween(TestTime).RangeTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Component.range))
                .Run();
        }

        [UnityTest]
        public IEnumerator RangeFromTo()
        {
            const float from = 0.25f;
            const float to = 0.75f;
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Component.range = 0f)
                .Act(() => Component.Tween(TestTime).RangeTo(from, to)
                                    .OnUpdated(_ => startingValue ??= Component.range)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, Component.range);
                })
                .Run();
        }

        #endregion

        #region Color

        [UnityTest]
        public IEnumerator Colour()
        {
            Color value = Color.green;

            yield return CreateTester()
                .Arrange(() => Component.color = Color.clear)
                .Act(() => Component.Tween(TestTime).Color(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, Component.color))
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourTo()
        {
            Color to = Color.green;

            yield return CreateTester()
                .Arrange(() => Component.color = Color.red)
                .Act(() => Component.Tween(TestTime).ColorTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Component.color))
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourFromTo()
        {
            Color from = Color.red;
            Color to = Color.green;
            Color? startingValue = null;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).ColorTo(from, to)
                                    .OnUpdated((_) => startingValue ??= Component.color)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, Component.color);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourToGradient()
        {
            Color? actualFrom = null;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime)
                    .OnUpdated((_) => actualFrom ??= Component.color)
                    .ColorTo(Variables.Gradient).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(Variables.Gradient.Evaluate(0f), actualFrom);
                    Assert.AreEqual(Variables.Gradient.Evaluate(1f), Component.color);
                })
                .Run();
        }

        #endregion

        #region ShadowRadius
#if !UNITY_WEBGL

        [UnityTest]
        public IEnumerator ShadowRadius()
        {
            const float value = 0.75f;

            yield return CreateTester()
                .Arrange(() => Component.shadowRadius = 0f)
                .Act(() => Component.Tween(TestTime).ShadowRadius(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, Component.shadowRadius))
                .Run();
        }

        [UnityTest]
        public IEnumerator ShadowRadiusTo()
        {
            const float to = 0.75f;

            yield return CreateTester()
                .Arrange(() => Component.shadowRadius = 0f)
                .Act(() => Component.Tween(TestTime).ShadowRadiusTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Component.shadowRadius))
                .Run();
        }

        [UnityTest]
        public IEnumerator ShadowRadiusFromTo()
        {
            const float from = 0.25f;
            const float to = 0.75f;
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Component.shadowRadius = 0f)
                .Act(() => Component.Tween(TestTime).ShadowRadiusTo(from, to)
                                    .OnUpdated(_ => startingValue ??= Component.shadowRadius)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, Component.shadowRadius);
                })
                .Run();
        }
        
#endif
        #endregion

        #region ShadowStrength

        [UnityTest]
        public IEnumerator ShadowStrength()
        {
            const float value = 0.75f;

            yield return CreateTester()
                .Arrange(() => Component.shadowStrength = 0f)
                .Act(() => Component.Tween(TestTime).ShadowStrength(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, Component.shadowStrength))
                .Run();
        }

        [UnityTest]
        public IEnumerator ShadowStrengthTo()
        {
            const float to = 0.75f;

            yield return CreateTester()
                .Arrange(() => Component.shadowStrength = 0f)
                .Act(() => Component.Tween(TestTime).ShadowStrengthTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Component.shadowStrength))
                .Run();
        }

        [UnityTest]
        public IEnumerator ShadowStrengthFromTo()
        {
            const float from = 0.25f;
            const float to = 0.75f;
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Component.shadowStrength = 0f)
                .Act(() => Component.Tween(TestTime).ShadowStrengthTo(from, to)
                                    .OnUpdated(_ => startingValue ??= Component.shadowStrength)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, Component.shadowStrength);
                })
                .Run();
        }

        #endregion

    }
}