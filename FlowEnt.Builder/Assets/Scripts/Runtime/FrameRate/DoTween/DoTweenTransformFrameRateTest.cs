using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Builder
{
    public class DoTweenTransformFrameRateTest : AbstractObjectFrameRateTest
    {
        public DoTweenTransformFrameRateTest(FrameRateTestController controller, float testTime, int testAmount) : base(controller, testTime, testAmount)
        {
        }

        public override string TestName => "DOTween transform move";

        private int x;

        public override void Load()
        {
            DOTween.SetTweensCapacity(TestAmount + 10, 1);
            base.Load();
        }

        protected override void Prepare()
        {
            x++;
            for (int i = 0; i < internalTestAmount; i++)
            {
                Transforms[i].DOMove(new Vector3(x, 0, i), internalTestTime);
            }
        }

        protected override async Task StartControlAsync()
            => await DOTween.To(() => 1f, x => _ = x, 100, internalTestTime).AsyncWaitForCompletion();
    }
}