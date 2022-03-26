using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class TrailRendererTests : AbstractEngineTests<TrailRenderer>
    {
        #region Constants

        private const float MoveValue = 4f;
        private const float MoveFromValue = 1f;
        private const float MoveToValue = 4f;
        private const float MinWidth = 1.1f;
        private const float MaxWidth = 2.3f;

        #endregion

        private TrailRenderer component;
        protected override TrailRenderer Component
        {
            get
            {
                if (component == null)
                {
                    component = GameObject.AddComponent<TrailRenderer>();
                    component.AddPositions(new Vector3[2] { Vector3.zero, Vector3.right });
                }
                return component;
            }
        }

        #region Color Linear

        [UnityTest]
        public IEnumerator ColorLinear()
        {
            Color valueStart = Color.green;
            Color valueEnd = Color.blue;

            yield return CreateTester()
                .Arrange(() =>
                {
                    Component.startColor = Color.clear;
                    Component.endColor = Color.clear;
                })
                .Act(() => Component.Tween(TestTime).ColorLinear(valueStart, valueEnd).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(valueStart, Component.startColor);
                    Assert.AreEqual(valueEnd, Component.endColor);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ColorLinearTo()
        {
            Color toStart = Color.green;
            Color toEnd = Color.blue;

            yield return CreateTester()
                .Arrange(() =>
                {
                    Component.startColor = Color.clear;
                    Component.endColor = Color.clear;
                })
                .Act(() => Component.Tween(TestTime).ColorLinearTo(toStart, toEnd).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(toStart, Component.startColor);
                    Assert.AreEqual(toEnd, Component.endColor);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ColorLinearFromTo()
        {
            Color fromStart = Color.red;
            Color fromEnd = Color.yellow;
            Color toStart = Color.green;
            Color toEnd = Color.blue;
            Color? startingValueStart = null;
            Color? startingValueEnd = null;

            yield return CreateTester()
                .Arrange(() =>
                {
                    Component.startColor = Color.clear;
                    Component.endColor = Color.clear;
                })
                .Act(() => Component.Tween(TestTime).ColorLinearTo(fromStart, fromEnd, toStart, toEnd)
                                    .OnUpdated((_) =>
                                    {
                                        startingValueStart ??= Component.startColor;
                                        startingValueEnd ??= Component.endColor;
                                    })
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(fromStart, startingValueStart);
                    Assert.AreEqual(fromEnd, startingValueEnd);
                    Assert.AreEqual(toStart, Component.startColor);
                    Assert.AreEqual(toEnd, Component.endColor);
                })
                .Run();
        }

        #endregion

        #region Gradient

        [UnityTest]
        public IEnumerator Gradient()
        {
            Gradient value = GradientOperations.Generate(Color.green, Color.blue);

            yield return CreateTester()
                .Arrange(() => Component.colorGradient = GradientOperations.Generate(Color.clear, Color.clear))
                .Act(() => Component.Tween(TestTime).Gradient(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.True(GradientOperations.AreEqual(value, Component.colorGradient)))
                .Run();
        }

        [UnityTest]
        public IEnumerator GradientTo()
        {
            Gradient to = GradientOperations.Generate(Color.green, Color.blue);

            yield return CreateTester()
                .Arrange(() => Component.colorGradient = GradientOperations.Generate(Color.clear, Color.clear))
                .Act(() => Component.Tween(TestTime).GradientTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.True(GradientOperations.AreEqual(to, Component.colorGradient)))
                .Run();
        }

        [UnityTest]
        public IEnumerator GradientFromTo()
        {
            Gradient from = GradientOperations.Generate(Color.red, Color.yellow);
            Gradient to = GradientOperations.Generate(Color.green, Color.blue);
            Gradient startingValue = null;

            yield return CreateTester()
                .Arrange(() => Component.colorGradient = GradientOperations.Generate(Color.clear, Color.clear))
                .Act(() => Component.Tween(TestTime).GradientTo(from, to)
                                    .OnUpdated(_ => startingValue ??= Component.colorGradient)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(GradientOperations.AreEqual(from, startingValue));
                    Assert.True(GradientOperations.AreEqual(to, Component.colorGradient));
                })
                .Run();
        }

        #endregion

        #region Move Vertex Vector

        [UnityTest]
        public IEnumerator MoveVertex()
        {
            Vector3 value = new Vector3(MoveValue, MoveValue, MoveValue);

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveVertex(0, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, Component.GetPosition(0)))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveVertexTo()
        {
            Vector3 to = new Vector3(MoveToValue, MoveToValue, MoveToValue);

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveVertexTo(0, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Component.GetPosition(0)))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveVertexFromTo()
        {
            Vector3 from = new Vector3(MoveFromValue, MoveFromValue, MoveFromValue);
            Vector3 to = new Vector3(MoveToValue, MoveToValue, MoveToValue);
            Vector3? startingFrom = null;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveVertexTo(0, from, to)
                                    .OnUpdated((_) => startingFrom ??= Component.GetPosition(0))
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, Component.GetPosition(0));
                })
                .Run();
        }

        #endregion

        #region Width Linear

        [UnityTest]
        public IEnumerator WidthLinear()
        {
            const float valueStart = MaxWidth;
            const float valueEnd = MaxWidth;

            yield return CreateTester()
                .Arrange(() =>
                {
                    Component.startWidth = 0f;
                    Component.endWidth = 0f;
                })
                .Act(() => Component.Tween(TestTime).WidthLinear(valueStart, valueEnd).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(valueStart, Component.startWidth);
                    Assert.AreEqual(valueEnd, Component.endWidth);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator WidthLinearTo()
        {
            const float toStart = MaxWidth;
            const float toEnd = MaxWidth;

            yield return CreateTester()
                .Arrange(() =>
                {
                    Component.startWidth = 0f;
                    Component.endWidth = 0f;
                })
                .Act(() => Component.Tween(TestTime).WidthLinearTo(toStart, toEnd).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(toStart, Component.startWidth);
                    Assert.AreEqual(toEnd, Component.endWidth);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator WidthLinearFromTo()
        {
            const float fromStart = MinWidth;
            const float fromEnd = MinWidth;
            const float toStart = MaxWidth;
            const float toEnd = MaxWidth;
            float? startingValueStart = null;
            float? startingValueEnd = null;

            yield return CreateTester()
                .Arrange(() =>
                {
                    Component.startWidth = 0f;
                    Component.endWidth = 0f;
                })
                .Act(() => Component.Tween(TestTime).WidthLinearTo(fromStart, fromEnd, toStart, toEnd)
                                    .OnUpdated((_) =>
                                    {
                                        startingValueStart ??= Component.startWidth;
                                        startingValueEnd ??= Component.endWidth;
                                    })
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(fromStart, startingValueStart);
                    Assert.AreEqual(fromEnd, startingValueEnd);
                    Assert.AreEqual(toStart, Component.startWidth);
                    Assert.AreEqual(toEnd, Component.endWidth);
                })
                .Run();
        }

        #endregion
    }
}
