using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Echo.Rigidbodies;

namespace FriedSynapse.FlowEnt
{
    public static class RigidbodyMotionExtensions
    {
        /// <summary>
        /// Applies a <see cref="MoveByInputMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        public static EchoMotionProxy<Rigidbody> MoveByInput(this EchoMotionProxy<Rigidbody> proxy, float speed = MoveByInputMotion.DefaultSpeed)
            => proxy.Apply(new MoveByInputMotion(proxy.Item, speed));

        /// <summary>
        /// Applies a <see cref="RotateByInputMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        public static EchoMotionProxy<Rigidbody> RotateByInput(this EchoMotionProxy<Rigidbody> proxy, Transform camera, float speed = RotateByInputMotion.DefaultSensitivity)
            => proxy.Apply(new RotateByInputMotion(proxy.Item, camera, speed));

        /// <summary>
        /// Applies a <see cref="JumpByInputMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="force"></param>
        public static EchoMotionProxy<Rigidbody> JumpByInput(this EchoMotionProxy<Rigidbody> proxy, float force = JumpByInputMotion.DefaultForce)
            => proxy.Apply(new JumpByInputMotion(proxy.Item, force));
    }
}