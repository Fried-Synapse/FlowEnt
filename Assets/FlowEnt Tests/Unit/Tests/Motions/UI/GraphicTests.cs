using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class GraphicTests : AbstractUITest
    {
        #region Alpha

        [UnityTest]
        public IEnumerator Alpha()
        {
            const float value = 0.75f;

            yield return CreateTester()
                .Arrange(() => GameObject.GetComponent<Graphic>().color = Color.clear)
                .Act(() => GameObject.GetComponent<Graphic>().Tween(TestTime).Alpha(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, GameObject.GetComponent<Graphic>().color.a))
                .Run();
        }

        [UnityTest]
        public IEnumerator AlphaTo()
        {
            const float to = 0.75f;

            yield return CreateTester()
                .Arrange(() => GameObject.GetComponent<Graphic>().color = Color.clear)
                .Act(() => GameObject.GetComponent<Graphic>().Tween(TestTime).AlphaTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, GameObject.GetComponent<Graphic>().color.a))
                .Run();
        }

        [UnityTest]
        public IEnumerator AlphaFromTo()
        {
            const float from = 0.25f;
            const float to = 0.75f;
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => GameObject.GetComponent<Graphic>().color = Color.clear)
                .Act(() => GameObject.GetComponent<Graphic>().Tween(TestTime).AlphaTo(from, to)
                    .OnUpdated((_) => startingValue ??= GameObject.GetComponent<Graphic>().color.a)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, GameObject.GetComponent<Graphic>().color.a);
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
                .Arrange(() => GameObject.GetComponent<Graphic>().color = Color.clear)
                .Act(() => GameObject.GetComponent<Graphic>().Tween(TestTime).Color(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, GameObject.GetComponent<Graphic>().color))
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourTo()
        {
            Color to = Color.green;

            yield return CreateTester()
                .Arrange(() => GameObject.GetComponent<Graphic>().color = Color.red)
                .Act(() => GameObject.GetComponent<Graphic>().Tween(TestTime).ColorTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, GameObject.GetComponent<Graphic>().color))
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourFromTo()
        {
            Color from = Color.red;
            Color to = Color.green;
            Color? startingValue = null;

            yield return CreateTester()
                .Act(() => GameObject.GetComponent<Graphic>().Tween(TestTime).ColorTo(from, to)
                    .OnUpdated((_) => startingValue ??= GameObject.GetComponent<Graphic>().color)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, GameObject.GetComponent<Graphic>().color);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourToGradient()
        {
            Color? actualFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.GetComponent<Graphic>().Tween(TestTime)
                    .OnUpdated((_) => actualFrom ??= GameObject.GetComponent<Graphic>().color)
                    .ColorTo(Variables.Gradient).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(Variables.Gradient.Evaluate(0f), actualFrom);
                    Assert.AreEqual(Variables.Gradient.Evaluate(1f), GameObject.GetComponent<Graphic>().color);
                })
                .Run();
        }

        #endregion

    }
}