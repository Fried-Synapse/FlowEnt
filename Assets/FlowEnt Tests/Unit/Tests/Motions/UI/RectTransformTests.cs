using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using FriedSynapse.FlowEnt.Motions.Tween.UI.RectTransforms;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class RectTransformTests : AbstractUITest
    {
        #region MoveAnchoredPosition

        #region MoveAnchoredPosition

        private const float MoveAnchoredPositionValue = 400f;

        [UnityTest]
        public IEnumerator MoveAnchoredPosition()
        {
            Vector2 value = new Vector2(MoveAnchoredPositionValue, MoveAnchoredPositionValue);

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchoredPosition(value).Start())
                .AssertTime(TestTime)
                .Assert(() => RectTransform.anchoredPosition.Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchoredPositionX()
        {
            const float value = MoveAnchoredPositionValue;

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchoredPositionX(value).Start())
                .AssertTime(TestTime)
                .Assert(() => RectTransform.anchoredPosition.Should().Be(new Vector2(value, 0)))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchoredPositionY()
        {
            const float value = MoveAnchoredPositionValue;

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchoredPositionY(value).Start())
                .AssertTime(TestTime)
                .Assert(() => RectTransform.anchoredPosition.Should().Be(new Vector2(0, value)))
                .Run();
        }

        #endregion

        #region MoveAnchoredPositionTo

        private const float MoveAnchoredPositionFromValue = 100f;
        private const float MoveAnchoredPositionToValue = 400f;

        [UnityTest]
        public IEnumerator MoveAnchoredPositionTo()
        {
            Vector2 to = new Vector2(MoveAnchoredPositionToValue, MoveAnchoredPositionToValue);

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchoredPositionTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => RectTransform.anchoredPosition.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchoredPositionXTo()
        {
            const float to = MoveAnchoredPositionToValue;

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchoredPositionXTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => RectTransform.anchoredPosition.x.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchoredPositionYTo()
        {
            const float to = MoveAnchoredPositionToValue;

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchoredPositionYTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => RectTransform.anchoredPosition.y.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchoredPositionFromTo()
        {
            Vector2 from = new Vector2(MoveAnchoredPositionFromValue, MoveAnchoredPositionFromValue);
            Vector2 to = new Vector2(MoveAnchoredPositionToValue, MoveAnchoredPositionToValue);
            Vector2? startingFrom = null;

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchoredPositionTo(from, to)
                    .OnUpdated(_ => startingFrom ??= RectTransform.anchoredPosition)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFrom.Should().Be(from);
                    RectTransform.anchoredPosition.Should().Be(to);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchoredPositionXFromTo()
        {
            const float from = MoveAnchoredPositionFromValue;
            const float to = MoveAnchoredPositionToValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchoredPositionXTo(from, to)
                    .OnUpdated(_ => startingFrom ??= RectTransform.anchoredPosition.x)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFrom.Should().Be(from);
                    RectTransform.anchoredPosition.x.Should().Be(to);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchoredPositionYFromTo()
        {
            const float from = MoveAnchoredPositionFromValue;
            const float to = MoveAnchoredPositionToValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchoredPositionYTo(from, to)
                    .OnUpdated(_ => startingFrom ??= RectTransform.anchoredPosition.y)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFrom.Should().Be(from);
                    RectTransform.anchoredPosition.y.Should().Be(to);
                })
                .Run();
        }

        #endregion

        #region MoveAnchoredPositionTo Spline

        private const float MoveAnchoredPositionToSplineValue = 400f;

        private List<Vector3> GetSpline(Vector3 to)
        {
            return new List<Vector3>()
            {
                new(0, 0),
                new(0, 200),
                new(0, 300),
                new(500, 400),
                new(0, 800),
                to
            };
        }

        [UnityTest]
        public IEnumerator MoveAnchoredPositionToSpline_Linear()
        {
            Vector2 to = new Vector2(MoveAnchoredPositionToSplineValue, MoveAnchoredPositionToSplineValue);

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchoredPositionTo(new LinearSpline(GetSpline(to)))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() => RectTransform.anchoredPosition.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchoredPositionToSpline_Bezier()
        {
            Vector2 to = new Vector2(MoveAnchoredPositionToSplineValue, MoveAnchoredPositionToSplineValue);

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchoredPositionTo(new BSpline(GetSpline(to))).Start())
                .AssertTime(TestTime)
                .Assert(() => RectTransform.anchoredPosition.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchoredPositionToSpline_CatmullRom()
        {
            Vector2 endPoint = new Vector2(MoveAnchoredPositionToSplineValue, MoveAnchoredPositionToSplineValue);
            List<Vector3> spline = GetSpline(endPoint);
            Vector2 to = spline[spline.Count - 2];

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchoredPositionTo(new CatmullRomSpline(spline)).Start())
                .AssertTime(TestTime)
                .Assert(() => RectTransform.anchoredPosition.Should().Be(to))
                .Run();
        }

        #endregion

        #endregion

        #region MoveAnchor

        private const float MoveAnchorFromValue = 0f;
        private const float MoveAnchorToValue = 1f;

        [UnityTest]
        public IEnumerator MoveAnchor()
        {
            Vector2 fromMin = new Vector2(MoveAnchorFromValue, MoveAnchorFromValue);
            Vector2 fromMax = new Vector2(MoveAnchorFromValue, MoveAnchorFromValue);

            Vector2 valueMin = new Vector2(MoveAnchorToValue, MoveAnchorToValue);
            Vector2 valueMax = new Vector2(MoveAnchorToValue, MoveAnchorToValue);

            Vector2 expectedMin = fromMin + valueMin;
            Vector2 expectedMax = fromMax + valueMax;

            yield return CreateTester()
                .Arrange(() =>
                {
                    RectTransform.anchorMin = fromMin;
                    RectTransform.anchorMax = fromMax;
                })
                .Act(() => RectTransform.Tween(TestTime).MoveAnchor(valueMin, valueMax).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    RectTransform.anchorMin.Should().Be(expectedMin);
                    RectTransform.anchorMax.Should().Be(expectedMax);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchorTo()
        {
            Vector2 toMin = new Vector2(MoveAnchorToValue, MoveAnchorToValue);
            Vector2 toMax = new Vector2(MoveAnchorToValue, MoveAnchorToValue);

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchorTo(toMin, toMax).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    RectTransform.anchorMin.Should().Be(toMin);
                    RectTransform.anchorMax.Should().Be(toMax);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchorFromTo()
        {
            Vector2 fromMin = new Vector2(MoveAnchorFromValue, MoveAnchorFromValue);
            Vector2 fromMax = new Vector2(MoveAnchorFromValue, MoveAnchorFromValue);
            Vector2 toMin = new Vector2(MoveAnchorToValue, MoveAnchorToValue);
            Vector2 toMax = new Vector2(MoveAnchorToValue, MoveAnchorToValue);
            Vector2? startingFromMin = null;
            Vector2? startingFromMax = null;

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchorTo(fromMin, fromMax, toMin, toMax)
                    .OnUpdated(_ =>
                    {
                        startingFromMin ??= RectTransform.anchorMin;
                        startingFromMax ??= RectTransform.anchorMax;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFromMin.Should().Be(fromMin);
                    startingFromMax.Should().Be(fromMax);
                    RectTransform.anchorMin.Should().Be(toMin);
                    RectTransform.anchorMax.Should().Be(toMax);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchorPreset()
        {
            AnchorPresetData fromData = new AnchorPresetData(
                new Vector3(MoveAnchorFromValue, MoveAnchorFromValue),
                new Vector3(MoveAnchorFromValue, MoveAnchorFromValue));
            const AnchorPreset value = AnchorPreset.TopRight;
            AnchorPresetData valueData = AnchorPresetFactory.GetAnchors(value);
            AnchorPresetData expectedData = fromData + valueData;

            yield return CreateTester()
                .Arrange(() =>
                {
                    RectTransform.anchorMin = fromData.Min;
                    RectTransform.anchorMax = fromData.Max;
                })
                .Act(() => RectTransform.Tween(TestTime).MoveAnchor(value).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    RectTransform.anchorMin.Should().Be(expectedData.Min);
                    RectTransform.anchorMax.Should().Be(expectedData.Max);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchorToPreset()
        {
            const AnchorPreset to = AnchorPreset.TopRight;
            AnchorPresetData toData = AnchorPresetFactory.GetAnchors(to);

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchorTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    RectTransform.anchorMin.Should().Be(toData.Min);
                    RectTransform.anchorMax.Should().Be(toData.Max);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchorFromToPreset()
        {
            const AnchorPreset from = AnchorPreset.BottomLeft;
            const AnchorPreset to = AnchorPreset.TopRight;
            AnchorPresetData fromData = AnchorPresetFactory.GetAnchors(from);
            AnchorPresetData toData = AnchorPresetFactory.GetAnchors(to);
            Vector2? startingFromMin = null;
            Vector2? startingFromMax = null;

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchorTo(from, to)
                    .OnUpdated(_ =>
                    {
                        startingFromMin ??= RectTransform.anchorMin;
                        startingFromMax ??= RectTransform.anchorMax;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFromMin.Should().Be(fromData.Min);
                    startingFromMax.Should().Be(fromData.Max);
                    RectTransform.anchorMin.Should().Be(toData.Min);
                    RectTransform.anchorMax.Should().Be(toData.Max);
                })
                .Run();
        }

        #endregion

        #region MovePivot

        private const float MovePivotFromValue = 0f;
        private const float MovePivotToValue = 1f;

        [UnityTest]
        public IEnumerator MovePivot()
        {
            Vector2 from = new Vector2(MovePivotToValue, MovePivotToValue);
            Vector2 value = new Vector2(MovePivotToValue, MovePivotToValue);
            Vector2 expected = from + value;

            yield return CreateTester()
                .Arrange(() => RectTransform.pivot = from)
                .Act(() => RectTransform.Tween(TestTime).MovePivot(value).Start())
                .AssertTime(TestTime)
                .Assert(() => RectTransform.pivot.Should().Be(expected))
                .Run();
        }

        [UnityTest]
        public IEnumerator MovePivotTo()
        {
            Vector2 to = new Vector2(MovePivotToValue, MovePivotToValue);

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MovePivotTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => RectTransform.pivot.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator MovePivotFromTo()
        {
            Vector2 from = new Vector2(MovePivotFromValue, MovePivotFromValue);
            Vector2 to = new Vector2(MovePivotToValue, MovePivotToValue);
            Vector2? startingFrom = null;

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MovePivotTo(from, to)
                    .OnUpdated(_ => startingFrom ??= RectTransform.pivot)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFrom.Should().Be(from);
                    RectTransform.pivot.Should().Be(to);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MovePivotPreset()
        {
            Vector2 fromVector = new Vector2(MovePivotToValue, MovePivotToValue);
            PivotPreset value = PivotPreset.TopRight;
            Vector2 valueVector = PivotPresetFactory.GetPivot(value);
            Vector2 expectedVector = fromVector + valueVector;

            yield return CreateTester()
                .Arrange(() => RectTransform.pivot = fromVector)
                .Act(() => RectTransform.Tween(TestTime).MovePivot(value).Start())
                .AssertTime(TestTime)
                .Assert(() => RectTransform.pivot.Should().Be(expectedVector))
                .Run();
        }

        [UnityTest]
        public IEnumerator MovePivotToPreset()
        {
            const PivotPreset to = PivotPreset.TopRight;
            Vector2 toVector = PivotPresetFactory.GetPivot(to);

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MovePivotTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => RectTransform.pivot.Should().Be(toVector))
                .Run();
        }

        [UnityTest]
        public IEnumerator MovePivotFromToPreset()
        {
            const PivotPreset from = PivotPreset.BottomLeft;
            const PivotPreset to = PivotPreset.TopRight;
            Vector2 fromVector = PivotPresetFactory.GetPivot(from);
            Vector2 toVector = PivotPresetFactory.GetPivot(to);
            Vector2? startingFrom = null;

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MovePivotTo(from, to)
                    .OnUpdated(_ => startingFrom ??= RectTransform.pivot)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFrom.Should().Be(fromVector);
                    RectTransform.pivot.Should().Be(toVector);
                })
                .Run();
        }

        #endregion

        #region ScaleSizeDelta

        #region ScaleSizeDelta

        private const float ScaleValue = 2f;

        [UnityTest]
        public IEnumerator ScaleSizeDelta()
        {
            Vector2 value = new Vector2(ScaleValue, ScaleValue);
            Vector2 targetValue = Vector2.zero;

            yield return CreateTester()
                .Arrange(() => targetValue = Vector2.Scale(RectTransform.sizeDelta, value))
                .Act(() => RectTransform.Tween(TestTime).ScaleSizeDelta(value).Start())
                .AssertTime(TestTime)
                .Assert(() => RectTransform.sizeDelta.Should().Be(targetValue))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleSizeDeltaX()
        {
            const float value = ScaleValue;
            float targetValue = 0;

            yield return CreateTester()
                .Arrange(() => targetValue = RectTransform.sizeDelta.x * value)
                .Act(() => RectTransform.Tween(TestTime).ScaleSizeDeltaX(value).Start())
                .AssertTime(TestTime)
                .Assert(() => RectTransform.sizeDelta.x.Should().Be(targetValue))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleSizeDeltaY()
        {
            const float value = ScaleValue;
            float targetValue = 0;

            yield return CreateTester()
                .Arrange(() => targetValue = RectTransform.sizeDelta.y * value)
                .Act(() => RectTransform.Tween(TestTime).ScaleSizeDeltaY(value).Start())
                .AssertTime(TestTime)
                .Assert(() => RectTransform.sizeDelta.y.Should().Be(targetValue))
                .Run();
        }

        #endregion

        #region ScaleSizeDeltaFromTo

        private const float ScaleFrom = 200f;
        private const float ScaleTo = 400f;

        [UnityTest]
        public IEnumerator ScaleSizeDeltaTo()
        {
            Vector2 from = new Vector2(ScaleFrom, ScaleFrom);
            Vector2 to = new Vector2(ScaleTo, ScaleTo);

            yield return CreateTester()
                .Arrange(() => RectTransform.sizeDelta = from)
                .Act(() => RectTransform.Tween(TestTime).ScaleSizeDeltaTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => RectTransform.sizeDelta.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleSizeDeltaFromTo()
        {
            Vector2 from = new Vector2(ScaleFrom, ScaleFrom);
            Vector2 to = new Vector2(ScaleTo, ScaleTo);
            Vector2? startingFrom = null;

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).ScaleSizeDeltaTo(from, to)
                    .OnUpdated(_ => startingFrom ??= RectTransform.sizeDelta)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFrom.Should().Be(from);
                    RectTransform.sizeDelta.Should().Be(to);
                })
                .Run();
        }

        #endregion

        #endregion
    }
}