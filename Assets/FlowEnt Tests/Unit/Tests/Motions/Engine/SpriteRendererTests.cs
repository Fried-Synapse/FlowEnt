using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class SpriteRendererTests : AbstractEngineTests<SpriteRenderer>
    {
        #region Constants
        private const float SizeValue = 4f;
        private const float SizeFromValue = 1f;
        private const float SizeToValue = 4f;
        #endregion
        protected override GameObject CreateObject()
            => new GameObject();

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

        #region Size

        [UnityTest]
        public IEnumerator Size()
        {
            Vector2 value = new Vector2(SizeValue, SizeValue);

            yield return CreateTester()
                .Arrange(() => Component.size = Vector2.zero)
                .Act(() => Component.Tween(TestTime).Size(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, Component.size))
                .Run();
        }

        [UnityTest]
        public IEnumerator SizeTo()
        {
            Vector2 from = new Vector2(SizeFromValue, SizeFromValue);
            Vector2 to = new Vector2(SizeToValue, SizeToValue);

            yield return CreateTester()
                .Arrange(() => Component.size = from)
                .Act(() => Component.Tween(TestTime).SizeTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Component.size))
                .Run();
        }

        [UnityTest]
        public IEnumerator SizeFromTo()
        {
            Vector2 from = new Vector2(SizeFromValue, SizeFromValue);
            Vector2 to = new Vector2(SizeToValue, SizeToValue);
            Vector2? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Component.size = Vector2.zero)
                .Act(() => Component.Tween(TestTime).SizeTo(from, to)
                                    .OnUpdated(_ => startingValue ??= Component.size)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, Component.size);
                })
                .Run();
        }

        #endregion
    }
}