#if !FlowEnt_AutoCancel
using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    [Category("AutoCancel")]
    public class AutoCancelTests : AbstractEngineTests
    {
        private const int Timeout = (int)(DoubleTestTime * 1000);

        [UnityTest]
        [Timeout(Timeout)]
        public IEnumerator AutoCancelTween()
        {
            yield return AutoCancel(go => new Tween(DoubleTestTime).For(go.transform).Move(Vector3.one));
        }

        [UnityTest]
        [Timeout(Timeout)]
        public IEnumerator AutoCancelEcho()
        {
            yield return AutoCancel(go => new Echo().For(go.transform).Move(Vector3.one));
        }

        [UnityTest]
        [Timeout(Timeout)]
        public IEnumerator AutoCancelFlow()
        {
            yield return AutoCancel(go => new Flow().Queue(new Tween(DoubleTestTime).For(go.transform).Move(Vector3.one)));
        }

        private IEnumerator AutoCancel(Func<GameObject, AbstractAnimation> getAnimation)
        {
            GameObject gameObject = null;
            AbstractAnimation animation = null;

            yield return CreateTester()
                .Arrange(() =>
                {
                    gameObject = CreateObject();
                    animation = getAnimation(gameObject);
                })
                .Act(() =>
                {
                    animation.Start();
                    new Tween(HalfTestTime)
                        .OnCompleting(() => Object.DestroyImmediate(gameObject))
                        .Start();
                    return new Tween(TestTime)
                        .Start();
                })
                .AssertTime(TestTime)
                .Assert(LogAssert.NoUnexpectedReceived)
                .Run();
        }
    }
}
#endif