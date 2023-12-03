using System.Collections;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Tween.UI.RectTransforms;
using NUnit.Framework;
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
                .Assert(() => Assert.AreEqual(value, RectTransform.anchoredPosition))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchoredPositionX()
        {
            const float value = MoveAnchoredPositionValue;

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchoredPositionX(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(new Vector2(value, 0), RectTransform.anchoredPosition))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchoredPositionY()
        {
            const float value = MoveAnchoredPositionValue;

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchoredPositionY(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(new Vector2(0, value), RectTransform.anchoredPosition))
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
                .Assert(() => Assert.AreEqual(to, RectTransform.anchoredPosition))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchoredPositionXTo()
        {
            const float to = MoveAnchoredPositionToValue;

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchoredPositionXTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, RectTransform.anchoredPosition.x))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchoredPositionYTo()
        {
            const float to = MoveAnchoredPositionToValue;

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchoredPositionYTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, RectTransform.anchoredPosition.y))
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
                    .OnUpdated((_) => startingFrom ??= RectTransform.anchoredPosition)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, RectTransform.anchoredPosition);
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
                    .OnUpdated((_) => startingFrom ??= RectTransform.anchoredPosition.x)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, RectTransform.anchoredPosition.x);
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
                    .OnUpdated((_) => startingFrom ??= RectTransform.anchoredPosition.y)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, RectTransform.anchoredPosition.y);
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
                new Vector3(0, 0),
                new Vector3(0, 200),
                new Vector3(0, 300),
                new Vector3(500, 400),
                new Vector3(0, 800),
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
                .Assert(() => Assert.AreEqual(to, RectTransform.anchoredPosition))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveAnchoredPositionToSpline_Bezier()
        {
            Vector2 to = new Vector2(MoveAnchoredPositionToSplineValue, MoveAnchoredPositionToSplineValue);

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MoveAnchoredPositionTo(new BSpline(GetSpline(to))).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, RectTransform.anchoredPosition))
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
                .Assert(() => Assert.AreEqual(to, RectTransform.anchoredPosition))
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
                    Assert.AreEqual(expectedMin, RectTransform.anchorMin);
                    Assert.AreEqual(expectedMax, RectTransform.anchorMax);
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
                    Assert.AreEqual(toMin, RectTransform.anchorMin);
                    Assert.AreEqual(toMax, RectTransform.anchorMax);
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
                    .OnUpdated((_) =>
                    {
                        startingFromMin ??= RectTransform.anchorMin;
                        startingFromMax ??= RectTransform.anchorMax;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(fromMin, startingFromMin);
                    Assert.AreEqual(fromMax, startingFromMax);
                    Assert.AreEqual(toMin, RectTransform.anchorMin);
                    Assert.AreEqual(toMax, RectTransform.anchorMax);
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
                    Assert.AreEqual(expectedData.Min, RectTransform.anchorMin);
                    Assert.AreEqual(expectedData.Max, RectTransform.anchorMax);
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
                    Assert.AreEqual(toData.Min, RectTransform.anchorMin);
                    Assert.AreEqual(toData.Max, RectTransform.anchorMax);
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
                    .OnUpdated((_) =>
                    {
                        startingFromMin ??= RectTransform.anchorMin;
                        startingFromMax ??= RectTransform.anchorMax;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(fromData.Min, startingFromMin);
                    Assert.AreEqual(fromData.Max, startingFromMax);
                    Assert.AreEqual(toData.Min, RectTransform.anchorMin);
                    Assert.AreEqual(toData.Max, RectTransform.anchorMax);
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
                .Assert(() => Assert.AreEqual(expected, RectTransform.pivot))
                .Run();
        }

        [UnityTest]
        public IEnumerator MovePivotTo()
        {
            Vector2 to = new Vector2(MovePivotToValue, MovePivotToValue);

            yield return CreateTester()
                .Act(() => RectTransform.Tween(TestTime).MovePivotTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, RectTransform.pivot))
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
                    .OnUpdated((_) => startingFrom ??= RectTransform.pivot)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, RectTransform.pivot);
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
                .Assert(() => Assert.AreEqual(expectedVector, RectTransform.pivot))
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
                .Assert(() => Assert.AreEqual(toVector, RectTransform.pivot))
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
                    .OnUpdated((_) => startingFrom ??= RectTransform.pivot)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(fromVector, startingFrom);
                    Assert.AreEqual(toVector, RectTransform.pivot);
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
                .Assert(() => Assert.AreEqual(targetValue, RectTransform.sizeDelta))
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
                .Assert(() => Assert.AreEqual(targetValue, RectTransform.sizeDelta.x))
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
                .Assert(() => Assert.AreEqual(targetValue, RectTransform.sizeDelta.y))
                .Run();
        }

        #endregion

        #region ScaleSizeDeltaTo

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
                .Assert(() => Assert.AreEqual(to, RectTransform.sizeDelta))
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
                    .OnUpdated((_) => startingFrom ??= RectTransform.sizeDelta)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, RectTransform.sizeDelta);
                })
                .Run();
        }

        #endregion

        #endregion
    }
}