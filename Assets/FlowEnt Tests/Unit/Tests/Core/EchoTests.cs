using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                {
                    echo = Variables.Echo.Build();
                })
                .Assert(() =>
                {
                    List<AbstractEchoMotion> motions = echo.GetFieldValue<IList>("motions").Cast<AbstractEchoMotion>().ToList();
                    Assert.AreEqual(2, motions.Count);
                    Assert.IsTrue(motions[0] is MoveVectorMotion);
                    Assert.IsTrue(motions[1] is DebugMotion);
                })
                .Run();
        }
    }
}
