using System.Collections;
using FluentAssertions;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class MaterialTests : AbstractEngineTests
    {
        private Material material;

        private Material Material
        {
            get
            {
                if (material == null)
                {
                    material = GameObject.GetComponent<Renderer>().material;
                }

                return material;
            }
        }

        private const string MainTexPropertyName = "_MainTex";
        private static readonly int MainTexPropertyId = Shader.PropertyToID(MainTexPropertyName);
        private const string ColorPropertyName = "_Color";
        private static readonly int ColorPropertyId = Shader.PropertyToID(ColorPropertyName);
        private const string MetallicPropertyName = "_Metallic";
        private static readonly int MetallicPropertyId = Shader.PropertyToID(MetallicPropertyName);

        [UnityTest]
        public IEnumerator AAAlphaTestToGetMeshMaterialSoTestsWorkOnGithub()
        {
            yield return CreateTester()
                .Arrange(() => Material.color = Color.clear)
                .Act(() => Material.Tween(TestTime).Start())
                .Run();
        }

        #region Float

        [UnityTest]
        public IEnumerator FloatProperty()
        {
            const float value = 0.75f;

            yield return CreateTester()
                .Arrange(() => Material.SetFloat(MetallicPropertyName, 0))
                .Act(() => Material.Tween(TestTime).Float(MetallicPropertyName, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetFloat(MetallicPropertyName).Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator FloatPropertyTo()
        {
            const float to = 0.75f;

            yield return CreateTester()
                .Arrange(() => Material.SetFloat(MetallicPropertyName, 0))
                .Act(() => Material.Tween(TestTime).FloatTo(MetallicPropertyName, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetFloat(MetallicPropertyName).Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator FloatPropertyFromTo()
        {
            const float from = 0.25f;
            const float to = 0.75f;
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Material.SetFloat(MetallicPropertyName, 0))
                .Act(() => Material.Tween(TestTime).FloatTo(MetallicPropertyName, from, to)
                    .OnUpdated(_ => startingValue ??= Material.GetFloat(MetallicPropertyName))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Material.GetFloat(MetallicPropertyName).Should().Be(to);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator FloatPropertyId()
        {
            const float value = 0.75f;

            yield return CreateTester()
                .Arrange(() => Material.SetFloat(MetallicPropertyId, 0))
                .Act(() => Material.Tween(TestTime).Float(MetallicPropertyId, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetFloat(MetallicPropertyId).Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator FloatPropertyIdTo()
        {
            const float to = 0.75f;

            yield return CreateTester()
                .Arrange(() => Material.SetFloat(MetallicPropertyId, 0))
                .Act(() => Material.Tween(TestTime).FloatTo(MetallicPropertyId, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetFloat(MetallicPropertyId).Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator FloatPropertyIdFromTo()
        {
            const float from = 0.25f;
            const float to = 0.75f;
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Material.SetFloat(MetallicPropertyId, 0))
                .Act(() => Material.Tween(TestTime).FloatTo(MetallicPropertyId, from, to)
                    .OnUpdated(_ => startingValue ??= Material.GetFloat(MetallicPropertyId))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Material.GetFloat(MetallicPropertyId).Should().Be(to);
                })
                .Run();
        }

        #endregion

        #region Int

        [UnityTest]
        public IEnumerator IntProperty()
        {
            const int value = 3;

            yield return CreateTester()
                .Arrange(() => Material.SetInt(MetallicPropertyName, 0))
                .Act(() => Material.Tween(TestTime).Int(MetallicPropertyName, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetInt(MetallicPropertyName).Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator IntPropertyTo()
        {
            const int to = 3;

            yield return CreateTester()
                .Arrange(() => Material.SetInt(MetallicPropertyName, 0))
                .Act(() => Material.Tween(TestTime).IntTo(MetallicPropertyName, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetInt(MetallicPropertyName).Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator IntPropertyFromTo()
        {
            const int from = 1;
            const int to = 3;
            int? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Material.SetInt(MetallicPropertyName, 0))
                .Act(() => Material.Tween(TestTime).IntTo(MetallicPropertyName, from, to)
                    .OnUpdated(_ => startingValue ??= Material.GetInt(MetallicPropertyName))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Material.GetInt(MetallicPropertyName).Should().Be(to);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator IntPropertyId()
        {
            const int value = 3;

            yield return CreateTester()
                .Arrange(() => Material.SetInt(MetallicPropertyId, 0))
                .Act(() => Material.Tween(TestTime).Int(MetallicPropertyId, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetInt(MetallicPropertyId).Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator IntPropertyIdTo()
        {
            const int to = 3;

            yield return CreateTester()
                .Arrange(() => Material.SetInt(MetallicPropertyId, 0))
                .Act(() => Material.Tween(TestTime).IntTo(MetallicPropertyId, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetInt(MetallicPropertyId).Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator IntPropertyIdFromTo()
        {
            const int from = 1;
            const int to = 3;
            int? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Material.SetInt(MetallicPropertyId, 0))
                .Act(() => Material.Tween(TestTime).IntTo(MetallicPropertyId, from, to)
                    .OnUpdated(_ => startingValue ??= Material.GetInt(MetallicPropertyId))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Material.GetInt(MetallicPropertyId).Should().Be(to);
                })
                .Run();
        }

        #endregion

        #region Alpha

        [UnityTest]
        public IEnumerator Alpha()
        {
            const float value = 0.75f;

            yield return CreateTester()
                .Arrange(() => Material.color = Color.clear)
                .Act(() => Material.Tween(TestTime).Alpha(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.color.a.Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator AlphaTo()
        {
            const float to = 0.75f;

            yield return CreateTester()
                .Arrange(() => Material.color = Color.clear)
                .Act(() => Material.Tween(TestTime).AlphaTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.color.a.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator AlphaFromTo()
        {
            const float from = 0.25f;
            const float to = 0.75f;
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Material.color = Color.clear)
                .Act(() => Material.Tween(TestTime).AlphaTo(from, to)
                    .OnUpdated(_ => startingValue ??= Material.color.a)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Material.color.a.Should().Be(to);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator AlphaProperty()
        {
            const float value = 0.75f;

            yield return CreateTester()
                .Arrange(() => Material.SetColor(ColorPropertyName, Color.clear))
                .Act(() => Material.Tween(TestTime).Alpha(ColorPropertyName, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetColor(ColorPropertyName).a.Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator AlphaPropertyTo()
        {
            const float to = 0.75f;

            yield return CreateTester()
                .Arrange(() => Material.SetColor(ColorPropertyName, Color.clear))
                .Act(() => Material.Tween(TestTime).AlphaTo(ColorPropertyName, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetColor(ColorPropertyName).a.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator AlphaPropertyFromTo()
        {
            const float from = 0.25f;
            const float to = 0.75f;
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Material.SetColor(ColorPropertyName, Color.clear))
                .Act(() => Material.Tween(TestTime).AlphaTo(ColorPropertyName, from, to)
                    .OnUpdated(_ => startingValue ??= Material.GetColor(ColorPropertyName).a)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Material.GetColor(ColorPropertyName).a.Should().Be(to);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator AlphaPropertyId()
        {
            const float value = 0.75f;

            yield return CreateTester()
                .Arrange(() => Material.SetColor(ColorPropertyId, Color.clear))
                .Act(() => Material.Tween(TestTime).Alpha(ColorPropertyId, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetColor(ColorPropertyId).a.Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator AlphaPropertyIdTo()
        {
            const float to = 0.75f;

            yield return CreateTester()
                .Arrange(() => Material.SetColor(ColorPropertyId, Color.clear))
                .Act(() => Material.Tween(TestTime).AlphaTo(ColorPropertyId, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetColor(ColorPropertyId).a.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator AlphaPropertyIdFromTo()
        {
            const float from = 0.25f;
            const float to = 0.75f;
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Material.SetColor(ColorPropertyId, Color.clear))
                .Act(() => Material.Tween(TestTime).AlphaTo(ColorPropertyId, from, to)
                    .OnUpdated(_ => startingValue ??= Material.GetColor(ColorPropertyId).a)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Material.GetColor(ColorPropertyId).a.Should().Be(to);
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
                .Arrange(() => Material.color = Color.clear)
                .Act(() => Material.Tween(TestTime).Color(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.color.Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourTo()
        {
            Color to = Color.green;

            yield return CreateTester()
                .Arrange(() => Material.color = Color.red)
                .Act(() => Material.Tween(TestTime).ColorTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.color.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourFromTo()
        {
            Color from = Color.red;
            Color to = Color.green;
            Color? startingValue = null;

            yield return CreateTester()
                .Act(() => Material.Tween(TestTime).ColorTo(from, to)
                    .OnUpdated(_ => startingValue ??= Material.color)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Material.color.Should().Be(to);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourToGradient()
        {
            Color? actualFrom = null;

            yield return CreateTester()
                .Act(() => Material.Tween(TestTime)
                    .OnUpdated(_ => actualFrom ??= Material.color)
                    .ColorTo(Variables.Gradient).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    actualFrom.Should().Be(Variables.Gradient.Evaluate(0f));
                    Material.color.Should().Be(Variables.Gradient.Evaluate(1f));
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourProperty()
        {
            Color value = Color.green;

            yield return CreateTester()
                .Arrange(() => Material.color = Color.clear)
                .Act(() => Material.Tween(TestTime).Color(ColorPropertyName, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetColor(ColorPropertyName).Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourPropertyTo()
        {
            Color to = Color.green;

            yield return CreateTester()
                .Arrange(() => Material.SetColor(ColorPropertyName, Color.red))
                .Act(() => Material.Tween(TestTime).ColorTo(ColorPropertyName, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetColor(ColorPropertyName).Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourPropertyFromTo()
        {
            Color from = Color.red;
            Color to = Color.green;
            Color? startingValue = null;

            yield return CreateTester()
                .Act(() => Material.Tween(TestTime).ColorTo(ColorPropertyName, from, to)
                    .OnUpdated(_ => startingValue ??= Material.GetColor(ColorPropertyName))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Material.GetColor(ColorPropertyName).Should().Be(to);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourPropertyToGradient()
        {
            Color? actualFrom = null;

            yield return CreateTester()
                .Act(() => Material.Tween(TestTime)
                    .OnUpdated(_ => actualFrom ??= Material.GetColor(ColorPropertyName))
                    .ColorTo(ColorPropertyName, Variables.Gradient).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    actualFrom.Should().Be(Variables.Gradient.Evaluate(0f));
                    Material.GetColor(ColorPropertyName).Should().Be(Variables.Gradient.Evaluate(1f));
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourPropertyId()
        {
            Color value = Color.green;

            yield return CreateTester()
                .Arrange(() => Material.color = Color.clear)
                .Act(() => Material.Tween(TestTime).Color(ColorPropertyId, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetColor(ColorPropertyId).Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourPropertyIdTo()
        {
            Color to = Color.green;

            yield return CreateTester()
                .Arrange(() => Material.SetColor(ColorPropertyId, Color.red))
                .Act(() => Material.Tween(TestTime).ColorTo(ColorPropertyId, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetColor(ColorPropertyId).Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourPropertyIdFromTo()
        {
            Color from = Color.red;
            Color to = Color.green;
            Color? startingValue = null;

            yield return CreateTester()
                .Act(() => Material.Tween(TestTime).ColorTo(ColorPropertyId, from, to)
                    .OnUpdated(_ => startingValue ??= Material.GetColor(ColorPropertyId))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Material.GetColor(ColorPropertyId).Should().Be(to);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ColourPropertyIdToGradient()
        {
            Color? actualFrom = null;

            yield return CreateTester()
                .Act(() => Material.Tween(TestTime)
                    .OnUpdated(_ => actualFrom ??= Material.GetColor(ColorPropertyId))
                    .ColorTo(ColorPropertyId, Variables.Gradient).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    actualFrom.Should().Be(Variables.Gradient.Evaluate(0f));
                    Material.GetColor(ColorPropertyId).Should().Be(Variables.Gradient.Evaluate(1f));
                })
                .Run();
        }

        #endregion

        #region TextureOffset

        [UnityTest]
        public IEnumerator TextureOffsetProperty()
        {
            Vector2 value = Vector2.one;

            yield return CreateTester()
                .Arrange(() => Material.SetTextureOffset(MainTexPropertyName, Vector2.zero))
                .Act(() => Material.Tween(TestTime).TextureOffset(MainTexPropertyName, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetTextureOffset(MainTexPropertyName).Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator TextureOffsetPropertyTo()
        {
            Vector2 to = Vector2.one;

            yield return CreateTester()
                .Arrange(() => Material.SetTextureOffset(MainTexPropertyName, Vector2.zero))
                .Act(() => Material.Tween(TestTime).TextureOffsetTo(MainTexPropertyName, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetTextureOffset(MainTexPropertyName).Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator TextureOffsetPropertyFromTo()
        {
            Vector2 from = Vector2.zero;
            Vector2 to = Vector2.one;
            Vector2? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Material.SetTextureOffset(MainTexPropertyName, Vector2.zero))
                .Act(() => Material.Tween(TestTime).TextureOffsetTo(MainTexPropertyName, from, to)
                    .OnUpdated(_ => startingValue ??= Material.GetTextureOffset(MainTexPropertyName))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Material.GetTextureOffset(MainTexPropertyName).Should().Be(to);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator TextureOffsetPropertyId()
        {
            Vector2 value = Vector2.one;

            yield return CreateTester()
                .Arrange(() => Material.SetTextureOffset(MainTexPropertyId, Vector2.zero))
                .Act(() => Material.Tween(TestTime).TextureOffset(MainTexPropertyId, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetTextureOffset(MainTexPropertyId).Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator TextureOffsetPropertyIdTo()
        {
            Vector2 to = Vector2.one;

            yield return CreateTester()
                .Arrange(() => Material.SetTextureOffset(MainTexPropertyId, Vector2.zero))
                .Act(() => Material.Tween(TestTime).TextureOffsetTo(MainTexPropertyId, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetTextureOffset(MainTexPropertyId).Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator TextureOffsetPropertyIdFromTo()
        {
            Vector2 from = Vector2.zero;
            Vector2 to = Vector2.one;
            Vector2? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Material.SetTextureOffset(MainTexPropertyId, Vector2.zero))
                .Act(() => Material.Tween(TestTime).TextureOffsetTo(MainTexPropertyId, from, to)
                    .OnUpdated(_ => startingValue ??= Material.GetTextureOffset(MainTexPropertyId))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Material.GetTextureOffset(MainTexPropertyId).Should().Be(to);
                })
                .Run();
        }

        #endregion

        #region TextureScale

        [UnityTest]
        public IEnumerator TextureScaleProperty()
        {
            Vector2 value = Vector2.one;

            yield return CreateTester()
                .Arrange(() => Material.SetTextureScale(MainTexPropertyName, Vector2.zero))
                .Act(() => Material.Tween(TestTime).TextureScale(MainTexPropertyName, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetTextureScale(MainTexPropertyName).Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator TextureScalePropertyTo()
        {
            Vector2 to = Vector2.one;

            yield return CreateTester()
                .Arrange(() => Material.SetTextureScale(MainTexPropertyName, Vector2.zero))
                .Act(() => Material.Tween(TestTime).TextureScaleTo(MainTexPropertyName, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetTextureScale(MainTexPropertyName).Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator TextureScalePropertyFromTo()
        {
            Vector2 from = Vector2.zero;
            Vector2 to = Vector2.one;
            Vector2? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Material.SetTextureScale(MainTexPropertyName, Vector2.zero))
                .Act(() => Material.Tween(TestTime).TextureScaleTo(MainTexPropertyName, from, to)
                    .OnUpdated(_ => startingValue ??= Material.GetTextureScale(MainTexPropertyName))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Material.GetTextureScale(MainTexPropertyName).Should().Be(to);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator TextureScalePropertyId()
        {
            Vector2 value = Vector2.one;

            yield return CreateTester()
                .Arrange(() => Material.SetTextureScale(MainTexPropertyId, Vector2.zero))
                .Act(() => Material.Tween(TestTime).TextureScale(MainTexPropertyId, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetTextureScale(MainTexPropertyId).Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator TextureScalePropertyIdTo()
        {
            Vector2 to = Vector2.one;

            yield return CreateTester()
                .Arrange(() => Material.SetTextureScale(MainTexPropertyId, Vector2.zero))
                .Act(() => Material.Tween(TestTime).TextureScaleTo(MainTexPropertyId, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetTextureScale(MainTexPropertyId).Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator TextureScalePropertyIdFromTo()
        {
            Vector2 from = Vector2.zero;
            Vector2 to = Vector2.one;
            Vector2? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Material.SetTextureScale(MainTexPropertyId, Vector2.zero))
                .Act(() => Material.Tween(TestTime).TextureScaleTo(MainTexPropertyId, from, to)
                    .OnUpdated(_ => startingValue ??= Material.GetTextureScale(MainTexPropertyId))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Material.GetTextureScale(MainTexPropertyId).Should().Be(to);
                })
                .Run();
        }

        #endregion

        #region Vector

        [UnityTest]
        public IEnumerator VectorProperty()
        {
            Vector4 value = Vector4.one;

            yield return CreateTester()
                .Arrange(() => Material.SetVector(ColorPropertyName, Vector4.zero))
                .Act(() => Material.Tween(TestTime).Vector(ColorPropertyName, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetVector(ColorPropertyName).Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator VectorPropertyTo()
        {
            Vector4 to = Vector4.one;

            yield return CreateTester()
                .Arrange(() => Material.SetVector(ColorPropertyName, Vector4.zero))
                .Act(() => Material.Tween(TestTime).VectorTo(ColorPropertyName, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetVector(ColorPropertyName).Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator VectorPropertyFromTo()
        {
            Vector4 from = Vector4.zero;
            Vector4 to = Vector4.one;
            Vector4? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Material.SetVector(ColorPropertyName, Vector4.zero))
                .Act(() => Material.Tween(TestTime).VectorTo(ColorPropertyName, from, to)
                    .OnUpdated(_ => startingValue ??= Material.GetVector(ColorPropertyName))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Material.GetVector(ColorPropertyName).Should().Be(to);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator VectorPropertyId()
        {
            Vector4 value = Vector4.one;

            yield return CreateTester()
                .Arrange(() => Material.SetVector(ColorPropertyId, Vector4.zero))
                .Act(() => Material.Tween(TestTime).Vector(ColorPropertyId, value).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetVector(ColorPropertyId).Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator VectorPropertyIdTo()
        {
            Vector4 to = Vector4.one;

            yield return CreateTester()
                .Arrange(() => Material.SetVector(ColorPropertyId, Vector4.zero))
                .Act(() => Material.Tween(TestTime).VectorTo(ColorPropertyId, to).Start())
                .AssertTime(TestTime)
                .Assert(() => Material.GetVector(ColorPropertyId).Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator VectorPropertyIdFromTo()
        {
            Vector4 from = Vector4.zero;
            Vector4 to = Vector4.one;
            Vector4? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Material.SetVector(ColorPropertyId, Vector4.zero))
                .Act(() => Material.Tween(TestTime).VectorTo(ColorPropertyId, from, to)
                    .OnUpdated(_ => startingValue ??= Material.GetVector(ColorPropertyId))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    Material.GetVector(ColorPropertyId).Should().Be(to);
                })
                .Run();
        }

        #endregion
    }
}