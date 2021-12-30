using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace FriedSynapse.FlowEnt.Motions.Echo.ParticleSystems
{
    public class ConvergeToVectorMotion : AbstractEchoMotion<ParticleSystem>
    {
        public const float DefaultSpeed = 1f;
        public ConvergeToVectorMotion(ParticleSystem item, Vector3 target, float speed = DefaultSpeed) : base(item)
        {
            this.target = target;
            this.speed = speed;
        }

        protected Vector3 target;
        protected float speed;
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
                particles[i].position = Vector3.MoveTowards(particles[i].position, target, deltaTime * speed);
            }
            item.SetParticles(particles, activeCount);
        }
    }
}