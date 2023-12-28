using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Echo.ParticleSystems;
using FriedSynapse.FlowEnt.Motions.Echo;

namespace FriedSynapse.FlowEnt
{
    public static class ParticleSystemMotionExtensions
    {
        /// <summary>
        /// Applies a <see cref="ConvergeToVectorMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        /// <param name="speedType"></param>
        public static EchoMotionProxy<ParticleSystem> ConvergeTo(this EchoMotionProxy<ParticleSystem> proxy, Vector3 target, float speed = ConvergeToVectorMotion.DefaultSpeed, SpeedType speedType = ConvergeToVectorMotion.DefaultSpeedType)
            => proxy.Apply(new ConvergeToVectorMotion(proxy.Item, target, speed, speedType));

        /// <summary>
        /// Applies a <see cref="ConvergeToTransformMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        /// <param name="speedType"></param>
        public static EchoMotionProxy<ParticleSystem> ConvergeTo(this EchoMotionProxy<ParticleSystem> proxy, Transform target, float speed = ConvergeToVectorMotion.DefaultSpeed, SpeedType speedType = ConvergeToVectorMotion.DefaultSpeedType)
            => proxy.Apply(new ConvergeToTransformMotion(proxy.Item, target, speed, speedType));
    }
}