using UnityEngine;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public class EngineVariables : AbstractVariables
    {
        [SerializeField]
        private AnimationCurve3d animationCurve;
        public AnimationCurve3d AnimationCurve => animationCurve;

        [SerializeField]
        private Gradient gradient;
        public Gradient Gradient => gradient;
    }
}
