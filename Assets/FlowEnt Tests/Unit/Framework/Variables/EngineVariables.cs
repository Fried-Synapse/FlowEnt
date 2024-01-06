using UnityEngine;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public class EngineVariables : AbstractVariables
    {
        [SerializeField]
        private TweenBuilder tween;

        public TweenBuilder Tween => tween;

        [SerializeField]
        private EchoBuilder echo;

        public EchoBuilder Echo => echo;

        [SerializeField]
        private FlowBuilder flow;

        public FlowBuilder Flow => flow;

        [SerializeField]
        private Transform target;

        public Transform Target => target;

        [SerializeField]
        private AnimationCurve3d animationCurve;

        public AnimationCurve3d AnimationCurve => animationCurve;

        [SerializeField]
        private Gradient gradient;

        public Gradient Gradient => gradient;
    }
}