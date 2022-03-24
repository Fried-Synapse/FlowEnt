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
        private void Start()
        {
            Echo.Build().Start();
            Tween.Build().Start();
            Flow.Build().Start();
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