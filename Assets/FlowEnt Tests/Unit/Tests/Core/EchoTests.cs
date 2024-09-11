using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FriedSynapse.FlowEnt.Motions.Echo;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using FriedSynapse.FlowEnt.Motions.Echo.Transforms;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class EchoTests : AbstractAnimationTests<Echo>
    {
        protected override Echo CreateAnimation(float testTime) => new(testTime);

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
                    motions.Should().HaveCount(2);
                    motions[0].Should().BeOfType<MoveVectorMotion>();
                    motions[1].Should().BeOfType<DebugMotion>();
                })
                .Run();
        }
    }
}
