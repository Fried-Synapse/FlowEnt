using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace FriedSynapse.FlowEnt.Motions.Echo.ParticleSystems
{
    public class ConvergeToVectorMotion : AbstractEchoMotion<ParticleSystem>
    {
        public const float DefaultSpeed = 1f;
        public const SpeedType DefaultSpeedType = SpeedType.Linear;
        public ConvergeToVectorMotion(ParticleSystem item, Vector3 target, float speed = DefaultSpeed, SpeedType speedType = DefaultSpeedType) : base(item)
        {
            this.target = target;
            this.speed = speed;
            this.speedType = speedType;
        }

        protected Vector3 target;
        protected float speed;
        private readonly SpeedType speedType;
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
                float speed = 0;
                switch (speedType)
                {
                    case SpeedType.Linear:
                        speed = this.speed;
                        break;
                    case SpeedType.Elastic:
                        speed = this.speed * Vector3.Distance(particle.position, target);
                        break;
                    case SpeedType.Gravity:
                        speed = this.speed / Vector3.Distance(particle.position, target);
                        break;
                }

                particles[i].position = Vector3.MoveTowards(particle.position, target, deltaTime * speed);
            }
            item.SetParticles(particles, activeCount);
        }
    }
}