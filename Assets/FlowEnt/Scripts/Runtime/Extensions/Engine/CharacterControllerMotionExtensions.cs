using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Echo.CharacterControllers;

namespace FriedSynapse.FlowEnt
{
    public static class CharacterControllerMotionExtensions
    {
        /// <summary>
        /// Applies a <see cref="MoveByInputMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        public static EchoMotionProxy<CharacterController> MoveByInput(this EchoMotionProxy<CharacterController> proxy, float speed = MoveByInputMotion.DefaultSpeed)
            => proxy.Apply(new MoveByInputMotion(proxy.Item, speed));

        /// <summary>
        /// Applies a <see cref="RotateByInputMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        public static EchoMotionProxy<CharacterController> RotateByInput(this EchoMotionProxy<CharacterController> proxy, Transform camera, float speed = RotateByInputMotion.DefaultSensitivity)
            => proxy.Apply(new RotateByInputMotion(proxy.Item, camera, speed));
    }
}