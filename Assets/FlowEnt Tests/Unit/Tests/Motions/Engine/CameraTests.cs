using System.Collections;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class CameraTests : AbstractEngineTests<Camera>
    {
        private const float Value = 4f;
        private const float FromValue = 1f;
        private const float ToValue = 4f;

        #region BackgroundColor

        [UnityTest]
        public IEnumerator BackgroundColor()
        {
            Color value = Color.green;

            yield return CreateTester()
                .Arrange(() => Component.backgroundColor = Color.clear)
                .Act(() => Component.Tween(TestTime).BackgroundColor(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, Component.backgroundColor))
                .Run();
        }

        [UnityTest]
        public IEnumerator BackgroundColorTo()
        {
            Color to = Color.green;

            yield return CreateTester()
                .Arrange(() => Component.backgroundColor = Color.red)
                .Act(() => Component.Tween(TestTime).BackgroundColorTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Component.backgroundColor))
                .Run();
        }

        [UnityTest]
        public IEnumerator BackgroundColourFromTo()
        {
            Color from = Color.red;
            Color to = Color.green;
            Color? startingValue = null;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).BackgroundColorTo(from, to)
                    .OnUpdated((_) => startingValue ??= Component.backgroundColor)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, Component.backgroundColor);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator BackgroundColourToGradient()
        {
            Color? actualFrom = null;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime)
                    .OnUpdated((_) => actualFrom ??= Component.backgroundColor)
                    .BackgroundColorTo(Variables.Gradient).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(Variables.Gradient.Evaluate(0f), actualFrom);
                    Assert.AreEqual(Variables.Gradient.Evaluate(1f), Component.backgroundColor);
                })
                .Run();
        }

        #endregion

        #region OrthographicSize

        [UnityTest]
        public IEnumerator OrthographicSize()
        {
            const float value = 1f;

            yield return CreateTester()
                .Arrange(() => Component.orthographicSize = 0f)
                .Act(() => Component.Tween(TestTime).OrthographicSize(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.orthographicSize.Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator OrthographicSizeTo()
        {
            const float to = 2f;

            yield return CreateTester()
                .Arrange(() => Component.orthographicSize = 0f)
                .Act(() => Component.Tween(TestTime).OrthographicSizeTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.orthographicSize.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator OrthographicSizeFromTo()
        {
            const float from = 1f;
            const float to = 2f;

            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Component.orthographicSize = 0f)
                .Act(() => Component.Tween(TestTime).OrthographicSizeTo(from, to)
                    .OnUpdated(_ => startingValue ??= Component.orthographicSize)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Component.orthographicSize.Should().Be(to);
                })
                .Run();
        }

        #endregion

        #region FieldOfView

        [UnityTest]
        public IEnumerator FieldOfView()
        {
            yield return CreateTester()
                .Arrange(() => Component.fieldOfView = FromValue)
                .Act(() => Component.Tween(TestTime).FieldOfView(Value).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.fieldOfView.Should().Be(FromValue + Value))
                .Run();
        }

        [UnityTest]
        public IEnumerator FieldOfViewTo()
        {
            yield return CreateTester()
                .Arrange(() => Component.fieldOfView = FromValue)
                .Act(() => Component.Tween(TestTime).FieldOfViewTo(ToValue).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.fieldOfView.Should().Be(ToValue))
                .Run();
        }

        [UnityTest]
        public IEnumerator FieldOfViewFromTo()
        {
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Component.fieldOfView = 0f)
                .Act(() => Component.Tween(TestTime).FieldOfViewTo(FromValue, ToValue)
                    .OnUpdated(_ => startingValue ??= Component.fieldOfView)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(FromValue);
                    Component.fieldOfView.Should().Be(ToValue);
                })
                .Run();
        }

        #endregion

        #region NearClipPlane

        [UnityTest]
        public IEnumerator NearClipPlane()
        {
            yield return CreateTester()
                .Arrange(() => Component.nearClipPlane = FromValue)
                .Act(() => Component.Tween(TestTime).NearClipPlane(Value).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.nearClipPlane.Should().Be(FromValue + Value))
                .Run();
        }

        [UnityTest]
        public IEnumerator NearClipPlaneTo()
        {
            yield return CreateTester()
                .Arrange(() => Component.nearClipPlane = FromValue)
                .Act(() => Component.Tween(TestTime).NearClipPlaneTo(ToValue).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.nearClipPlane.Should().Be(ToValue))
                .Run();
        }

        [UnityTest]
        public IEnumerator NearClipPlaneFromTo()
        {
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Component.nearClipPlane = 0.5f)
                .Act(() => Component.Tween(TestTime).NearClipPlaneTo(FromValue, ToValue)
                    .OnUpdated(_ => startingValue ??= Component.nearClipPlane)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(FromValue);
                    Component.nearClipPlane.Should().Be(ToValue);
                })
                .Run();
        }

        #endregion

        #region FarClipPlane

        [UnityTest]
        public IEnumerator FarClipPlane()
        {
            yield return CreateTester()
                .Arrange(() => Component.farClipPlane = FromValue)
                .Act(() => Component.Tween(TestTime).FarClipPlane(Value).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.farClipPlane.Should().Be(FromValue + Value))
                .Run();
        }

        [UnityTest]
        public IEnumerator FarClipPlaneTo()
        {
            yield return CreateTester()
                .Arrange(() => Component.farClipPlane = FromValue)
                .Act(() => Component.Tween(TestTime).FarClipPlaneTo(ToValue).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.farClipPlane.Should().Be(ToValue))
                .Run();
        }

        [UnityTest]
        public IEnumerator FarClipPlaneFromTo()
        {
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Component.farClipPlane = 0.5f)
                .Act(() => Component.Tween(TestTime).FarClipPlaneTo(FromValue, ToValue)
                    .OnUpdated(_ => startingValue ??= Component.farClipPlane)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(FromValue);
                    Component.farClipPlane.Should().Be(ToValue);
                })
                .Run();
        }

        #endregion
    }
}