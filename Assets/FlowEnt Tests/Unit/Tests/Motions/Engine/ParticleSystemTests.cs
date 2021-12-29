using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static UnityEngine.ParticleSystem;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class ParticleSystemTests : AbstractEngineTests<ParticleSystem>
    {
        private void SetModules(int particleCount)
        {
            EmissionModule emission = Component.emission;
            emission.rateOverTime = 0;
            emission.SetBursts(new Burst[]
                {
                    new Burst(0, particleCount)
                });
            MainModule main = Component.main;
            main.startSpeed = 0;
        }

        private const float Speed = 30f;
        private const float ConvergeToValue = 2f;

        [UnityTest]
        public IEnumerator ConvergeToVector()
        {
            const int particleCount = 10;
            Vector3 target = new Vector3(ConvergeToValue, ConvergeToValue, ConvergeToValue);

            yield return CreateTester()
                .Arrange(() => SetModules(particleCount))
                .Act(() => Component.Echo(TestTime).ConvergeTo(target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Particle[] particles = new Particle[Component.main.maxParticles];
                    int activeCount = Component.GetParticles(particles);
                    Assert.AreEqual(particleCount, activeCount);
                    for (int i = 0; i < activeCount; i++)
                    {
                        FlowEntAssert.AreEqual(target, particles[i].position);
                    }
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ConvergeToVectorElastic()
        {
            const int particleCount = 10;
            Vector3 target = new Vector3(ConvergeToValue, ConvergeToValue, ConvergeToValue);

            yield return CreateTester()
                .Arrange(() => SetModules(particleCount))
                .Act(() => Component.Echo(TestTime).ConvergeToElastic(target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Particle[] particles = new Particle[Component.main.maxParticles];
                    int activeCount = Component.GetParticles(particles);
                    Assert.AreEqual(particleCount, activeCount);
                    for (int i = 0; i < activeCount; i++)
                    {
                        FlowEntAssert.AreEqual(target, particles[i].position);
                    }
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ConvergeToTransform()
        {
            const int particleCount = 10;

            yield return CreateTester()
                .Arrange(() => SetModules(particleCount))
                .Act(() => Component.Echo(TestTime).ConvergeTo(Variables.Target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Particle[] particles = new Particle[Component.main.maxParticles];
                    int activeCount = Component.GetParticles(particles);
                    Assert.AreEqual(particleCount, activeCount);
                    for (int i = 0; i < activeCount; i++)
                    {
                        FlowEntAssert.AreEqual(Variables.Target.position, particles[i].position);
                    }
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ConvergeToTransformElastic()
        {
            const int particleCount = 10;

            yield return CreateTester()
                .Arrange(() => SetModules(particleCount))
                .Act(() => Component.Echo(TestTime).ConvergeToElastic(Variables.Target, Speed).Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Particle[] particles = new Particle[Component.main.maxParticles];
                    int activeCount = Component.GetParticles(particles);
                    Assert.AreEqual(particleCount, activeCount);
                    for (int i = 0; i < activeCount; i++)
                    {
                        FlowEntAssert.AreEqual(Variables.Target.position, particles[i].position);
                    }
                })
                .Run();
        }
    }
}
