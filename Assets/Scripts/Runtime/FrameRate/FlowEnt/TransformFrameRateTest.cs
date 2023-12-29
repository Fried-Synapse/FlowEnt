using System.Threading.Tasks;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Builder
{
    public class TransformFrameRateTest : AbstractObjectFrameRateTest
    {
        public TransformFrameRateTest(FrameRateTestController controller, float testTime, int testAmount) : base(controller, testTime, testAmount)
        {
        }

        public override string TestName => "FlowEnt transform move";

        protected override void Prepare()
        {
            Vector3 value = Vector3.right;
            for (int i = 0; i < internalTestAmount; i++)
            {
                new Tween(internalTestTime).For(Transforms[i]).Move(value).Start();
            }
        }

        protected override async Task StartControlAsync()
            => await new Tween(internalTestTime).StartAsync();
    }
}