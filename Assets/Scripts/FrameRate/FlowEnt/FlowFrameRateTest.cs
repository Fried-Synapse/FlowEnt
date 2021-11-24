using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt.Builder
{
    public class FlowFrameRateTest : AbstractFrameRateTest
    {
        public FlowFrameRateTest(FrameRateTestController controller, float testTime, int testAmount) : base(controller, testTime, testAmount)
        {
        }

        public override string TestName => "FlowEnt flows";

        protected override void Prepare()
        {
            for (int i = 0; i < internalTestAmount; i++)
            {
                new Flow().Queue(new Tween(internalTestTime)).Start();
            }
        }

        protected override async Task StartControlAsync()
            => await new Tween(internalTestTime).StartAsync();
    }
}