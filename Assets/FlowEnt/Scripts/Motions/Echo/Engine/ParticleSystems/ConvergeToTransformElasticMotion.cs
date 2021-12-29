using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.ParticleSystems
{
    public class ConvergeToTransformElasticMotion : ConvergeToVectorElasticMotion
    {
        public ConvergeToTransformElasticMotion(ParticleSystem item, Transform target, float speed = DefaultSpeed) : base(item, target.position, speed)
        {
            this.target = target;
        }

        private new readonly Transform target;

        public override void OnUpdate(float t)
        {
            base.target = target.position;
            base.OnUpdate(t);
        }
    }
}