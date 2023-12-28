using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies;
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

        private static List<Vector3> GetSpline(Vector3 to) =>
            new()
            {
                new Vector3(0, 0, 0),
                new Vector3(0, 2, 0),
                new Vector3(0, 3, 3),
                new Vector3(5, 4, 3),
                new Vector3(0, 8, 0),
                to
            };

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
            Vector3 value = new(MoveValue, MoveValue, MoveValue);

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).Move(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.position.Should().Be(value))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveTo()
        {
            Vector3 to = new(MoveToValue, MoveToValue, MoveToValue);

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.position.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveFromTo()
        {
            Vector3 from = new(MoveFromValue, MoveFromValue, MoveFromValue);
            Vector3 to = new(MoveToValue, MoveToValue, MoveToValue);
            Vector3? startingFrom = null;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveTo(from, to)
                    .OnUpdated(_ => startingFrom ??= Component.position)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFrom.Should().Be(from);
                    Component.position.Should().Be(to);
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
                    .OnUpdated(_ => actualFrom ??= Component.position)
                    .MoveTo(Variables.AnimationCurve).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    actualFrom.Should().Be(Variables.AnimationCurve.Evaluate(0f));
                    Component.position.Should().Be(Variables.AnimationCurve.Evaluate(1f));
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
                .Assert(() => Component.position.Should().Be(new Vector3(value, 0, 0)))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToX()
        {
            const float to = MoveToValue;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveXTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.position.x.Should().Be(to))
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
                    .OnUpdated(_ => startingFrom ??= Component.position.x)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFrom.Should().Be(from);
                    Component.position.x.Should().Be(to);
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
                .Assert(() => Component.position.Should().Be(new Vector3(0, value, 0)))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToY()
        {
            const float to = MoveToValue;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveYTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.position.y.Should().Be(to))
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
                    .OnUpdated(_ => startingFrom ??= Component.position.y)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFrom.Should().Be(from);
                    Component.position.y.Should().Be(to);
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
                .Assert(() => Component.position.Should().Be(new Vector3(0, 0, value)))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToZ()
        {
            const float to = MoveToValue;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveZTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.position.z.Should().Be(to))
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
                    .OnUpdated(_ => startingFrom ??= Component.position.z)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFrom.Should().Be(from);
                    Component.position.z.Should().Be(to);
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
                    Component.position.x.Should().Be(to);
                    Component.position.y.Should().Be(to);
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
                    .OnUpdated(_ =>
                    {
                        Vector3 position = Component.position;
                        startingFromX ??= position.x;
                        startingFromY ??= position.y;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFromX.Should().Be(from);
                    startingFromY.Should().Be(from);
                    Component.position.x.Should().Be(to);
                    Component.position.y.Should().Be(to);
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
                    Component.position.x.Should().Be(expected);
                    Component.position.y.Should().Be(expected);
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
                    Component.position.x.Should().Be(expectedX);
                    Component.position.y.Should().Be(expectedY);
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
                    Component.position.x.Should().Be(to);
                    Component.position.y.Should().Be(to);
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
                    Component.position.x.Should().Be(to);
                    Component.position.y.Should().Be(to);
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
                    .OnUpdated(_ =>
                    {
                        Vector3 position = Component.position;
                        startingFromX ??= position.x;
                        startingFromY ??= position.y;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFromX.Should().Be(from);
                    startingFromY.Should().Be(from);
                    Component.position.x.Should().Be(to);
                    Component.position.y.Should().Be(to);
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
                    .OnUpdated(_ =>
                    {
                        Vector3 position = Component.position;
                        startingFromX ??= position.x;
                        startingFromY ??= position.y;
                    })
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFromX.Should().Be(from);
                    startingFromY.Should().Be(from);
                    Component.position.x.Should().Be(to);
                    Component.position.y.Should().Be(to);
                })
                .Run();
        }

        #endregion

        #region Move Spline

        [UnityTest]
        public IEnumerator MoveToSpline_Linear()
        {
            Vector3 to = new(MoveToSplineValue, MoveToSplineValue, MoveToSplineValue);

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveTo(new LinearSpline(GetSpline(to))).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.position.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToSpline_Bezier()
        {
            Vector3 to = new(MoveToSplineValue, MoveToSplineValue, MoveToSplineValue);

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MoveTo(new BSpline(GetSpline(to))).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.position.Should().Be(to))
                .Run();
        }

        #endregion

        #endregion

        #region Rotate

        #region Rotate Quaternion

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
                .Assert(() => Component.rotation.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateToQuaternion()
        {
            Quaternion to = Quaternion.Euler(new Vector3(RotateXValue, RotateYValue, RotateZValue));

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).RotateTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.rotation.Should().Be(to))
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
                    .OnUpdated(_ => startingFrom ??= Component.rotation)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFrom.Value.Should().Be(from);
                    Component.rotation.Should().Be(to);
                })
                .Run();
        }

        #endregion

        #region Rotate Vector

        [UnityTest]
        public IEnumerator RotateVector()
        {
            Vector3 from = new(-RotateXValue, 0f, 0f);
            Vector3 value = new(RotateXValue, RotateYValue, 0f);
            Vector3 to = from + value;

            yield return CreateTester()
                .Arrange(() => Component.rotation = Quaternion.Euler(from))
                .Act(() => Component.Tween(TestTime).Rotate(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.rotation.eulerAngles.Should().BeApproximately(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateToVector()
        {
            Vector3 to = new(RotateXValue, RotateYValue, RotateZValue);

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).RotateTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.rotation.eulerAngles.Should().Be(to))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateFromToVector()
        {
            Vector3 from = new(RotateXValue, FullCircle - RotateYValue, FullCircle - RotateZValue);
            Vector3 to = new(RotateXValue, RotateYValue, RotateZValue);
            Vector3? startingFrom = null;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).RotateTo(from, to)
                    .OnUpdated(_ => startingFrom ??= Component.rotation.eulerAngles)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFrom.Value.Should().Be(from);
                    Component.rotation.eulerAngles.Should().Be(to);
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
                .Assert(() => Component.mass.Should().Be(initialMass + MoveValue))
                .Run();
        }

        [UnityTest]
        public IEnumerator MassTo()
        {
            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MassTo(MoveToValue).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.mass.Should().Be(MoveToValue))
                .Run();
        }

        [UnityTest]
        public IEnumerator MassFromTo()
        {
            float? startingFrom = null;

            yield return CreateTester()
                .Act(() => Component.Tween(TestTime).MassTo(MoveFromValue, MoveToValue)
                    .OnUpdated(_ => startingFrom ??= Component.mass)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFrom.Should().Be(MoveFromValue);
                    Component.mass.Should().Be(MoveToValue);
                })
                .Run();
        }

        #endregion

        #region Velocity

        [UnityTest]
        public IEnumerator Velocity()
        {
            Vector3 value = new(MoveValue, MoveValue, MoveValue);

            yield return CreateTester()
                .Arrange(ArrangeForVelocity)
                .Act(() => Component.Tween(TestTime).Velocity(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.velocity.Should().BeApproximately(value, VelocityErrorMargin))
                .Run();
        }

        [UnityTest]
        public IEnumerator VelocityTo()
        {
            Vector3 to = new(MoveToValue, MoveToValue, MoveToValue);

            yield return CreateTester()
                .Arrange(ArrangeForVelocity)
                .Act(() => Component.Tween(TestTime).VelocityTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.velocity.Should().BeApproximately(to, VelocityErrorMargin))
                .Run();
        }

        [UnityTest]
        public IEnumerator VelocityFromTo()
        {
            Vector3 from = new(MoveFromValue, MoveFromValue, MoveFromValue);
            Vector3 to = new(MoveToValue, MoveToValue, MoveToValue);
            Vector3? startingFrom = null;

            yield return CreateTester()
                .Arrange(ArrangeForVelocity)
                .Act(() => Component.Tween(TestTime).VelocityTo(from, to)
                    .OnUpdated(_ => startingFrom ??= Component.velocity)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFrom.Should().Be(from);
                    Component.velocity.Should().BeApproximately(to, VelocityErrorMargin);
                })
                .Run();
        }

        #endregion

        #region AngularVelocity

        [UnityTest]
        public IEnumerator AngularVelocity()
        {
            Vector3 value = new(MoveValue, MoveValue, MoveValue);

            yield return CreateTester()
                .Arrange(ArrangeForVelocity)
                .Act(() => Component.Tween(TestTime).AngularVelocity(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.angularVelocity.Should().BeApproximately(value, VelocityErrorMargin))
                .Run();
        }

        [UnityTest]
        public IEnumerator AngularVelocityTo()
        {
            Vector3 to = new(MoveToValue, MoveToValue, MoveToValue);

            yield return CreateTester()
                .Arrange(ArrangeForVelocity)
                .Act(() => Component.Tween(TestTime).AngularVelocityTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Component.angularVelocity.Should().BeApproximately(to, VelocityErrorMargin))
                .Run();
        }

        [UnityTest]
        public IEnumerator AngularVelocityFromTo()
        {
            Vector3 from = new(MoveFromValue, MoveFromValue, MoveFromValue);
            Vector3 to = new(MoveToValue, MoveToValue, MoveToValue);
            Vector3? startingFrom = null;

            yield return CreateTester()
                .Arrange(ArrangeForVelocity)
                .Act(() => Component.Tween(TestTime).AngularVelocityTo(from, to)
                    .OnUpdated(_ => startingFrom ??= Component.angularVelocity)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    startingFrom.Should().Be(from);
                    Component.angularVelocity.Should().BeApproximately(to, VelocityErrorMargin);
                })
                .Run();
        }

        #endregion
    }
}