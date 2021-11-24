using FriedSynapse.FlowEnt.Motions.Renderers;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    public class LateAlphaToMotion : AlphaToMotion<MeshRenderer>
    {
        public LateAlphaToMotion(MeshRenderer item, float value, float percentage) : base(item, value)
        {
            multiplier = 1f / (1f - percentage);
        }

        private readonly float multiplier;

        public override void OnUpdate(float t)
        {
            base.OnUpdate(Mathf.Clamp01((t * multiplier) - (multiplier - 1)));
        }
    }
}
