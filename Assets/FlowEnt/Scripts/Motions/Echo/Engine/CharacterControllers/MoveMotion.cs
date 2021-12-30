using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.CharacterControllers
{
    /// <summary>
    /// Moves the charater using the inputs.
    /// </summary>
    public class MoveMotion : AbstractEchoMotion<CharacterController>
    {
        public const float DefaultSpeed = 10f;
        public MoveMotion(CharacterController item, float speed = DefaultSpeed) : base(item)
        {
            this.speed = speed;
            transform = item.transform;
        }

        private readonly float speed;
        private readonly Transform transform;

        public override void OnUpdate(float deltaTime)
        {
            Vector3 move = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            item.Move(speed * deltaTime * move);
        }
    }
}