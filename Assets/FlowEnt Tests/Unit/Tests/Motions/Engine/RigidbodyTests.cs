using System.Collections;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions;
using FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class RigidbodyTests : AbstractEngineTests<Rigidbody>
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

        private Rigidbody component;

        protected override Rigidbody Component
        {
            get
            {
                if (component == null)
                {
                    component = GameObject.AddComponent<Rigidbody>();
                    component.isKinematic = true;
                }

                return component;
            }
        }

        private void ArrangeForVelocity()
        {
            Component.mass = 0;
            Component.isKinematic = false;
        }

        #region Move

        #region Move

        [UnityTest]
        public IEnumerator Move()
        {
            Vector3 value = new Vector3(MoveValue, MoveValue, MoveValue);

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).Move(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, Component.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveTo()
        {
            Vector3 to = new Vector3(MoveToValue, MoveToValue, MoveToValue);

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Component.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveFromTo()
        {
            Vector3 from = new Vector3(MoveFromValue, MoveFromValue, MoveFromValue);
            Vector3 to = new Vector3(MoveToValue, MoveToValue, MoveToValue);
            Vector3? startingFrom = null;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveTo(from, to)
                    .OnUpdated((_) => startingFrom ??= Component.position)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, Component.position);
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
                .Act(() => Component.Tween(TestTime)
                    .OnUpdated((_) => actualFrom ??= Component.position)
                    .MoveTo(Variables.AnimationCurve).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(Variables.AnimationCurve.Evaluate(0f), actualFrom);
                    Assert.AreEqual(Variables.AnimationCurve.Evaluate(1f), Component.position);
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
                .Act(() => Component.Tween(TestTime).MoveX(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(new Vector3(value, 0, 0), Component.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToX()
        {
            const float to = MoveToValue;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveXTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Component.position.x))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveFromToX()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveXTo(from, to)
                    .OnUpdated((_) => startingFrom ??= Component.position.x)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, Component.position.x);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveY()
        {
            const float value = MoveValue;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveY(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(new Vector3(0, value, 0), Component.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToY()
        {
            const float to = MoveToValue;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveYTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Component.position.y))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveFromToY()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveYTo(from, to)
                    .OnUpdated((_) => startingFrom ??= Component.position.y)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, Component.position.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveZ()
        {
            const float value = MoveValue;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveZ(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(new Vector3(0, 0, value), Component.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToZ()
        {
            const float to = MoveToValue;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveZTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Component.position.z))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveFromToZ()
        {
            const float from = MoveFromValue;
            const float to = MoveToValue;
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveZTo(from, to)
                    .OnUpdated((_) => startingFrom ??= Component.position.z)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    Assert.AreEqual(to, Component.position.z);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToXY()
        {
            const float to = MoveToValue;

            yield return CreateTester()
                .Act(() => new Tween(TestTime).Apply(new MoveAxisMotion(Component, Axis.XY, to)).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(to, Component.position.x);
                    Assert.AreEqual(to, Component.position.y);
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
                .Act(() => new Tween(TestTime).For(Component).Apply(new MoveAxisMotion(Component, Axis.XY, from, to))
                    .OnUpdated((_) =>
                    {
                        startingFromX ??= Component.position.x;
                        startingFromY ??= Component.position.y;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFromX);
                    Assert.AreEqual(from, startingFromY);
                    Assert.AreEqual(to, Component.position.x);
                    Assert.AreEqual(to, Component.position.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveXY_Parallel()
        {
            const float from = MoveFromValue;
            const float value = MoveValue;
            const float expected = from + value;

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = new Vector3(from, from, 0))
                .Act(() => new Flow()
                    .Queue(Component.Tween(TestTime).MoveX(value))
                    .At(0, Component.Tween(TestTime).MoveY(value))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expected, Component.position.x);
                    Assert.AreEqual(expected, Component.position.y);
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
                .Arrange(() => GameObject.transform.position = new Vector3(from, from * 2, 0))
                .Act(() => new Flow()
                    .Queue(Component.Tween(HalfTestTime).Move(Axis.XY, value))
                    .Queue(Component.Tween(HalfTestTime).MoveY(value))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expectedX, Component.position.x);
                    Assert.AreEqual(expectedY, Component.position.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToXY_Parallel()
        {
            const float to = MoveToValue;

            yield return CreateTester()
                .Act(() => new Flow()
                    .Queue(Component.Tween(TestTime).MoveXTo(to))
                    .At(0, Component.Tween(TestTime).MoveYTo(to))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(to, Component.position.x);
                    Assert.AreEqual(to, Component.position.y);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToXY_Sequence()
        {
            const float to = MoveToValue;

            yield return CreateTester()
                .Act(() => new Flow()
                    .Queue(Component.Tween(HalfTestTime).MoveTo(Axis.XY, to))
                    .Queue(Component.Tween(HalfTestTime).MoveYTo(to))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(to, Component.position.x);
                    Assert.AreEqual(to, Component.position.y);
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
                    .Queue(Component.Tween(TestTime).MoveXTo(from, to))
                    .At(0, Component.Tween(TestTime).MoveYTo(from, to))
                    .OnUpdated((_) =>
                    {
                        startingFromX ??= Component.position.x;
                        startingFromY ??= Component.position.y;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFromX);
                    Assert.AreEqual(from, startingFromY);
                    Assert.AreEqual(to, Component.position.x);
                    Assert.AreEqual(to, Component.position.y);
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
                    .Queue(Component.Tween(HalfTestTime).MoveTo(Axis.XY, from, to))
                    .Queue(Component.Tween(HalfTestTime).MoveYTo(from, to))
                    .OnUpdated((_) =>
                    {
                        startingFromX ??= Component.position.x;
                        startingFromY ??= Component.position.y;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFromX);
                    Assert.AreEqual(from, startingFromY);
                    Assert.AreEqual(to, Component.position.x);
                    Assert.AreEqual(to, Component.position.y);
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
                .Act(() => Component.Tween(TestTime).MoveTo(new LinearSpline(GetSpline(to))).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, Component.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToSpline_Bezier()
        {
            Vector3 to = new Vector3(MoveToSplineValue, MoveToSplineValue, MoveToSplineValue);

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveTo(new BSpline(GetSpline(to))).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, Component.position))
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
                .Arrange(() => Component.rotation = from)
                .Act(() => Component.Tween(TestTime).Rotate(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, Component.rotation))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateToQuaternion()
        {
            Quaternion to = Quaternion.Euler(new Vector3(RotateXValue, RotateYValue, RotateZValue));

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).RotateTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, Component.rotation))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateFromToQuaternion()
        {
            Quaternion from =
                Quaternion.Euler(new Vector3(RotateXValue, FullCircle - RotateYValue, FullCircle - RotateZValue));
            Quaternion to = Quaternion.Euler(new Vector3(RotateXValue, RotateYValue, RotateZValue));
            Quaternion? startingFrom = null;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).RotateTo(from, to)
                    .OnUpdated((_) => startingFrom ??= Component.rotation)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    FlowEntAssert.AreEqual(from, startingFrom.Value);
                    FlowEntAssert.AreEqual(to, Component.rotation);
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
                .Arrange(() => Component.rotation = Quaternion.Euler(from))
                .Act(() => Component.Tween(TestTime).Rotate(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, Component.rotation.eulerAngles))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateToVector()
        {
            Vector3 to = new Vector3(RotateXValue, RotateYValue, RotateZValue);

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).RotateTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, Component.rotation.eulerAngles))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateFromToVector()
        {
            Vector3 from = new Vector3(RotateXValue, FullCircle - RotateYValue, FullCircle - RotateZValue);
            Vector3 to = new Vector3(RotateXValue, RotateYValue, RotateZValue);
            Vector3? startingFrom = null;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).RotateTo(from, to)
                    .OnUpdated((_) => startingFrom ??= Component.rotation.eulerAngles)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    FlowEntAssert.AreEqual(from, startingFrom.Value);
                    FlowEntAssert.AreEqual(to, Component.rotation.eulerAngles);
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
                .Arrange(() => initialMass = Component.mass)
                .Act(() => Component.Tween(TestTime).Mass(MoveValue).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(MoveValue, Component.mass - initialMass))
                .Run();
        }

        [UnityTest]
        public IEnumerator MassTo()
        {
            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MassTo(MoveToValue).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(MoveToValue, Component.mass))
                .Run();
        }

        [UnityTest]
        public IEnumerator MassFromTo()
        {
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MassTo(MoveFromValue, MoveToValue)
                    .OnUpdated((_) => startingFrom ??= Component.mass)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(MoveFromValue, startingFrom);
                    Assert.AreEqual(MoveToValue, Component.mass);
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
                .Act(() => Component.Tween(TestTime).Velocity(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(value, Component.velocity, VelocityErrorMargin))
                .Run();
        }

        [UnityTest]
        public IEnumerator VelocityTo()
        {
            Vector3 to = new Vector3(MoveToValue, MoveToValue, MoveToValue);

            yield return CreateTester()
                .Arrange(ArrangeForVelocity)
                .Act(() => Component.Tween(TestTime).VelocityTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, Component.velocity, VelocityErrorMargin))
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
                .Act(() => Component.Tween(TestTime).VelocityTo(from, to)
                    .OnUpdated((_) => startingFrom ??= Component.velocity)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    FlowEntAssert.AreEqual(to, Component.velocity, VelocityErrorMargin);
                })
                .Run();
        }

        #endregion

        #region AngularVelocity

        [UnityTest]
        public IEnumerator AngularVelocity()
        {
            Vector3 value = new Vector3(MoveValue, MoveValue, MoveValue);

            yield return CreateTester()
                .Arrange(ArrangeForVelocity)
                .Act(() => Component.Tween(TestTime).AngularVelocity(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(value, Component.angularVelocity, VelocityErrorMargin))
                .Run();
        }

        [UnityTest]
        public IEnumerator AngularVelocityTo()
        {
            Vector3 to = new Vector3(MoveToValue, MoveToValue, MoveToValue);

            yield return CreateTester()
                .Arrange(ArrangeForVelocity)
                .Act(() => Component.Tween(TestTime).AngularVelocityTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(to, Component.angularVelocity, VelocityErrorMargin))
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
                .Act(() => Component.Tween(TestTime).AngularVelocityTo(from, to)
                    .OnUpdated((_) => startingFrom ??= Component.angularVelocity)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingFrom);
                    FlowEntAssert.AreEqual(to, Component.angularVelocity, VelocityErrorMargin);
                })
                .Run();
        }

        #endregion
    }
}