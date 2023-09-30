using System.Collections;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class CameraTests : AbstractEngineTests<Camera>
    {
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
            const float value = 1f;

            yield return CreateTester()
                .Arrange(() => Component.fieldOfView = 0f)
                .Act(() => Component.Tween(TestTime).FieldOfView(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.fieldOfView.Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator FieldOfViewTo()
        {
            const float to = 2f;

            yield return CreateTester()
                .Arrange(() => Component.fieldOfView = 0f)
                .Act(() => Component.Tween(TestTime).FieldOfViewTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.fieldOfView.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator FieldOfViewFromTo()
        {
            const float from = 1f;
            const float to = 2f;

            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Component.fieldOfView = 0f)
                .Act(() => Component.Tween(TestTime).FieldOfViewTo(from, to)
                    .OnUpdated(_ => startingValue ??= Component.fieldOfView)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Component.fieldOfView.Should().Be(to);
                })
                .Run();
        }

        #endregion

        #region NearClipPlane

        [UnityTest]
        public IEnumerator NearClipPlane()
        {
            const float value = 1f;

            yield return CreateTester()
                .Arrange(() => Component.nearClipPlane = 0.5f)
                .Act(() => Component.Tween(TestTime).NearClipPlane(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.nearClipPlane.Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator NearClipPlaneTo()
        {
            const float to = 2f;

            yield return CreateTester()
                .Arrange(() => Component.nearClipPlane = 0.5f)
                .Act(() => Component.Tween(TestTime).NearClipPlaneTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.nearClipPlane.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator NearClipPlaneFromTo()
        {
            const float from = 1f;
            const float to = 2f;

            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Component.nearClipPlane = 0.5f)
                .Act(() => Component.Tween(TestTime).NearClipPlaneTo(from, to)
                    .OnUpdated(_ => startingValue ??= Component.nearClipPlane)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Component.nearClipPlane.Should().Be(to);
                })
                .Run();
        }

        #endregion

        #region FarClipPlane

        [UnityTest]
        public IEnumerator FarClipPlane()
        {
            const float value = 1f;

            yield return CreateTester()
                .Arrange(() => Component.farClipPlane = 0.5f)
                .Act(() => Component.Tween(TestTime).FarClipPlane(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.farClipPlane.Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator FarClipPlaneTo()
        {
            const float to = 2f;

            yield return CreateTester()
                .Arrange(() => Component.farClipPlane = 0.5f)
                .Act(() => Component.Tween(TestTime).FarClipPlaneTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.farClipPlane.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator FarClipPlaneFromTo()
        {
            const float from = 1f;
            const float to = 2f;

            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Component.farClipPlane = 0.5f)
                .Act(() => Component.Tween(TestTime).FarClipPlaneTo(from, to)
                    .OnUpdated(_ => startingValue ??= Component.farClipPlane)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Component.farClipPlane.Should().Be(to);
                })
                .Run();
        }

        #endregion
    }
}