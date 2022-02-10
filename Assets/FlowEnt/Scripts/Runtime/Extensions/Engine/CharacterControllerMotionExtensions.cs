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
        public static EchoMotionProxy<TCharacterController> MoveByInput<TCharacterController>(this EchoMotionProxy<TCharacterController> proxy, float speed = MoveByInputMotion<TCharacterController>.DefaultSpeed)
        where TCharacterController : CharacterController
            => proxy.Apply(new MoveByInputMotion<TCharacterController>(proxy.Item, speed));

        /// <summary>
        /// Applies a <see cref="RotateByInputMotion" /> to the echo.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        public static EchoMotionProxy<TCharacterController> RotateByInput<TCharacterController>(this EchoMotionProxy<TCharacterController> proxy, Transform camera, float speed = RotateByInputMotion<TCharacterController>.DefaultSensitivity)
            where TCharacterController : CharacterController
            => proxy.Apply(new RotateByInputMotion<TCharacterController>(proxy.Item, camera, speed));
    }
}