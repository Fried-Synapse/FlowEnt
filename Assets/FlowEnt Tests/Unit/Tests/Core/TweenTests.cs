using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FriedSynapse.FlowEnt.Motions.Tween;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using FriedSynapse.FlowEnt.Motions.Tween.Transforms;
using FriedSynapse.FlowEnt.Reflection;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class TweenTests : AbstractAnimationTests<Tween>
    {
        protected override Tween CreateAnimation(float testTime)
            => new Tween(testTime);

        [UnityTest]
        public IEnumerator Builder()
        {
            Tween tween = default;

            yield return CreateTester()
                .Act(() => { tween = Variables.Tween.Build(); })
                .Assert(() =>
                {
                    List<AbstractTweenMotion> motions = tween .GetFieldValue<IList>("motions").Cast<AbstractTweenMotion>().ToList();
                    Assert.AreEqual(2, motions.Count);
                    Assert.IsTrue(motions[0] is MoveVectorMotion);
                    Assert.IsTrue(motions[1] is DebugMotion);
                })
                .Run();
        }
    }
}