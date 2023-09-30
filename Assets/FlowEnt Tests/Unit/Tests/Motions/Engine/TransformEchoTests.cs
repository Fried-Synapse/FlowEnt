using System.Collections;
using FluentAssertions;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class TransformEchoTests : AbstractEngineTests
    {
        #region Constants

        private const float MoveValue = 3f;
        private const float RotateValue = 45f;
        private const float ScaleValue = 3f;
        private const float Speed = 1000f;

        #endregion

        #region Move

        [UnityTest]
        public IEnumerator Move()
        {
            Vector3 target = new Vector3(MoveValue, MoveValue, MoveValue);

            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).Move(target).Start())
                .AssertTime(TestTime)
                .Assert<Echo>(echo =>
                    GameObject.transform.position.Should().Be(target * (TestTime + echo.Overdraft.Value)))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveX()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).MoveX(MoveValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>(echo =>
                    GameObject.transform.position.x.Should().Be(MoveValue * (TestTime + echo.Overdraft.Value)))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveY()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).MoveY(MoveValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>(echo =>
                    GameObject.transform.position.y.Should().Be(MoveValue * (TestTime + echo.Overdraft.Value)))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveZ()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).MoveZ(MoveValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>(echo =>
                    GameObject.transform.position.z.Should().Be(MoveValue * (TestTime + echo.Overdraft.Value)))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToVector()
        {
            Vector3 target = new Vector3(MoveValue, MoveValue, MoveValue);

            yield return CreateTester()
                .Act(() => GameObject.transform.Echo(TestTime).MoveTo(target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() => GameObject.transform.position.Should().Be(target))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToTransform()
        {
            yield return CreateTester()
                .Act(() => GameObject.transform.Echo(TestTime).MoveTo(Variables.Target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() => GameObject.transform.position.Should().Be(Variables.Target.position))
                .Run();
        }

        #endregion

        #region Rotate

        [UnityTest]
        public IEnumerator RotateQuaternion()
        {
            Quaternion target = Quaternion.Euler(RotateValue, RotateValue, RotateValue);

            yield return CreateTester()
                .Arrange(() => GameObject.transform.eulerAngles = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).Rotate(target).Start())
                .AssertTime(TestTime)
                .Assert<Echo>(echo => GameObject.transform.eulerAngles.Should()
                    .BeApproximately(target.eulerAngles * (TestTime + echo.Overdraft.Value), 5f))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateVector()
        {
            Vector3 target = new Vector3(RotateValue, RotateValue, RotateValue);

            yield return CreateTester()
                .Arrange(() => GameObject.transform.eulerAngles = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).Rotate(target).Start())
                .AssertTime(TestTime)
                .Assert<Echo>(echo => GameObject.transform.eulerAngles.Should()
                    .BeApproximately(target * (TestTime + echo.Overdraft.Value), 5f))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateX()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.eulerAngles = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).RotateX(RotateValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>(echo =>
                    GameObject.transform.eulerAngles.x.Should().Be(RotateValue * (TestTime + echo.Overdraft.Value)))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateY()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.eulerAngles = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).RotateY(RotateValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>(echo =>
                    GameObject.transform.eulerAngles.y.Should().Be(RotateValue * (TestTime + echo.Overdraft.Value)))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateZ()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.eulerAngles = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).RotateZ(RotateValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>(echo =>
                    GameObject.transform.eulerAngles.z.Should().Be(RotateValue * (TestTime + echo.Overdraft.Value)))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateToQuaternion()
        {
            Quaternion target = Quaternion.Euler(RotateValue, RotateValue, RotateValue);

            yield return CreateTester()
                .Act(() => GameObject.transform.Echo(TestTime).RotateTo(target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() => GameObject.transform.rotation.Should().Be(target))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateToVector()
        {
            Vector3 target = new Vector3(RotateValue, RotateValue, RotateValue);

            yield return CreateTester()
                .Act(() => GameObject.transform.Echo(TestTime).RotateTo(target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() => GameObject.transform.eulerAngles.Should().Be(target))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateToTransform()
        {
            yield return CreateTester()
                .Act(() => GameObject.transform.Echo(TestTime).RotateTo(Variables.Target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() => GameObject.transform.eulerAngles.Should().Be(Variables.Target.eulerAngles))
                .Run();
        }

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
                .Act(() => GameObject.transform.Echo(TestTime).RotateAround(Vector3.zero, Vector3.up, 90f / TestTime)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() => GameObject.transform.position.Should().BeApproximately(Vector3.forward, 0.1f))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateAroundVector()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = Vector3.left)
                .Act(() => GameObject.transform.Echo(TestTime).RotateAround(Vector3.zero, Vector3.up, 90f / TestTime)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() => GameObject.transform.position.Should().BeApproximately(Vector3.forward, 0.1f))
                .Run();
        }

        #endregion

        #region Scale

        [UnityTest]
        public IEnumerator Scale()
        {
            Vector3 target = new Vector3(ScaleValue, ScaleValue, ScaleValue);

            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).Scale(target).Start())
                .AssertTime(TestTime)
                .Assert<Echo>(echo =>
                    GameObject.transform.localScale.Should().Be(target * (TestTime + echo.Overdraft.Value)))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleX()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).ScaleX(ScaleValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>(echo =>
                    GameObject.transform.localScale.x.Should().Be(ScaleValue * (TestTime + echo.Overdraft.Value)))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleY()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).ScaleY(ScaleValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>(echo =>
                    GameObject.transform.localScale.y.Should().Be(ScaleValue * (TestTime + echo.Overdraft.Value)))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleZ()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).ScaleZ(ScaleValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>(echo =>
                    GameObject.transform.localScale.z.Should().Be(ScaleValue * (TestTime + echo.Overdraft.Value)))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleToVector()
        {
            Vector3 target = new Vector3(ScaleValue, ScaleValue, ScaleValue);

            yield return CreateTester()
                .Act(() => GameObject.transform.Echo(TestTime).ScaleTo(target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() => GameObject.transform.localScale.Should().BeApproximately(target))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleToTransform()
        {
            yield return CreateTester()
                .Act(() => GameObject.transform.Echo(TestTime).ScaleTo(Variables.Target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() => GameObject.transform.localScale.Should().BeApproximately(Variables.Target.localScale))
                .Run();
        }

        #endregion
    }
}