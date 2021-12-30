using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Echo.CharacterControllers;

namespace FriedSynapse.FlowEnt
{
    public static class CharacterControllerMotionExtensions
    {
        /// <summary>
        /// Applies a <see cref="MoveMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        public static EchoMotionProxy<CharacterController> Move(this EchoMotionProxy<CharacterController> proxy, float speed = MoveMotion.DefaultSpeed)
            => proxy.Apply(new MoveMotion(proxy.Item, speed));

        /// <summary>
        /// Applies a <see cref="RotateMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="speed"></param>
        public static EchoMotionProxy<CharacterController> Rotate(this EchoMotionProxy<CharacterController> proxy, Transform camera, float speed = RotateMotion.DefaultSensitivity)
            => proxy.Apply(new RotateMotion(proxy.Item, camera, speed));
    }
}