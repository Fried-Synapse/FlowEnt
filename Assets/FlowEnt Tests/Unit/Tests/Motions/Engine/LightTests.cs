using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class LightTests : AbstractEngineTests
    {
        private Light light;

        private Light Light
        {
            get
            {
                if (light == null)
                {
                    light = GameObject.AddComponent<Light>();
                }
                return light;
            }
        }

        #region Intensity

        [UnityTest]
        public IEnumerator Intensity()
        {
            const float value = 0.75f;

            yield return CreateTester()
                .Arrange(() => Light.intensity = 0f)
                .Act(() => Light.Tween(TestTime).Intensity(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, Light.intensity))
                .Run();
        }

        [UnityTest]
        public IEnumerator IntensityTo()
        {
            const float to = 0.75f;

            yield return CreateTester()
                .Arrange(() => Light.intensity = 0f)
                .Act(() => Light.Tween(TestTime).IntensityTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Light.intensity))
                .Run();
        }

        [UnityTest]
        public IEnumerator IntensityFromTo()
        {
            const float from = 0.25f;
            const float to = 0.75f;
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Light.intensity = 0f)
                .Act(() => Light.Tween(TestTime).IntensityTo(from, to)
                                    .OnUpdated(_ => startingValue ??= Light.intensity)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, Light.intensity);
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
                .Arrange(() => Light.color = Color.clear)
                .Act(() => Light.Tween(TestTime).Color(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, Light.color))
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourTo()
        {
            Color to = Color.green;

            yield return CreateTester()
                .Arrange(() => Light.color = Color.red)
                .Act(() => Light.Tween(TestTime).ColorTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Light.color))
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourFromTo()
        {
            Color from = Color.red;
            Color to = Color.green;
            Color? startingValue = null;

            yield return CreateTester()
                .Act(() => Light.Tween(TestTime).ColorTo(from, to)
                                    .OnUpdated((_) => startingValue ??= Light.color)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, Light.color);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourToGradient()
        {
            Color? actualFrom = null;

            yield return CreateTester()
                .Act(() => Light.Tween(TestTime)
                    .OnUpdated((_) => actualFrom ??= Light.color)
                    .ColorTo(Variables.Gradient).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(Variables.Gradient.Evaluate(0f), actualFrom);
                    Assert.AreEqual(Variables.Gradient.Evaluate(1f), Light.color);
                })
                .Run();
        }

        #endregion

    }
}