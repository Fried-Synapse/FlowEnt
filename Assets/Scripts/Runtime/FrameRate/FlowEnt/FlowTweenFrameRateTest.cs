using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt.Builder
{
    public class FlowTweenFrameRateTest : AbstractFrameRateTest
    {
        public FlowTweenFrameRateTest(FrameRateTestController controller, float testTime, int testAmount) : base(controller, testTime, testAmount)
        {
        }

        public override string TestName => "FlowEnt flow tweens";

        protected override void Prepare()
        {
            Flow flow = new Flow();
            for (int i = 0; i < internalTestAmount; i++)
            {
                flow.At(0, new Tween(internalTestTime));
            }
            flow.Start();
        }

        protected override async Task StartControlAsync()
            => await new Tween(internalTestTime).StartAsync();
    }
}
