using System.Collections;
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
                .Act(() =>
                //HACK It think this returns something for some fucking reason
#pragma warning disable RCS1021 
                {
                    tween = Variables.Tween.Build();
                })
#pragma warning restore RCS1021
                .Assert(() =>
                {
                    ITweenMotion[] motions = tween.GetFieldValue<ITweenMotion[]>("motions");
                    Assert.AreEqual(motions.Length, 2);
                    Assert.IsTrue(motions[0] is MoveVectorMotion);
                    Assert.IsTrue(motions[1] is DebugMotion);
                })
                .Run();
        }
    }
}
