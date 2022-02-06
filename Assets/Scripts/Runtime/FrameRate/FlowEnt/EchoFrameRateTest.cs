using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt.Builder
{
    public class EchoFrameRateTest : AbstractFrameRateTest
    {
        public EchoFrameRateTest(FrameRateTestController controller, float testTime, int testAmount) : base(controller, testTime, testAmount)
        {
        }

        public override string TestName => "FlowEnt echoes";

        protected override void Prepare()
        {
            for (int i = 0; i < internalTestAmount; i++)
            {
                new Echo(internalTestTime).Start();
            }
        }

        protected override async Task StartControlAsync()
            => await new Echo(internalTestTime).StartAsync();
    }
}