using System.Collections;
using FluentAssertions;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class CanvasGroupTests : AbstractUITest
    {
        protected override void PrepareObject(RectTransform rectTransform)
        {
            base.PrepareObject(rectTransform);
            rectTransform.gameObject.AddComponent<CanvasGroup>();
        }

        #region Alpha

        [UnityTest]
        public IEnumerator Alpha()
        {
            const float value = 0.75f;

            yield return CreateTester()
                .Arrange(() => GameObject.GetComponent<CanvasGroup>().alpha = 0)
                .Act(() => RectTransform.GetComponent<CanvasGroup>().Tween(TestTime).Alpha(value).Start())
                .AssertTime(TestTime)
                .Assert(() => GameObject.GetComponent<CanvasGroup>().alpha.Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator AlphaTo()
        {
            const float value = 0.75f;

            yield return CreateTester()
                .Arrange(() => GameObject.GetComponent<CanvasGroup>().alpha = 0)
                .Act(() => RectTransform.GetComponent<CanvasGroup>().Tween(TestTime).AlphaTo(value).Start())
                .AssertTime(TestTime)
                .Assert(() => GameObject.GetComponent<CanvasGroup>().alpha.Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator AlphaFromTo()
        {
            const float from = 0.25f;
            const float to = 0.75f;
            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => GameObject.GetComponent<CanvasGroup>().alpha = 0)
                .Act(() => RectTransform.GetComponent<CanvasGroup>().Tween(TestTime).AlphaTo(from, to)
                    .OnUpdated(_ => startingValue ??= GameObject.GetComponent<CanvasGroup>().alpha)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingValue.Should().Be(from);
                    GameObject.GetComponent<CanvasGroup>().alpha.Should().Be(to);
                })
                .Run();
        }

        #endregion
    }
}