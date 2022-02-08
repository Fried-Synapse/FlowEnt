using System.Collections;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions;
using FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class RigidbodyTests : AbstractEngineTests
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
                new Vector3(0, 0, 0),
                new Vector3(0, 2, 0),
                new Vector3(0, 3, 3),
                new Vector3(5, 4, 3),
                new Vector3(0, 8, 0),
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

        private Rigidbody rigidbody;

        private Rigidbody Rigidbody
        {
            get
            {
                if (rigidbody == null)
                {
                    rigidbody = GameObject.AddComponent<Rigidbody>();
                    rigidbody.isKinematic = true;
                }
                return rigidbody;
            }
        }

        #region Move

        #region Move

        [UnityTest]
        public IEnumerator Move()
        {
            Vector3 value = new Vector3(MoveValue, MoveValue, MoveValue);

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).Move(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, Rigidbody.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveX()
        {
            const float value = MoveValue;

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).MoveX(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(new Vector3(value, 0, 0), Rigidbody.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveY()
        {
            const float value = MoveValue;

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).MoveY(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(new Vector3(0, value, 0), Rigidbody.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveZ()
        {
            const float value = MoveValue;

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).MoveZ(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(new Vector3(0, 0, value), Rigidbody.position))
                .Run();
        }

        #endregion

        #region MoveTo 

        [UnityTest]
        public IEnumerator MoveTo()
        {
            Vector3 to = new Vector3(MoveToValue, MoveToValue, MoveToValue);

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).MoveTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Rigidbody.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveFromTo()
        {
            Vector3 from = new Vector3(MoveFromValue, MoveFromValue, MoveFromValue);
            Vector3 to = new Vector3(MoveToValue, MoveToValue, MoveToValue);
            Vector3? startingFrom = null;

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).MoveTo(from, to)
                                    .OnUpdated((_) => startingFrom ??= Rigidbody.position)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, Rigidbody.position);
                })
                .Run();
        }

        #endregion

        #region MoveTo AnimationCurve3d

        [UnityTest]
        public IEnumerator MoveToAnimationCurve3d()
        {
            Vector3? actualFrom = null;
            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime)
                    .OnUpdated((_) => actualFrom ??= Rigidbody.position)
                    .MoveTo(Variables.AnimationCurve).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(Variables.AnimationCurve.Evaluate(0f), actualFrom);
                    Assert.AreEqual(Variables.AnimationCurve.Evaluate(1f), Rigidbody.position);
                })
                .Run();
        }

        #endregion

        #region MoveTo Axis 

        [UnityTest]
        public IEnumerator MoveToX()
        {
            const float to = MoveToValue;

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).MoveXTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Rigidbody.position.x))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveFromToX()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).MoveXTo(from, to)
                                    .OnUpdated((_) => startingFrom ??= Rigidbody.position.x)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, Rigidbody.position.x);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToY()
        {
            const float to = MoveToValue;

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).MoveYTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Rigidbody.position.y))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveFromToY()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).MoveYTo(from, to)
                                    .OnUpdated((_) => startingFrom ??= Rigidbody.position.y)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, Rigidbody.position.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToZ()
        {
            const float to = MoveToValue;

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).MoveZTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Rigidbody.position.z))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveFromToZ()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).MoveZTo(from, to)
                                    .OnUpdated((_) => startingFrom ??= Rigidbody.position.z)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, Rigidbody.position.z);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToXY()
        {
            const float to = MoveToValue;

            yield return CreateTester()
                .Act(() => new Tween(TestTime).Apply(new MoveAxisMotion<Rigidbody>(Rigidbody, Axis.XY, to)).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(to, Rigidbody.position.x);
                    Assert.AreEqual(to, Rigidbody.position.y);
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
                .Act(() => new Tween(TestTime).For(Rigidbody).Apply(new MoveAxisMotion<Rigidbody>(Rigidbody, Axis.XY, from, to))
                                    .OnUpdated((_) =>
                                    {
                                        startingFromX ??= Rigidbody.position.x;
                                        startingFromY ??= Rigidbody.position.y;
                                    })
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFromX);
                    Assert.AreEqual(from, startingFromY);
                    Assert.AreEqual(to, Rigidbody.position.x);
                    Assert.AreEqual(to, Rigidbody.position.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToXY_Separated()
        {
            const float to = MoveToValue;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .Queue(Rigidbody.Tween(TestTime).MoveXTo(to))
                                .At(0, Rigidbody.Tween(TestTime).MoveYTo(to))
                                .Start();
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(to, Rigidbody.position.x);
                    Assert.AreEqual(to, Rigidbody.position.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveFromToXY_Separated()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFromX = null;
            float? startingFromY = null;

            yield return CreateTester()
                .Act(() =>
                {
                    return new Flow()
                                .Queue(Rigidbody.Tween(TestTime).MoveXTo(from, to))
                                .At(0, Rigidbody.Tween(TestTime).MoveYTo(from, to))
                                .OnUpdated((_) =>
                                {
                                    startingFromX ??= Rigidbody.position.x;
                                    startingFromY ??= Rigidbody.position.y;
                                })
                                .Start();
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFromX);
                    Assert.AreEqual(from, startingFromY);
                    Assert.AreEqual(to, Rigidbody.position.x);
                    Assert.AreEqual(to, Rigidbody.position.y);
                })
                .Run();
        }

        #endregion

        #region MoveTo Spline

        [UnityTest]
        public IEnumerator MoveToSpline_Linear()
        {
            Vector3 to = new Vector3(MoveToSplineValue, MoveToSplineValue, MoveToSplineValue);

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).MoveTo(new LinearSpline(GetSpline(to))).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, Rigidbody.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToSpline_Bezier()
        {
            Vector3 to = new Vector3(MoveToSplineValue, MoveToSplineValue, MoveToSplineValue);

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).MoveTo(new BSpline(GetSpline(to))).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, Rigidbody.position))
                .Run();
        }

        #endregion

        #endregion
    }
}
