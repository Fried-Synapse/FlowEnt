using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.ParticleSystems
{
    public class ConvergeToTransformMotion : ConvergeToVectorMotion
    {
        public ConvergeToTransformMotion(ParticleSystem item, Transform target, float speed = DefaultSpeed, SpeedType speedType = DefaultSpeedType) : base(item, target.position, speed, speedType)
        {
            this.target = target;
        }

        private new readonly Transform target;

        public override void OnUpdate(float deltaTime)
        {
            base.target = target.position;
            base.OnUpdate(deltaTime);
        }
    }
}