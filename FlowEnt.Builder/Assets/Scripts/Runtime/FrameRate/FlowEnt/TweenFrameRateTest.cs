using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt.Builder
{
    public class TweenFrameRateTest : AbstractFrameRateTest
    {
        public TweenFrameRateTest(FrameRateTestController controller, float testTime, int testAmount) : base(controller, testTime, testAmount)
        {
        }

        public override string TestName => "FlowEnt tweens";

        protected override void Prepare()
        {
            for (int i = 0; i < internalTestAmount; i++)
            {
                new Tween(internalTestTime).Start();
            }
        }

        protected override async Task StartControlAsync()
            => await new Tween(internalTestTime).StartAsync();
    }
}