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

        private const float VelocityErrorMargin = 0.3f;

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

        private void ArrangeForVelocity()
        {
            Rigidbody.mass = 0;
            Rigidbody.isKinematic = false;
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

        #region Move AnimationCurve3d

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

        #region Move Axis 

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
        public IEnumerator MoveZ()
        {
            const float value = MoveValue;

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).MoveZ(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(new Vector3(0, 0, value), Rigidbody.position))
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
                .Act(() => new Tween(TestTime).Apply(new MoveAxisMotion(Rigidbody, Axis.XY, to)).Start())
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
                .Act(() => new Tween(TestTime).For(Rigidbody).Apply(new MoveAxisMotion(Rigidbody, Axis.XY, from, to))
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
                                    Debug.Log($"{Rigidbody.position.x}");
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

        #region Move Spline

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

        #region Rotate

        #region Rotate Vector

        [UnityTest]
        public IEnumerator RotateQuaternion()
        {
            Quaternion from = Quaternion.Euler(new Vector3(-RotateXValue, 0f, 0f));
            Quaternion value = Quaternion.Euler(new Vector3(RotateXValue, RotateYValue, 0f));
            Quaternion to = from * value;

            yield return CreateTester()
                .Arrange(() => Rigidbody.rotation = from)
                .Act(() => Rigidbody.Tween(TestTime).Rotate(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, Rigidbody.rotation))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateToQuaternion()
        {
            Quaternion to = Quaternion.Euler(new Vector3(RotateXValue, RotateYValue, RotateZValue));

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).RotateTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, Rigidbody.rotation))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateFromToQuaternion()
        {
            Quaternion from = Quaternion.Euler(new Vector3(RotateXValue, FullCircle - RotateYValue, FullCircle - RotateZValue));
            Quaternion to = Quaternion.Euler(new Vector3(RotateXValue, RotateYValue, RotateZValue));
            Quaternion? startingFrom = null;

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).RotateTo(from, to)
                                    .OnUpdated((_) => startingFrom ??= Rigidbody.rotation)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    FlowEntAssert.AreEqual(from, startingFrom.Value);
                    FlowEntAssert.AreEqual(to, Rigidbody.rotation);
                })
                .Run();
        }

        #endregion

        #region Rotate Quaternion

        [UnityTest]
        public IEnumerator RotateVector()
        {
            Vector3 from = new Vector3(-RotateXValue, 0f, 0f);
            Vector3 value = new Vector3(RotateXValue, RotateYValue, 0f);
            Vector3 to = from + value;

            yield return CreateTester()
                .Arrange(() => Rigidbody.rotation = Quaternion.Euler(from))
                .Act(() => Rigidbody.Tween(TestTime).Rotate(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, Rigidbody.rotation.eulerAngles))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateToVector()
        {
            Vector3 to = new Vector3(RotateXValue, RotateYValue, RotateZValue);

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).RotateTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, Rigidbody.rotation.eulerAngles))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateFromToVector()
        {
            Vector3 from = new Vector3(RotateXValue, FullCircle - RotateYValue, FullCircle - RotateZValue);
            Vector3 to = new Vector3(RotateXValue, RotateYValue, RotateZValue);
            Vector3? startingFrom = null;

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).RotateTo(from, to)
                                    .OnUpdated((_) => startingFrom ??= Rigidbody.rotation.eulerAngles)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    FlowEntAssert.AreEqual(from, startingFrom.Value);
                    FlowEntAssert.AreEqual(to, Rigidbody.rotation.eulerAngles);
                })
                .Run();
        }

        #endregion

        #endregion

        #region Mass

        [UnityTest]
        public IEnumerator Mass()
        {
            float initialMass = 0;
            yield return CreateTester()
                .Arrange(() => initialMass = Rigidbody.mass)
                .Act(() => Rigidbody.Tween(TestTime).Mass(MoveValue).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(MoveValue, Rigidbody.mass - initialMass))
                .Run();
        }

        [UnityTest]
        public IEnumerator MassTo()
        {
            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).MassTo(MoveToValue).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(MoveToValue, Rigidbody.mass))
                .Run();
        }

        [UnityTest]
        public IEnumerator MassFromTo()
        {
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => Rigidbody.Tween(TestTime).MassTo(MoveFromValue, MoveToValue)
                                    .OnUpdated((_) => startingFrom ??= Rigidbody.mass)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(MoveFromValue, startingFrom);
                    Assert.AreEqual(MoveToValue, Rigidbody.mass);
                })
                .Run();
        }

        #endregion

        #region Velocity

        [UnityTest]
        public IEnumerator Velocity()
        {
            Vector3 value = new Vector3(MoveValue, MoveValue, MoveValue);

            yield return CreateTester()
                .Arrange(ArrangeForVelocity)
                .Act(() => Rigidbody.Tween(TestTime).Velocity(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(value, Rigidbody.velocity, VelocityErrorMargin))
                .Run();
        }

        [UnityTest]
        public IEnumerator VelocityTo()
        {
            Vector3 to = new Vector3(MoveToValue, MoveToValue, MoveToValue);

            yield return CreateTester()
                .Arrange(ArrangeForVelocity)
                .Act(() => Rigidbody.Tween(TestTime).VelocityTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, Rigidbody.velocity, VelocityErrorMargin))
                .Run();
        }

        [UnityTest]
        public IEnumerator VelocityFromTo()
        {
            Vector3 from = new Vector3(MoveFromValue, MoveFromValue, MoveFromValue);
            Vector3 to = new Vector3(MoveToValue, MoveToValue, MoveToValue);
            Vector3? startingFrom = null;

            yield return CreateTester()
                .Arrange(ArrangeForVelocity)
                .Act(() => Rigidbody.Tween(TestTime).VelocityTo(from, to)
                                    .OnUpdated((_) => startingFrom ??= Rigidbody.velocity)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    FlowEntAssert.AreEqual(to, Rigidbody.velocity, VelocityErrorMargin);
                })
                .Run();
        }

        #endregion

        #region Move

        [UnityTest]
        public IEnumerator AngularVelocity()
        {
            Vector3 value = new Vector3(MoveValue, MoveValue, MoveValue);

            yield return CreateTester()
                .Arrange(ArrangeForVelocity)
                .Act(() => Rigidbody.Tween(TestTime).AngularVelocity(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(value, Rigidbody.angularVelocity, VelocityErrorMargin))
                .Run();
        }

        [UnityTest]
        public IEnumerator AngularVelocityTo()
        {
            Vector3 to = new Vector3(MoveToValue, MoveToValue, MoveToValue);

            yield return CreateTester()
                .Arrange(ArrangeForVelocity)
                .Act(() => Rigidbody.Tween(TestTime).AngularVelocityTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, Rigidbody.angularVelocity, VelocityErrorMargin))
                .Run();
        }

        [UnityTest]
        public IEnumerator AngularVelocityFromTo()
        {
            Vector3 from = new Vector3(MoveFromValue, MoveFromValue, MoveFromValue);
            Vector3 to = new Vector3(MoveToValue, MoveToValue, MoveToValue);
            Vector3? startingFrom = null;

            yield return CreateTester()
                .Arrange(ArrangeForVelocity)
                .Act(() => Rigidbody.Tween(TestTime).AngularVelocityTo(from, to)
                                    .OnUpdated((_) => startingFrom ??= Rigidbody.angularVelocity)
                                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    FlowEntAssert.AreEqual(to, Rigidbody.angularVelocity, VelocityErrorMargin);
                })
                .Run();
        }

        #endregion

    }
}
