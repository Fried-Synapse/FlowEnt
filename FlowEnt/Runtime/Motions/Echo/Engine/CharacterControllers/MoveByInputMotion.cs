using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.CharacterControllers
{
    /// <summary>
    /// Moves the charater using the inputs.
    /// </summary>
    public class MoveByInputMotion : AbstractFloatSpeedMotion<CharacterController>
    {
        [Serializable]
        public class Builder : AbstractFloatSpeedBuilder
        {
            public override AbstractEchoMotion Build()
                => new MoveByInputMotion(item, speed);
        }
        public MoveByInputMotion(CharacterController item, float speed = DefaultSpeed) : base(item, speed)
        {
            transform = item.transform;
        }

        private readonly Transform transform;

        public override void OnUpdate(float deltaTime)
        {
            Vector3 move = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            item.Move(speed * deltaTime * move);
        }
    }
}