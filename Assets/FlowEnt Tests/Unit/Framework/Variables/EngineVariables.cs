using UnityEngine;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public class EngineVariables : AbstractVariables
    {
#pragma warning disable RCS1169, IDE0044
        [SerializeField]
        private TweenOptionsBuilder tweenOptionsBuilder;
        public TweenOptionsBuilder TweenOptionsBuilder => tweenOptionsBuilder;

        [SerializeField]
        private TweenEventsBuilder tweenEventsBuilder;
        public TweenEventsBuilder TweenEventsBuilder => tweenEventsBuilder;

        [SerializeField]
        private EchoOptionsBuilder echoOptionsBuilder;
        public EchoOptionsBuilder EchoOptionsBuilder => echoOptionsBuilder;

        [SerializeField]
        private EchoEventsBuilder echoEventsBuilder;
        public EchoEventsBuilder EchoEventsBuilder => echoEventsBuilder;

        [SerializeField]
        private Transform target;
        public Transform Target => target;

        [SerializeField]
        private AnimationCurve3d animationCurve;
        public AnimationCurve3d AnimationCurve => animationCurve;

        [SerializeField]
        private Gradient gradient;
        public Gradient Gradient => gradient;
#pragma warning restore RCS1169, IDE0044
    }
}
