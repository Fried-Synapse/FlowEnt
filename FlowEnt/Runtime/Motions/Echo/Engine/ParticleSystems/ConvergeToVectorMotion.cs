using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace FriedSynapse.FlowEnt.Motions.Echo.ParticleSystems
{
    public class ConvergeToVectorMotion : AbstractFloatSpeedTypeMotion<ParticleSystem>
    {
        [Serializable]
        public class Builder : AbstractFloatSpeedTypeBuilder
        {
            [SerializeField]
            private Vector3 target;

            public override AbstractEchoMotion Build()
                => new ConvergeToVectorMotion(item, target, speed, speedType);
        }

        public ConvergeToVectorMotion(ParticleSystem item, Vector3 target, float speed = DefaultSpeed, SpeedType speedType = DefaultSpeedType) : base(item, speed, speedType)
        {
            this.target = target;
        }

        protected Vector3 target;
        protected Particle[] particles = new Particle[0];

        public override void OnUpdate(float deltaTime)
        {
            if (particles.Length < item.main.maxParticles)
            {
                particles = new Particle[item.main.maxParticles];
            }

            int activeCount = item.GetParticles(particles);
            for (int i = 0; i < activeCount; i++)
            {
                Particle particle = particles[i];
                particles[i].position = Vector3.MoveTowards(particle.position, target, deltaTime * GetSpeed(Vector3.Distance(particle.position, target)));
            }
            item.SetParticles(particles, activeCount);
        }
    }
}