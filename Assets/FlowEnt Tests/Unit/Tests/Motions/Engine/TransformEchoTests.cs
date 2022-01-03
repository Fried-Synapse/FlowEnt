using System.Collections;
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
                .Assert<Echo>((echo) => FlowEntAssert.AreEqual(target * (TestTime + echo.OverDraft.Value), GameObject.transform.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveX()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).MoveX(MoveValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>((echo) => FlowEntAssert.AreEqual(MoveValue * (TestTime + echo.OverDraft.Value), GameObject.transform.position.x))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveY()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).MoveY(MoveValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>((echo) => FlowEntAssert.AreEqual(MoveValue * (TestTime + echo.OverDraft.Value), GameObject.transform.position.y))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveZ()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.position = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).MoveZ(MoveValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>((echo) => FlowEntAssert.AreEqual(MoveValue * (TestTime + echo.OverDraft.Value), GameObject.transform.position.z))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToVector()
        {
            Vector3 target = new Vector3(MoveValue, MoveValue, MoveValue);

            yield return CreateTester()
                .Act(() => GameObject.transform.Echo(TestTime).MoveTo(target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(target, GameObject.transform.position))
                .Run();
        }

        [UnityTest]
        public IEnumerator MoveToTransform()
        {
            yield return CreateTester()
                .Act(() => GameObject.transform.Echo(TestTime).MoveTo(Variables.Target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(Variables.Target.position, GameObject.transform.position))
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
                .Assert<Echo>((echo) => FlowEntAssert.AreEqual(target.eulerAngles * (TestTime + echo.OverDraft.Value), GameObject.transform.eulerAngles))
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
                .Assert<Echo>((echo) => FlowEntAssert.AreEqual(target * (TestTime + echo.OverDraft.Value), GameObject.transform.eulerAngles))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateX()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.eulerAngles = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).RotateX(RotateValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>((echo) => FlowEntAssert.AreEqual(RotateValue * (TestTime + echo.OverDraft.Value), GameObject.transform.eulerAngles.x))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateY()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.eulerAngles = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).RotateY(RotateValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>((echo) => FlowEntAssert.AreEqual(RotateValue * (TestTime + echo.OverDraft.Value), GameObject.transform.eulerAngles.y))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateZ()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.eulerAngles = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).RotateZ(RotateValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>((echo) => FlowEntAssert.AreEqual(RotateValue * (TestTime + echo.OverDraft.Value), GameObject.transform.eulerAngles.z))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateToQuaternion()
        {
            Quaternion target = Quaternion.Euler(RotateValue, RotateValue, RotateValue);

            yield return CreateTester()
                .Act(() => GameObject.transform.Echo(TestTime).RotateTo(target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(target, GameObject.transform.rotation))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateToVector()
        {
            Vector3 target = new Vector3(RotateValue, RotateValue, RotateValue);

            yield return CreateTester()
                .Act(() => GameObject.transform.Echo(TestTime).RotateTo(target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(target, GameObject.transform.eulerAngles))
                .Run();
        }

        [UnityTest]
        public IEnumerator RotateToTransform()
        {
            yield return CreateTester()
                .Act(() => GameObject.transform.Echo(TestTime).RotateTo(Variables.Target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(Variables.Target.eulerAngles, GameObject.transform.eulerAngles))
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
                .Assert<Echo>((echo) => FlowEntAssert.AreEqual(target * (TestTime + echo.OverDraft.Value), GameObject.transform.localScale))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleX()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).ScaleX(ScaleValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>((echo) => FlowEntAssert.AreEqual(ScaleValue * (TestTime + echo.OverDraft.Value), GameObject.transform.localScale.x))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleY()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).ScaleY(ScaleValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>((echo) => FlowEntAssert.AreEqual(ScaleValue * (TestTime + echo.OverDraft.Value), GameObject.transform.localScale.y))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleZ()
        {
            yield return CreateTester()
                .Arrange(() => GameObject.transform.localScale = Vector3.zero)
                .Act(() => GameObject.transform.Echo(TestTime).ScaleZ(ScaleValue).Start())
                .AssertTime(TestTime)
                .Assert<Echo>((echo) => FlowEntAssert.AreEqual(ScaleValue * (TestTime + echo.OverDraft.Value), GameObject.transform.localScale.z))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleToVector()
        {
            Vector3 target = new Vector3(ScaleValue, ScaleValue, ScaleValue);

            yield return CreateTester()
                .Act(() => GameObject.transform.Echo(TestTime).ScaleTo(target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(target, GameObject.transform.localScale))
                .Run();
        }

        [UnityTest]
        public IEnumerator ScaleToTransform()
        {
            yield return CreateTester()
                .Act(() => GameObject.transform.Echo(TestTime).ScaleTo(Variables.Target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.AreEqual(Variables.Target.localScale, GameObject.transform.localScale))
                .Run();
        }

        #endregion
    }
}