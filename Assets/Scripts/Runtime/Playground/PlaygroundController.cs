using UnityEngine;

namespace FriedSynapse.FlowEnt.Builder
{
    [System.Serializable]
    public class Test
    {
        public int x;
    }
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
#pragma warning restore RCS1169, IDE0044

#pragma warning disable IDE0051, RCS1213
        private void Start()
        {
            Tween.Build().Start();
        }

        private void OnDrawGizmos()
        {
        }

        public void Log(Vector3 x)
        {
            Debug.Log($"{x}");
        }
#pragma warning restore IDE0051, RCS1213
    }
}