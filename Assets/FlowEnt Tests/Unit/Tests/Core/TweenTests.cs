using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FriedSynapse.FlowEnt.Motions.Tween;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using FriedSynapse.FlowEnt.Motions.Tween.Transforms;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class TweenTests : AbstractAnimationTests<Tween>
    {
        protected override Tween CreateAnimation(float testTime) => new(testTime);

        [UnityTest]
        public IEnumerator Builder()
        {
            Tween tween = default;

            yield return CreateTester()
                .Arrange(() => ((MoveVectorMotion.ValueBuilder)Variables.Tween.Motions.Items[0]).Item = GameObject.transform)
                .Act(() => { tween = Variables.Tween.Build(); })
                .Assert(() =>
                {
                    List<AbstractTweenMotion> motions = tween.GetFieldValue<IFastList>("motions").Cast<AbstractTweenMotion>().ToList();
                    motions.Should().HaveCount(2);
                    motions[0].Should().BeOfType<MoveVectorMotion>();
                    ((MoveVectorMotion)motions[0]).Item.Should().Be(GameObject.transform);
                    motions[1].Should().BeOfType<DebugMotion>();
                })
                .Run();
        }
    }
}