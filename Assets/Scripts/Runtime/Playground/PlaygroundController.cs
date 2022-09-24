using System.Threading;
using System.Threading.Tasks;
using FriedSynapse.Quickit;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Builder
{
    public class PlaygroundController : MonoBehaviour
    {
#pragma warning disable RCS1169, IDE0044
        [SerializeField]
        private Transform character;
        private Transform Character => character;
        [SerializeField]
        private Transform green;
        private Transform Green => green;
        [SerializeField]
        private Transform yellow;
        private Transform Yellow => yellow;
        [SerializeField]
        private Transform red;
        private Transform Red => red;
        [SerializeField]
        private TweenBuilder tween;
        private TweenBuilder Tween => tween;
        [SerializeField]
        private EchoBuilder echo;
        private EchoBuilder Echo => echo;
        [SerializeField]
        private FlowBuilder flow;
        private FlowBuilder Flow => flow;
#pragma warning restore RCS1169, IDE0044

#pragma warning disable IDE0051, RCS1213

        public AbstractAnimation TweenProp
            => new Tween(1)
                .SetName("tween name")
                .SetTimeScale(1)
                .SetEasing(Easing.EaseInOutCubic)
                .SetLoopCount(3)
                .SetLoopType(LoopType.PingPong)
                .For(Green)
                    .MoveLocalYTo(3.5f);

        public AbstractAnimation EchoProp
            => new Echo(1)
                .For(Green)
                    .MoveY(3.5f);

        public Flow FlowProp
            => new Flow()
                .Queue(
                    new Tween(1)
                        .For(Green)
                            .MoveLocalY(2.5f))
                        .Start();

        private AbstractAnimation GetTween()
            => new Tween(1)
                .SetTimeScale(2)
                .SetLoopCount(3)
                .For(Green)
                    .MoveLocalYTo(3.5f);

        private async void Start()
        {
            await Task.Yield();
        }
#pragma warning restore IDE0051, RCS1213

        public void ResetMob()
        {
            static void reset(Transform transform, Vector3 position)
            {
                transform.localPosition = position;
            }

            reset(Green, new Vector3(-2f, 0f, 0f));
            reset(Yellow, new Vector3(0f, 0f, 0f));
            reset(Red, new Vector3(2f, 0f, 0f));
        }
    }
}