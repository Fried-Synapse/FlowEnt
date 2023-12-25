using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class TransformTweenTests : AbstractEngineTests
    {
        #region Constants

        private const float MoveValue = 4f;
        private const float MoveFromValue = 1f;
        private const float MoveToValue = 4f;
        private const float MoveToSplineValue = 4f;

        private List<Vector3> GetSpline(Vector3 to)
        {
            return new List<Vector3>()
            {
                new (0, 0, 0),
                new (0, 2, 0),
                new (0, 3, 3),
                new (5, 4, 3),
                new (0, 8, 0),
                to
            };
        }

        private const float RotateXValue = 20f;
        private const float RotateYValue = 40f;
        private const float RotateZValue = 80f;
        private const float FullCircle = 360f;

        private const float ScaleValue = 2f;
        private const float ScaleFrom = 2f;
        private const float ScaleTo = 4f;

        #endregion

        #region Move

        #region Move Vector

        [UnityTest]
        public IEnumerator Move()
        {
            Vector3 from = new(MoveFromValue, MoveFromValue, MoveFromValue);
            Vector3 value = new(MoveValue, MoveValue, MoveValue);
            Vector3 expected = from + value;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = from)
                .Act(() => GameObject.transform.Tween(TestTime).Move(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(expected, GameObject.transform.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveTo()
        {
            Vector3 from = new (MoveFromValue, MoveFromValue, MoveFromValue);
            Vector3 to = new (MoveToValue, MoveToValue, MoveToValue);

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = from)
                .Act(() => GameObject.transform.Tween(TestTime).MoveTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, GameObject.transform.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveFromTo()
        {
            Vector3 from = new (MoveFromValue, MoveFromValue, MoveFromValue);
            Vector3 to = new (MoveToValue, MoveToValue, MoveToValue);
            Vector3? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).MoveTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.position)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, GameObject.transform.position);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocal()
        {
            Vector3 from = new (MoveFromValue, MoveFromValue, MoveFromValue);
            Vector3 value = new (MoveValue, MoveValue, MoveValue);
            Vector3 expected = from + value;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = from)
                .Act(() => GameObject.transform.Tween(TestTime).MoveLocal(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(expected, GameObject.transform.localPosition))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalTo()
        {
            Vector3 from = new (MoveFromValue, MoveFromValue, MoveFromValue);
            Vector3 to = new (MoveToValue, MoveToValue, MoveToValue);

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = from)
                .Act(() => GameObject.transform.Tween(TestTime).MoveLocalTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, GameObject.transform.localPosition))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalFromTo()
        {
            Vector3 from = new (MoveFromValue, MoveFromValue, MoveFromValue);
            Vector3 to = new (MoveToValue, MoveToValue, MoveToValue);
            Vector3? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).MoveLocalTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.localPosition)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, GameObject.transform.localPosition);
                })
                .Run();
        }

        #endregion

        #region Move Axis

        #region Move Axis X

        [UnityTest]
        public IEnumerator MoveX()
        {
            const float from = MoveFromValue;
            const float value = MoveValue;
            const float expected = from + value;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (from, 0, 0))
                .Act(() => GameObject.transform.Tween(TestTime).MoveX(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(new Vector3(expected, 0, 0), GameObject.transform.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToX()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (from, 0, 0))
                .Act(() => GameObject.transform.Tween(TestTime).MoveXTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, GameObject.transform.position.x))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveFromToX()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).MoveXTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.position.x)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, GameObject.transform.position.x);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalX()
        {
            const float from = MoveFromValue;
            const float value = MoveValue;
            const float expected = from + value;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (from, 0, 0))
                .Act(() => GameObject.transform.Tween(TestTime).MoveLocalX(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(new Vector3(expected, 0, 0), GameObject.transform.localPosition))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalToX()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (from, 0, 0))
                .Act(() => GameObject.transform.Tween(TestTime).MoveLocalXTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, GameObject.transform.localPosition.x))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalFromToX()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).MoveLocalXTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.localPosition.x)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, GameObject.transform.localPosition.x);
                })
                .Run();
        }

        #endregion

        #region Move Axis Y

        [UnityTest]
        public IEnumerator MoveY()
        {
            const float from = MoveFromValue;
            const float value = MoveValue;
            const float expected = from + value;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (0, from, 0))
                .Act(() => GameObject.transform.Tween(TestTime).MoveY(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(new Vector3(0, expected, 0), GameObject.transform.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToY()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (from, 0, 0))
                .Act(() => GameObject.transform.Tween(TestTime).MoveYTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, GameObject.transform.position.y))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveFromToY()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).MoveYTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.position.y)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, GameObject.transform.position.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalY()
        {
            const float from = MoveFromValue;
            const float value = MoveValue;
            const float expected = from + value;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (0, from, 0))
                .Act(() => GameObject.transform.Tween(TestTime).MoveLocalY(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(new Vector3(0, expected, 0), GameObject.transform.localPosition))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalToY()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (0, from, 0))
                .Act(() => GameObject.transform.Tween(TestTime).MoveLocalYTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, GameObject.transform.localPosition.y))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalFromToY()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).MoveLocalYTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.localPosition.y)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, GameObject.transform.localPosition.y);
                })
                .Run();
        }

        #endregion

        #region Move Axis Z

        [UnityTest]
        public IEnumerator MoveZ()
        {
            const float from = MoveFromValue;
            const float value = MoveValue;
            const float expected = from + value;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (0, 0, from))
                .Act(() => GameObject.transform.Tween(TestTime).MoveZ(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(new Vector3(0, 0, expected), GameObject.transform.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToZ()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (0, 0, from))
                .Act(() => GameObject.transform.Tween(TestTime).MoveZTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, GameObject.transform.position.z))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveFromToZ()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).MoveZTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.position.z)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, GameObject.transform.position.z);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalZ()
        {
            const float from = MoveFromValue;
            const float value = MoveValue;
            const float expected = from + value;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (0, 0, from))
                .Act(() => GameObject.transform.Tween(TestTime).MoveLocalZ(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(new Vector3(0, 0, expected), GameObject.transform.localPosition))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalToZ()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (0, 0, from))
                .Act(() => GameObject.transform.Tween(TestTime).MoveLocalZTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, GameObject.transform.localPosition.z))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalFromToZ()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).MoveLocalZTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.localPosition.z)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, GameObject.transform.localPosition.z);
                })
                .Run();
        }

        #endregion

        #region Move Axis Mixed

        [UnityTest]
        public IEnumerator MoveXY()
        {
            const float from = MoveFromValue;
            const float value = MoveValue;
            const float expected = from + value;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (from, from, 0))
                .Act(() => new Tween(TestTime).For(GameObject.transform).Move(Axis.XY, value).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expected, GameObject.transform.position.x);
                    Assert.AreEqual(expected, GameObject.transform.position.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToXY()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (from, from, 0))
                .Act(() => new Tween(TestTime).For(GameObject.transform).MoveTo(Axis.XY, to).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(to, GameObject.transform.position.x);
                    Assert.AreEqual(to, GameObject.transform.position.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveFromToXY()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFromX = null;
            float? startingFromY = null;

            yield return CreateTester()
                .Act(() => new Tween(TestTime).For(GameObject.transform).MoveTo(Axis.XY, from, to)
                    .OnUpdated((_) =>
                    {
                        startingFromX ??= GameObject.transform.position.x;
                        startingFromY ??= GameObject.transform.position.y;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFromX);
                    Assert.AreEqual(from, startingFromY);
                    Assert.AreEqual(to, GameObject.transform.position.x);
                    Assert.AreEqual(to, GameObject.transform.position.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalXY()
        {
            const float from = MoveFromValue;
            const float value = MoveValue;
            const float expected = from + value;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (from, from, 0))
                .Act(() => new Tween(TestTime).For(GameObject.transform).MoveLocal(Axis.XY, value).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expected, GameObject.transform.localPosition.x);
                    Assert.AreEqual(expected, GameObject.transform.localPosition.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalToXY()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (from, from, 0))
                .Act(() => new Tween(TestTime).For(GameObject.transform).MoveLocalTo(Axis.XY, to).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(to, GameObject.transform.localPosition.x);
                    Assert.AreEqual(to, GameObject.transform.localPosition.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalFromToXY()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFromX = null;
            float? startingFromY = null;

            yield return CreateTester()
                .Act(() => new Tween(TestTime).For(GameObject.transform).MoveLocalTo(Axis.XY, from, to)
                    .OnUpdated((_) =>
                    {
                        startingFromX ??= GameObject.transform.localPosition.x;
                        startingFromY ??= GameObject.transform.localPosition.y;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFromX);
                    Assert.AreEqual(from, startingFromY);
                    Assert.AreEqual(to, GameObject.transform.localPosition.x);
                    Assert.AreEqual(to, GameObject.transform.localPosition.y);
                })
                .Run();
        }

        #endregion

        #region Move Axis Separated

        [UnityTest]
        public IEnumerator MoveXY_Parallel()
        {
            const float from = MoveFromValue;
            const float value = MoveValue;
            const float expected = from + value;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (from, from, 0))
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(TestTime).MoveX(value))
                    .At(0, GameObject.transform.Tween(TestTime).MoveY(value))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expected, GameObject.transform.position.x);
                    Assert.AreEqual(expected, GameObject.transform.position.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveXY_Sequence()
        {
            const float from = MoveFromValue;
            const float value = MoveValue;
            const float expectedX = from + value;
            const float expectedY = expectedX * 2;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (from, from * 2, 0))
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(HalfTestTime).Move(Axis.XY, value))
                    .Queue(GameObject.transform.Tween(HalfTestTime).MoveY(value))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expectedX, GameObject.transform.position.x);
                    Assert.AreEqual(expectedY, GameObject.transform.position.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToXY_Parallel()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (from, from, 0))
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(TestTime).MoveXTo(to))
                    .At(0, GameObject.transform.Tween(TestTime).MoveYTo(to))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(to, GameObject.transform.position.x);
                    Assert.AreEqual(to, GameObject.transform.position.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToXY_Sequence()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (from, from, 0))
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(HalfTestTime).MoveTo(Axis.XY, to))
                    .Queue(GameObject.transform.Tween(HalfTestTime).MoveYTo(to))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(to, GameObject.transform.position.x);
                    Assert.AreEqual(to, GameObject.transform.position.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveFromToXY_Parallel()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFromX = null;
            float? startingFromY = null;

            yield return CreateTester()
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(TestTime).MoveXTo(from, to))
                    .At(0, GameObject.transform.Tween(TestTime).MoveYTo(from, to))
                    .OnUpdated((_) =>
                    {
                        startingFromX ??= GameObject.transform.position.x;
                        startingFromY ??= GameObject.transform.position.y;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFromX);
                    Assert.AreEqual(from, startingFromY);
                    Assert.AreEqual(to, GameObject.transform.position.x);
                    Assert.AreEqual(to, GameObject.transform.position.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveFromToXY_Sequence()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFromX = null;
            float? startingFromY = null;

            yield return CreateTester()
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(HalfTestTime).MoveTo(Axis.XY, from, to))
                    .Queue(GameObject.transform.Tween(HalfTestTime).MoveYTo(from, to))
                    .OnUpdated((_) =>
                    {
                        startingFromX ??= GameObject.transform.position.x;
                        startingFromY ??= GameObject.transform.position.y;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFromX);
                    Assert.AreEqual(from, startingFromY);
                    Assert.AreEqual(to, GameObject.transform.position.x);
                    Assert.AreEqual(to, GameObject.transform.position.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalXY_Parallel()
        {
            const float from = MoveFromValue;
            const float value = MoveValue;
            const float expected = from + value;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (from, from, 0))
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(TestTime).MoveLocalX(value))
                    .At(0, GameObject.transform.Tween(TestTime).MoveLocalY(value))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expected, GameObject.transform.localPosition.x);
                    Assert.AreEqual(expected, GameObject.transform.localPosition.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalXY_Sequence()
        {
            const float from = MoveFromValue;
            const float value = MoveValue;
            const float expectedX = from + value;
            const float expectedY = value + value;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (from, 0, 0))
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(HalfTestTime).MoveLocal(Axis.XY, value))
                    .Queue(GameObject.transform.Tween(HalfTestTime).MoveLocalY(value))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expectedX, GameObject.transform.localPosition.x);
                    Assert.AreEqual(expectedY, GameObject.transform.localPosition.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalToXY_Parallel()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (from, from, 0))
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(TestTime).MoveLocalXTo(to))
                    .At(0, GameObject.transform.Tween(TestTime).MoveLocalYTo(to))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(to, GameObject.transform.localPosition.x);
                    Assert.AreEqual(to, GameObject.transform.localPosition.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalToXY_Sequence()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new (from, from, 0))
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(HalfTestTime).MoveLocalTo(Axis.XY, to))
                    .Queue(GameObject.transform.Tween(HalfTestTime).MoveLocalYTo(to))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(to, GameObject.transform.localPosition.x);
                    Assert.AreEqual(to, GameObject.transform.localPosition.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalFromToXY_Parallel()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFromX = null;
            float? startingFromY = null;

            yield return CreateTester()
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(TestTime).MoveLocalXTo(from, to))
                    .At(0, GameObject.transform.Tween(TestTime).MoveLocalYTo(from, to))
                    .OnUpdated((_) =>
                    {
                        startingFromX ??= GameObject.transform.localPosition.x;
                        startingFromY ??= GameObject.transform.localPosition.y;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFromX);
                    Assert.AreEqual(from, startingFromY);
                    Assert.AreEqual(to, GameObject.transform.localPosition.x);
                    Assert.AreEqual(to, GameObject.transform.localPosition.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalFromToXY_Sequence()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFromX = null;
            float? startingFromY = null;

            yield return CreateTester()
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(HalfTestTime).MoveLocalTo(Axis.XY, from, to))
                    .Queue(GameObject.transform.Tween(HalfTestTime).MoveLocalYTo(from, to))
                    .OnUpdated((_) =>
                    {
                        startingFromX ??= GameObject.transform.localPosition.x;
                        startingFromY ??= GameObject.transform.localPosition.y;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFromX);
                    Assert.AreEqual(from, startingFromY);
                    Assert.AreEqual(to, GameObject.transform.localPosition.x);
                    Assert.AreEqual(to, GameObject.transform.localPosition.y);
                })
                .Run();
        }

        #endregion

        #endregion

        #region MoveTo AnimationCurve3d

        [UnityTest]
        public IEnumerator MoveToAnimationCurve3d()
        {
            Vector3? actualFrom = null;
            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime)
                    .OnUpdated((_) => actualFrom ??= GameObject.transform.position)
                    .MoveTo(Variables.AnimationCurve).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(Variables.AnimationCurve.Evaluate(0f), actualFrom);
                    Assert.AreEqual(Variables.AnimationCurve.Evaluate(1f), GameObject.transform.position);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalToAnimationCurve3d()
        {
            Vector3 to = new (MoveToValue, MoveToValue, MoveToValue);

            Vector3? actualFrom = null;
            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime)
                    .OnUpdated((_) => actualFrom ??= GameObject.transform.localPosition)
                    .MoveLocalTo(Variables.AnimationCurve).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(Variables.AnimationCurve.Evaluate(0f), actualFrom);
                    Assert.AreEqual(Variables.AnimationCurve.Evaluate(1f), GameObject.transform.localPosition);
                })
                .Run();
        }

        #endregion

        #region MoveTo Spline

        [UnityTest]
        public IEnumerator MoveToSpline_Linear()
        {
            Vector3 to = new (MoveToSplineValue, MoveToSplineValue, MoveToSplineValue);

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).MoveTo(new LinearSpline(GetSpline(to))).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, GameObject.transform.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalToSpline_Linear()
        {
            Vector3 to = new (MoveToSplineValue, MoveToSplineValue, MoveToSplineValue);

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).MoveLocalTo(new LinearSpline(GetSpline(to))).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, GameObject.transform.localPosition))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToSpline_Bezier()
        {
            Vector3 to = new (MoveToSplineValue, MoveToSplineValue, MoveToSplineValue);

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).MoveTo(new BSpline(GetSpline(to))).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, GameObject.transform.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveLocalToSpline_Bezier()
        {
            Vector3 to = new (MoveToSplineValue, MoveToSplineValue, MoveToSplineValue);

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).MoveLocalTo(new BSpline(GetSpline(to))).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, GameObject.transform.localPosition))
                .Run();
        }

        #endregion

        #endregion

        #region Rotate

        #region Rotate Quaternion

        [UnityTest]
        public IEnumerator RotateQuaternion()
        {
            Quaternion from = Quaternion.Euler(new (-RotateXValue, 0f, 0f));
            Quaternion value = Quaternion.Euler(new (RotateXValue, RotateYValue, 0f));
            Quaternion expected = from * value;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = from)
                .Act(() => GameObject.transform.Tween(TestTime).Rotate(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(expected, GameObject.transform.rotation))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateToQuaternion()
        {
            Quaternion from = Quaternion.Euler(new (-RotateXValue, 0f, 0f));
            Quaternion to = Quaternion.Euler(new (RotateXValue, RotateYValue, RotateZValue));

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = from)
                .Act(() => GameObject.transform.Tween(TestTime).RotateTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, GameObject.transform.rotation))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateFromToQuaternion()
        {
            Quaternion from =
                Quaternion.Euler(new (RotateXValue, FullCircle - RotateYValue, FullCircle - RotateZValue));
            Quaternion to = Quaternion.Euler(new (RotateXValue, RotateYValue, RotateZValue));
            Quaternion? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).RotateTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.rotation)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    FlowEntAssert.AreEqual(from, startingFrom.Value);
                    FlowEntAssert.AreEqual(to, GameObject.transform.rotation);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateLocalQuaternion()
        {
            Quaternion from = Quaternion.Euler(new (-RotateXValue, 0f, 0f));
            Quaternion value = Quaternion.Euler(new (RotateXValue, RotateYValue, 0f));
            Quaternion expected = from * value;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = from)
                .Act(() => GameObject.transform.Tween(TestTime).RotateLocal(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(expected, GameObject.transform.rotation))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateLocalToQuaternion()
        {
            Quaternion from = Quaternion.Euler(new (-RotateXValue, 0f, 0f));
            Quaternion to = Quaternion.Euler(new (RotateXValue, RotateYValue, RotateZValue));

            yield return CreateTester()
                .Arrange(() => GameObject.transform.localRotation = from)
                .Act(() => GameObject.transform.Tween(TestTime).RotateLocalTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, GameObject.transform.localRotation))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateLocalFromToQuaternion()
        {
            Quaternion from =
                Quaternion.Euler(new (RotateXValue, FullCircle - RotateYValue, FullCircle - RotateZValue));
            Quaternion to = Quaternion.Euler(new (RotateXValue, RotateYValue, RotateZValue));
            Quaternion? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).RotateLocalTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.localRotation)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    FlowEntAssert.AreEqual(from, startingFrom.Value);
                    FlowEntAssert.AreEqual(to, GameObject.transform.localRotation);
                })
                .Run();
        }

        #endregion

        #region Rotate Vector

        [UnityTest]
        public IEnumerator RotateVector()
        {
            Vector3 from = new (-RotateXValue, 0f, 0f);
            Vector3 value = new (RotateXValue, RotateYValue, 0f);
            Vector3 expected = from + value;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = Quaternion.Euler(from))
                .Act(() => GameObject.transform.Tween(TestTime).Rotate(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(expected, GameObject.transform.rotation.eulerAngles))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateToVector()
        {
            Vector3 from = new (-RotateXValue, 0f, 0f);
            Vector3 to = new (RotateXValue, RotateYValue, RotateZValue);

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = Quaternion.Euler(from))
                .Act(() => GameObject.transform.Tween(TestTime).RotateTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, GameObject.transform.rotation.eulerAngles))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateFromToVector()
        {
            Vector3 from = new (RotateXValue, FullCircle - RotateYValue, FullCircle - RotateZValue);
            Vector3 to = new (RotateXValue, RotateYValue, RotateZValue);
            Vector3? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).RotateTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.rotation.eulerAngles)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    FlowEntAssert.AreEqual(from, startingFrom.Value);
                    FlowEntAssert.AreEqual(to, GameObject.transform.rotation.eulerAngles);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateLocalToVector()
        {
            Vector3 from = new (-RotateXValue, 0f, 0f);
            Vector3 expected = new (RotateXValue, RotateYValue, RotateZValue);

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = Quaternion.Euler(from))
                .Act(() => GameObject.transform.Tween(TestTime).RotateLocalTo(expected).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(expected, GameObject.transform.localRotation.eulerAngles))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateLocalFromToVector()
        {
            Vector3 from = new (RotateXValue, FullCircle - RotateYValue, FullCircle - RotateZValue);
            Vector3 to = new (RotateXValue, RotateYValue, RotateZValue);
            Vector3? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).RotateLocalTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.localRotation.eulerAngles)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    FlowEntAssert.AreEqual(from, startingFrom.Value);
                    FlowEntAssert.AreEqual(to, GameObject.transform.localRotation.eulerAngles);
                })
                .Run();
        }

        #endregion

        #region Rotate Axis

        #region Rotate Axis X

        [UnityTest]
        public IEnumerator RotateX()
        {
            const float from = -RotateXValue;
            const float value = (RotateXValue * 2) + FullCircle;
            const float expected = RotateXValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = Quaternion.Euler(new (from, 0f, 0f)))
                .Act(() => GameObject.transform.Tween(TestTime).RotateX(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(expected, GameObject.transform.rotation.eulerAngles.x))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateToX()
        {
            const float from = -RotateXValue;
            const float to = RotateXValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = Quaternion.Euler(new (from, 0f, 0f)))
                .Act(() => GameObject.transform.Tween(TestTime).RotateXTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, GameObject.transform.rotation.eulerAngles.x))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateFromToX()
        {
            const float from = -RotateXValue;
            const float to = RotateXValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).RotateXTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.rotation.eulerAngles.x)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    FlowEntAssert.AreEqual(from, startingFrom.Value);
                    FlowEntAssert.AreEqual(to, GameObject.transform.rotation.eulerAngles.x);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateLocalX()
        {
            const float from = -RotateXValue;
            const float value = (RotateXValue * 2) + FullCircle;
            const float expected = RotateXValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = Quaternion.Euler(new (from, 0f, 0f)))
                .Act(() => GameObject.transform.Tween(TestTime).RotateLocalX(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(expected, GameObject.transform.localRotation.eulerAngles.x))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateLocalToX()
        {
            const float from = -RotateXValue;
            const float to = RotateXValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = Quaternion.Euler(new (from, 0f, 0f)))
                .Act(() => GameObject.transform.Tween(TestTime).RotateLocalXTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, GameObject.transform.localRotation.eulerAngles.x))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateLocalFromToX()
        {
            const float from = -RotateXValue;
            const float to = RotateXValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).RotateLocalXTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.localRotation.eulerAngles.x)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    FlowEntAssert.AreEqual(from, startingFrom.Value);
                    FlowEntAssert.AreEqual(to, GameObject.transform.localRotation.eulerAngles.x);
                })
                .Run();
        }

        #endregion

        #region Rotate Axis Y

        [UnityTest]
        public IEnumerator RotateY()
        {
            const float from = -RotateYValue;
            const float value = (RotateYValue * 2) + FullCircle;
            const float expected = RotateYValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = Quaternion.Euler(new (0f, from, 0f)))
                .Act(() => GameObject.transform.Tween(TestTime).RotateY(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(expected, GameObject.transform.rotation.eulerAngles.y))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateToY()
        {
            const float from = -RotateYValue;
            const float to = RotateYValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = Quaternion.Euler(new (0f, from, 0f)))
                .Act(() => GameObject.transform.Tween(TestTime).RotateYTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, GameObject.transform.rotation.eulerAngles.y))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateFromToY()
        {
            const float from = -RotateYValue;
            const float to = RotateYValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).RotateYTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.rotation.eulerAngles.y)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    FlowEntAssert.AreEqual(from, startingFrom.Value);
                    FlowEntAssert.AreEqual(to, GameObject.transform.rotation.eulerAngles.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateLocalY()
        {
            const float from = -RotateYValue;
            const float value = (RotateYValue * 2) + FullCircle;
            const float expected = RotateYValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = Quaternion.Euler(new (0f, from, 0f)))
                .Act(() => GameObject.transform.Tween(TestTime).RotateLocalY(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(expected, GameObject.transform.localRotation.eulerAngles.y))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateLocalToY()
        {
            const float from = -RotateYValue;
            const float to = RotateYValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = Quaternion.Euler(new (0f, from, 0f)))
                .Act(() => GameObject.transform.Tween(TestTime).RotateLocalYTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, GameObject.transform.localRotation.eulerAngles.y))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateLocalFromToY()
        {
            const float from = -RotateYValue;
            const float to = RotateYValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).RotateLocalYTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.localRotation.eulerAngles.y)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    FlowEntAssert.AreEqual(from, startingFrom.Value);
                    FlowEntAssert.AreEqual(to, GameObject.transform.localRotation.eulerAngles.y);
                })
                .Run();
        }

        #endregion

        #region Rotate Axis Z

        [UnityTest]
        public IEnumerator RotateZ()
        {
            const float from = -RotateZValue;
            const float value = (RotateZValue * 2) + FullCircle;
            const float expected = RotateZValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = Quaternion.Euler(new (0f, 0f, from)))
                .Act(() => GameObject.transform.Tween(TestTime).RotateZ(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(expected, GameObject.transform.rotation.eulerAngles.z))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateToZ()
        {
            const float from = -RotateZValue;
            const float to = RotateZValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = Quaternion.Euler(new (0f, 0f, from)))
                .Act(() => GameObject.transform.Tween(TestTime).RotateZTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, GameObject.transform.rotation.eulerAngles.z))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateFromToZ()
        {
            const float from = -RotateZValue;
            const float to = RotateZValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).RotateZTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.rotation.eulerAngles.z)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    FlowEntAssert.AreEqual(from, startingFrom.Value);
                    FlowEntAssert.AreEqual(to, GameObject.transform.rotation.eulerAngles.z);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateLocalZ()
        {
            const float from = -RotateZValue;
            const float value = (RotateZValue * 2) + FullCircle;
            const float expected = RotateZValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = Quaternion.Euler(new (0f, 0f, from)))
                .Act(() => GameObject.transform.Tween(TestTime).RotateLocalY(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(expected, GameObject.transform.localRotation.eulerAngles.z))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateLocalToZ()
        {
            const float from = -RotateZValue;
            const float to = RotateZValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.rotation = Quaternion.Euler(new (0f, 0f, from)))
                .Act(() => GameObject.transform.Tween(TestTime).RotateLocalZTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, GameObject.transform.localRotation.eulerAngles.z))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateLocalFromToZ()
        {
            const float from = -RotateZValue;
            const float to = RotateZValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).RotateLocalZTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.localRotation.eulerAngles.z)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    FlowEntAssert.AreEqual(from, startingFrom.Value);
                    FlowEntAssert.AreEqual(to, GameObject.transform.localRotation.eulerAngles.z);
                })
                .Run();
        }

        #endregion

        #endregion

        #region RotateAround

        [UnityTest]
        public IEnumerator RotateAroundTransform()
        {
            Transform point = null;

            yield return CreateTester()
                .Arrange(() =>
                {
                    GameObject.transform.position = Vector3.left;
                    point = new GameObject().transform;
                    point.position = Vector3.zero;
                })
                .Act(() => GameObject.transform.Tween(TestTime).RotateAround(Vector3.zero, Vector3.up, 90f).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(Vector3.forward, GameObject.transform.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateAroundVector()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = Vector3.left)
                .Act(() => GameObject.transform.Tween(TestTime).RotateAround(Vector3.zero, Vector3.up, 90f).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(Vector3.forward, GameObject.transform.position))
                .Run();
        }

        #endregion

        #region OrientToPath

        [UnityTest]
        public IEnumerator OrientToPath()
        {
            Vector3 from = new (1, 1, 0);
            Vector3 to = new (3, 3, 0);
            List<Vector3> values = new List<Vector3>();
            Vector3 orientation = new (315f, 90f, 0f);

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).MoveTo(from, to).OrientToPath()
                    .OnUpdated((_) => values.Add(GameObject.transform.eulerAngles))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() => values.ForEach(v => FlowEntAssert.AreEqual(v, orientation)))
                .Run();
        }

        #endregion

        #endregion

        #region Scale

        #region Scale Vector

        [UnityTest]
        public IEnumerator ScaleLocal()
        {
            Vector3 from = new (ScaleFrom, ScaleFrom, ScaleFrom);
            Vector3 value = new (ScaleValue, ScaleValue, ScaleValue);
            Vector3 expected = Vector3.Scale(from, value);

            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = from)
                .Act(() => GameObject.transform.Tween(TestTime).ScaleLocal(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(expected, GameObject.transform.localScale))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleLocalTo()
        {
            Vector3 from = new (ScaleFrom, ScaleFrom, ScaleFrom);
            Vector3 to = new (ScaleTo, ScaleTo, ScaleTo);

            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = from)
                .Act(() => GameObject.transform.Tween(TestTime).ScaleLocalTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, GameObject.transform.localScale))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleLocalFromTo()
        {
            Vector3 from = new (ScaleFrom, ScaleFrom, ScaleFrom);
            Vector3 to = new (ScaleTo, ScaleTo, ScaleTo);
            Vector3? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).ScaleLocalTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.localScale)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, GameObject.transform.localScale);
                })
                .Run();
        }

        #endregion

        #region Scale Axis

        #region Scale Axis X

        [UnityTest]
        public IEnumerator ScaleLocalX()
        {
            const float from = ScaleFrom;
            const float value = ScaleValue;
            const float expected = ScaleFrom * ScaleValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = new (from, 1f, 1f))
                .Act(() => GameObject.transform.Tween(TestTime).ScaleLocalX(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(expected, GameObject.transform.localScale.x))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleLocalToX()
        {
            const float from = ScaleFrom;
            const float to = ScaleTo;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = new (from, 1f, 1f))
                .Act(() => GameObject.transform.Tween(TestTime).ScaleLocalXTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, GameObject.transform.localScale.x))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleLocalFromToX()
        {
            const float from = ScaleFrom;
            const float to = ScaleTo;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).ScaleLocalXTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.localScale.x)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, GameObject.transform.localScale.x);
                })
                .Run();
        }

        #endregion

        #region Scale Axis Y

        [UnityTest]
        public IEnumerator ScaleLocalY()
        {
            const float from = ScaleFrom;
            const float value = ScaleValue;
            const float expected = ScaleFrom * ScaleValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = new (1f, from, 1f))
                .Act(() => GameObject.transform.Tween(TestTime).ScaleLocalY(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(expected, GameObject.transform.localScale.y))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleLocalToY()
        {
            const float from = ScaleFrom;
            const float to = ScaleTo;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = new (1f, from, 1f))
                .Act(() => GameObject.transform.Tween(TestTime).ScaleLocalYTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, GameObject.transform.localScale.y))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleLocalFromToY()
        {
            const float from = ScaleFrom;
            const float to = ScaleTo;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).ScaleLocalYTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.localScale.y)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, GameObject.transform.localScale.y);
                })
                .Run();
        }

        #endregion

        #region Scale Axis Z

        [UnityTest]
        public IEnumerator ScaleLocalZ()
        {
            const float from = ScaleFrom;
            const float value = ScaleValue;
            const float expected = ScaleFrom * ScaleValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = new (1f, 1f, from))
                .Act(() => GameObject.transform.Tween(TestTime).ScaleLocalZ(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(expected, GameObject.transform.localScale.z))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleLocalToZ()
        {
            const float from = ScaleFrom;
            const float to = ScaleTo;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = new (1f, 1f, from))
                .Act(() => GameObject.transform.Tween(TestTime).ScaleLocalZTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, GameObject.transform.localScale.z))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleLocalFromToZ()
        {
            const float from = ScaleFrom;
            const float to = ScaleTo;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime).ScaleLocalZTo(from, to)
                    .OnUpdated((_) => startingFrom ??= GameObject.transform.localScale.z)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, GameObject.transform.localScale.z);
                })
                .Run();
        }

        #endregion

        #region Scale Axis Mixed

        [UnityTest]
        public IEnumerator ScaleLocalXY()
        {
            const float from = ScaleFrom;
            const float value = ScaleValue;
            const float expected = ScaleFrom * ScaleValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = new (from, from, 1f))
                .Act(() => new Tween(TestTime).For(GameObject.transform).ScaleLocal(Axis.XY, value).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expected, GameObject.transform.localScale.x);
                    Assert.AreEqual(expected, GameObject.transform.localScale.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleLocalToXY()
        {
            const float from = ScaleFrom;
            const float to = ScaleTo;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = new (from, from, 1f))
                .Act(() => new Tween(TestTime).For(GameObject.transform).ScaleLocalTo(Axis.XY, to).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(to, GameObject.transform.localScale.x);
                    Assert.AreEqual(to, GameObject.transform.localScale.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleLocalFromToXY()
        {
            const float from = ScaleFrom;
            const float to = ScaleTo;
            float? startingFromX = null;
            float? startingFromY = null;

            yield return CreateTester()
                .Act(() => new Tween(TestTime).For(GameObject.transform).ScaleLocalTo(Axis.XY, from, to)
                    .OnUpdated((_) =>
                    {
                        startingFromX ??= GameObject.transform.localScale.x;
                        startingFromY ??= GameObject.transform.localScale.y;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFromX);
                    Assert.AreEqual(from, startingFromY);
                    Assert.AreEqual(to, GameObject.transform.localScale.x);
                    Assert.AreEqual(to, GameObject.transform.localScale.y);
                })
                .Run();
        }

        #endregion

        #region Scale Axis Separated

        [UnityTest]
        public IEnumerator ScaleLocalXY_Parallel()
        {
            const float from = ScaleFrom;
            const float value = ScaleValue;
            const float expected = ScaleFrom * ScaleValue;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = new (from, from, 1f))
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(TestTime).ScaleLocalX(value))
                    .At(0, GameObject.transform.Tween(TestTime).ScaleLocalY(value))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expected, GameObject.transform.localScale.x);
                    Assert.AreEqual(expected, GameObject.transform.localScale.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleLocalXY_Sequence()
        {
            const float from = ScaleFrom;
            const float value = ScaleValue;
            const float expectedX = ScaleFrom * ScaleValue;
            const float expectedY = expectedX * 2;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = new (from, from * 2, 1f))
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(HalfTestTime).ScaleLocal(Axis.XY, value))
                    .Queue(GameObject.transform.Tween(HalfTestTime).ScaleLocalY(value))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expectedX, GameObject.transform.localScale.x);
                    Assert.AreEqual(expectedY, GameObject.transform.localScale.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleLocalToXY_Parallel()
        {
            const float from = ScaleFrom;
            const float to = ScaleTo;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = new (from, from, 1f))
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(TestTime).ScaleLocalXTo(to))
                    .At(0, GameObject.transform.Tween(TestTime).ScaleLocalYTo(to))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(to, GameObject.transform.localScale.x);
                    Assert.AreEqual(to, GameObject.transform.localScale.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleLocalToXY_Sequence()
        {
            const float from = ScaleFrom;
            const float to = ScaleTo;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = new (from, from, 1f))
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(HalfTestTime).ScaleLocalTo(Axis.XY, to))
                    .Queue(GameObject.transform.Tween(HalfTestTime).ScaleLocalYTo(to))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(to, GameObject.transform.localScale.x);
                    Assert.AreEqual(to, GameObject.transform.localScale.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleLocalFromToXY_Parallel()
        {
            const float from = ScaleFrom;
            const float to = ScaleTo;
            float? startingFromX = null;
            float? startingFromY = null;

            yield return CreateTester()
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(TestTime).ScaleLocalXTo(from, to))
                    .At(0, GameObject.transform.Tween(TestTime).ScaleLocalYTo(from, to))
                    .OnUpdated((_) =>
                    {
                        startingFromX ??= GameObject.transform.localScale.x;
                        startingFromY ??= GameObject.transform.localScale.y;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFromX);
                    Assert.AreEqual(from, startingFromY);
                    Assert.AreEqual(to, GameObject.transform.localScale.x);
                    Assert.AreEqual(to, GameObject.transform.localScale.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleLocalFromToXY_Sequence()
        {
            const float from = ScaleFrom;
            const float to = ScaleTo;
            float? startingFromX = null;
            float? startingFromY = null;

            yield return CreateTester()
                .Act(() => new Flow()
                    .Queue(GameObject.transform.Tween(HalfTestTime).ScaleLocalTo(Axis.XY, from, to))
                    .Queue(GameObject.transform.Tween(HalfTestTime).ScaleLocalYTo(from, to))
                    .OnUpdated((_) =>
                    {
                        startingFromX ??= GameObject.transform.localScale.x;
                        startingFromY ??= GameObject.transform.localScale.y;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFromX);
                    Assert.AreEqual(from, startingFromY);
                    Assert.AreEqual(to, GameObject.transform.localScale.x);
                    Assert.AreEqual(to, GameObject.transform.localScale.y);
                })
                .Run();
        }

        #endregion

        #endregion

        #region Scale AnimationCurve3d

        [UnityTest]
        public IEnumerator ScaleLocalToAnimationCurve3d()
        {
            Vector3? actualFrom = null;

            yield return CreateTester()
                .Act(() => GameObject.transform.Tween(TestTime)
                    .OnUpdated(_ => actualFrom ??= GameObject.transform.localScale)
                    .ScaleLocalTo(Variables.AnimationCurve).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(Variables.AnimationCurve.Evaluate(0f), actualFrom);
                    Assert.AreEqual(Variables.AnimationCurve.Evaluate(1f), GameObject.transform.localScale);
                })
                .Run();
        }

        #endregion

        #endregion
    }
}