using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class RendererTests : AbstractEngineTests
    {
        private MeshRenderer meshRenderer;

        private MeshRenderer MeshRenderer
        {
            get
            {
                if (meshRenderer == null)
                {
                    meshRenderer = GameObject.GetComponent<MeshRenderer>();
                }
                return meshRenderer;
            }
        }

        private const string ColorPropertyName = "_Color";
        private const string FloatPropertyName = "_Metallic";

        [UnityTest]
        public IEnumerator AAAlphaTestToGetMeshRendererSoTestsWorkOnGithub()
        {
            yield return CreateTester()
                .Arrange(() => MeshRenderer.material.color = Color.clear)
                .Act(() => MeshRenderer.Tween(TestTime).Start())
                .Assert(() => Assert.True(true))
                .Run();
        }

        #region Alpha

        [UnityTest]
        public IEnumerator Alpha()
        {
            const float value = 0.75f;

            yield return CreateTester()
                .Arrange(() => MeshRenderer.material.color = Color.clear)
                .Act(() => MeshRenderer.Tween(TestTime).Alpha(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, MeshRenderer.material.color.a))
                .Run();
        }

        [UnityTest]
        public IEnumerator AlphaTo()
        {
            const float to = 0.75f;

            yield return CreateTester()
                .Arrange(() => MeshRenderer.material.color = Color.clear)
                .Act(() => MeshRenderer.Tween(TestTime).AlphaTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, MeshRenderer.material.color.a))
                .Run();
        }

        [UnityTest]
        public IEnumerator AlphaFromTo()
        {
            const float from = 0.25f;
            const float to = 0.75f;
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => MeshRenderer.material.color = Color.clear)
                .Act(() => MeshRenderer.Tween(TestTime).AlphaTo(from, to)
                                    .OnUpdated(_ => startingValue ??= MeshRenderer.material.color.a)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, MeshRenderer.material.color.a);
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
                .Arrange(() => MeshRenderer.material.color = Color.clear)
                .Act(() => MeshRenderer.Tween(TestTime).Color(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, MeshRenderer.material.color))
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourTo()
        {
            Color to = Color.green;

            yield return CreateTester()
                .Arrange(() => MeshRenderer.material.color = Color.red)
                .Act(() => MeshRenderer.Tween(TestTime).ColorTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, MeshRenderer.material.color))
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourFromTo()
        {
            Color from = Color.red;
            Color to = Color.green;
            Color? startingValue = null;

            yield return CreateTester()
                .Act(() => MeshRenderer.Tween(TestTime).ColorTo(from, to)
                                    .OnUpdated((_) => startingValue ??= MeshRenderer.material.color)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, MeshRenderer.material.color);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourToGradient()
        {
            Color? actualFrom = null;

            yield return CreateTester()
                .Act(() => MeshRenderer.Tween(TestTime)
                    .OnUpdated((_) => actualFrom ??= MeshRenderer.material.color)
                    .ColorTo(Variables.Gradient).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(Variables.Gradient.Evaluate(0f), actualFrom);
                    Assert.AreEqual(Variables.Gradient.Evaluate(1f), MeshRenderer.material.color);
                })
                .Run();
        }

        #endregion

        #region Material Alpha

        [UnityTest]
        public IEnumerator MaterialFloat()
        {
            const float value = 0.75f;

            yield return CreateTester()
                .Arrange(() => MeshRenderer.material.SetFloat(FloatPropertyName, 0))
                .Act(() => MeshRenderer.Tween(TestTime).MaterialFloat(FloatPropertyName, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, MeshRenderer.material.GetFloat(FloatPropertyName)))
                .Run();
        }

        [UnityTest]
        public IEnumerator MaterialFloatTo()
        {
            const float to = 0.75f;

            yield return CreateTester()
                .Arrange(() => MeshRenderer.material.SetFloat(FloatPropertyName, 0))
                .Act(() => MeshRenderer.Tween(TestTime).MaterialFloatTo(FloatPropertyName, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, MeshRenderer.material.GetFloat(FloatPropertyName)))
                .Run();
        }

        [UnityTest]
        public IEnumerator MaterialFloatFromTo()
        {
            const float from = 0.25f;
            const float to = 0.75f;
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => MeshRenderer.material.SetFloat(FloatPropertyName, 0))
                .Act(() => MeshRenderer.Tween(TestTime).MaterialFloatTo(FloatPropertyName, from, to)
                                    .OnUpdated(_ => startingValue ??= MeshRenderer.material.GetFloat(FloatPropertyName))
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, MeshRenderer.material.GetFloat(FloatPropertyName));
                })
                .Run();
        }

        #endregion

        #region Material Alpha

        [UnityTest]
        public IEnumerator MaterialAlpha()
        {
            const float value = 0.75f;

            yield return CreateTester()
                .Arrange(() => MeshRenderer.material.SetColor(ColorPropertyName, Color.clear))
                .Act(() => MeshRenderer.Tween(TestTime).MaterialAlpha(ColorPropertyName, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, MeshRenderer.material.GetColor(ColorPropertyName).a))
                .Run();
        }

        [UnityTest]
        public IEnumerator MaterialAlphaTo()
        {
            const float to = 0.75f;

            yield return CreateTester()
                .Arrange(() => MeshRenderer.material.SetColor(ColorPropertyName, Color.clear))
                .Act(() => MeshRenderer.Tween(TestTime).MaterialAlphaTo(ColorPropertyName, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, MeshRenderer.material.GetColor(ColorPropertyName).a))
                .Run();
        }

        [UnityTest]
        public IEnumerator MaterialAlphaFromTo()
        {
            const float from = 0.25f;
            const float to = 0.75f;
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => MeshRenderer.material.SetColor(ColorPropertyName, Color.clear))
                .Act(() => MeshRenderer.Tween(TestTime).MaterialAlphaTo(ColorPropertyName, from, to)
                                    .OnUpdated(_ => startingValue ??= MeshRenderer.material.GetColor(ColorPropertyName).a)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, MeshRenderer.material.GetColor(ColorPropertyName).a);
                })
                .Run();
        }

        #endregion

        #region Material Color

        [UnityTest]
        public IEnumerator MaterialColour()
        {
            Color value = Color.green;

            yield return CreateTester()
                .Arrange(() => MeshRenderer.material.color = Color.clear)
                .Act(() => MeshRenderer.Tween(TestTime).MaterialColor(ColorPropertyName, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, MeshRenderer.material.GetColor(ColorPropertyName)))
                .Run();
        }

        [UnityTest]
        public IEnumerator MaterialColourTo()
        {
            Color to = Color.green;

            yield return CreateTester()
                .Arrange(() => MeshRenderer.material.SetColor(ColorPropertyName, Color.red))
                .Act(() => MeshRenderer.Tween(TestTime).MaterialColorTo(ColorPropertyName, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, MeshRenderer.material.GetColor(ColorPropertyName)))
                .Run();
        }

        [UnityTest]
        public IEnumerator MaterialColourFromTo()
        {
            Color from = Color.red;
            Color to = Color.green;
            Color? startingValue = null;

            yield return CreateTester()
                .Act(() => MeshRenderer.Tween(TestTime).MaterialColorTo(ColorPropertyName, from, to)
                                    .OnUpdated((_) => startingValue ??= MeshRenderer.material.GetColor(ColorPropertyName))
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, MeshRenderer.material.GetColor(ColorPropertyName));
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MaterialColourToGradient()
        {
            Color? actualFrom = null;

            yield return CreateTester()
                .Act(() => MeshRenderer.Tween(TestTime)
                    .OnUpdated((_) => actualFrom ??= MeshRenderer.material.GetColor(ColorPropertyName))
                    .MaterialColorTo(ColorPropertyName, Variables.Gradient).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(Variables.Gradient.Evaluate(0f), actualFrom);
                    Assert.AreEqual(Variables.Gradient.Evaluate(1f), MeshRenderer.material.GetColor(ColorPropertyName));
                })
                .Run();
        }

        #endregion
    }
}