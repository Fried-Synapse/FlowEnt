using UnityEngine;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public class UIVariables : AbstractVariables
    {
        [SerializeField]
        private Gradient gradient;
        public Gradient Gradient => gradient;
    }
}