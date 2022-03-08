using System.Collections;
using FriedSynapse.FlowEnt.Motions.Echo;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using FriedSynapse.FlowEnt.Motions.Echo.Transforms;
using FriedSynapse.FlowEnt.Reflection;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class EchoTests : AbstractAnimationTests<Echo>
    {
        protected override Echo CreateAnimation(float testTime)
            => new Echo(testTime);

        [UnityTest]
        public IEnumerator Builder()
        {
            Echo echo = default;
            yield return CreateTester()
                .Act(() =>
                //HACK It think this returns something for some fucking reason
#pragma warning disable RCS1021 
                {
                    echo = Variables.Echo.Build();
                })
#pragma warning restore RCS1021
                .Assert(() =>
                {
                    IEchoMotion[] motions = echo.GetFieldValue<IEchoMotion[]>("motions");
                    Assert.AreEqual(2, motions.Length);
                    Assert.IsTrue(motions[0] is MoveVectorMotion);
                    Assert.IsTrue(motions[1] is DebugMotion);
                })
                .Run();
        }
    }
}
