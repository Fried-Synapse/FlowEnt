using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Echo.ParticleSystems;

namespace FriedSynapse.FlowEnt
{
    public static class ParticleSystemMotionExtensions
    {
        /// <summary>
        /// Applies a <see cref="ConvergeToVectorMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        public static EchoMotionProxy<ParticleSystem> ConvergeTo(this EchoMotionProxy<ParticleSystem> proxy, Vector3 target, float speed = ConvergeToVectorMotion.DefaultSpeed)
            => proxy.Apply(new ConvergeToVectorMotion(proxy.Item, target, speed));

        /// <summary>
        /// Applies a <see cref="ConvergeToVectorElasticMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        public static EchoMotionProxy<ParticleSystem> ConvergeToElastic(this EchoMotionProxy<ParticleSystem> proxy, Vector3 target, float speed = ConvergeToVectorMotion.DefaultSpeed)
            => proxy.Apply(new ConvergeToVectorElasticMotion(proxy.Item, target, speed));

        /// <summary>
        /// Applies a <see cref="ConvergeToTransformMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        public static EchoMotionProxy<ParticleSystem> ConvergeTo(this EchoMotionProxy<ParticleSystem> proxy, Transform target, float speed = ConvergeToVectorMotion.DefaultSpeed)
            => proxy.Apply(new ConvergeToTransformMotion(proxy.Item, target, speed));

        /// <summary>
        /// Applies a <see cref="ConvergeToTransformElasticMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        public static EchoMotionProxy<ParticleSystem> ConvergeToElastic(this EchoMotionProxy<ParticleSystem> proxy, Transform target, float speed = ConvergeToVectorMotion.DefaultSpeed)
            => proxy.Apply(new ConvergeToTransformElasticMotion(proxy.Item, target, speed));
    }
}