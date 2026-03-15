using System.Threading.Tasks;
using DG.Tweening;

namespace FriedSynapse.FlowEnt.Builder
{
    public class DoTweenTweenFrameRateTest : AbstractFrameRateTest
    {
        public DoTweenTweenFrameRateTest(FrameRateTestController controller, float testTime, int testAmount) : base(controller, testTime, testAmount)
        {
        }

        public override string TestName => "DOTween tweens";

        public override void Load()
        {
            DOTween.SetTweensCapacity(TestAmount + 10, 1);
            base.Load();
        }

        protected override void Prepare()
        {
            for (int i = 0; i < internalTestAmount; i++)
            {
                DOTween.To(() => 1f, x => _ = x, 100, internalTestTime);
            }
        }

        protected override async Task StartControlAsync()
            => await DOTween.To(() => 1f, x => _ = x, 100, internalTestTime).AsyncWaitForCompletion();
    }
}