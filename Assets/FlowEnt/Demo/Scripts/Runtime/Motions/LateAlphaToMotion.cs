using FriedSynapse.FlowEnt.Motions.Tween.Materials;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    public class LateAlphaMotion : AlphaMotion
    {
        public LateAlphaMotion(Material item, float to, float percentage) : base(item, null, to)
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
