using System.Collections;
using FluentAssertions;
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
                .Assert(() => Component.intensity.Should().Be(value))
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
                .Assert(() => Component.intensity.Should().Be(to))
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
                    startingValue.Should().Be(from);
                    Component.intensity.Should().Be(to);
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
                .Assert(() => Component.range.Should().Be(value))
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
                .Assert(() => Component.range.Should().Be(to))
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
                    startingValue.Should().Be(from);
                    Component.range.Should().Be(to);
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
                .Assert(() => Component.color.Should().Be(value))
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
                .Assert(() => Component.color.Should().Be(to))
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
                    .OnUpdated(_ => startingValue ??= Component.color)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Component.color.Should().Be(to);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourToGradient()
        {
            Color? actualFrom = null;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime)
                    .OnUpdated(_ => actualFrom ??= Component.color)
                    .ColorTo(Variables.Gradient).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    actualFrom.Should().Be(Variables.Gradient.Evaluate(0f));
                    Component.color.Should().Be(Variables.Gradient.Evaluate(1f));
                })
                .Run();
        }

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
                .Assert(() => Component.shadowStrength.Should().Be(value))
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
                .Assert(() => Component.shadowStrength.Should().Be(to))
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
                    startingValue.Should().Be(from);
                    Component.shadowStrength.Should().Be(to);
                })
                .Run();
        }

        #endregion
    }
}