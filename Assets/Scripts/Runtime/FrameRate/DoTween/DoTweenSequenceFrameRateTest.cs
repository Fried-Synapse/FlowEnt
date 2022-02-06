using System.Threading.Tasks;
using DG.Tweening;

namespace FriedSynapse.FlowEnt.Builder
{
    public class DoTweenSequenceFrameRateTest : AbstractFrameRateTest
    {
        public DoTweenSequenceFrameRateTest(FrameRateTestController controller, float testTime, int testAmount) : base(controller, testTime, testAmount)
        {
        }

        public override string TestName => "DOTween sequence";

        public override void Load()
        {
            DOTween.SetTweensCapacity(1, TestAmount + 10);
            base.Load();
        }

        protected override void Prepare()
        {
            for (int i = 0; i < internalTestAmount; i++)
            {
                DOTween.Sequence().Append(DOTween.To(() => 1f, x => _ = x, 100, internalTestTime));
            }
        }

        protected override async Task StartControlAsync()
            => await DOTween.To(() => 1f, x => _ = x, 100, internalTestTime).AsyncWaitForCompletion();
    }
}