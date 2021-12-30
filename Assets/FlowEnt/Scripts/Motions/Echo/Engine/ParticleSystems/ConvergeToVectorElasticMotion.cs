using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace FriedSynapse.FlowEnt.Motions.Echo.ParticleSystems
{
    public class ConvergeToVectorElasticMotion : ConvergeToVectorMotion
    {
        public ConvergeToVectorElasticMotion(ParticleSystem item, Vector3 target, float speed = DefaultSpeed) : base(item, target, speed)
        {
        }

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
                particles[i].position = Vector3.MoveTowards(particles[i].position, target, deltaTime * speed * Vector3.Distance(particle.position, target));
            }
            item.SetParticles(particles, activeCount);
        }
    }
}