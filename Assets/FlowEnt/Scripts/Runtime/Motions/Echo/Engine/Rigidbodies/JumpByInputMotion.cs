using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Rigidbodies
{
    /// <summary>
    /// Moves the charater using the inputs.
    /// </summary>
    public class JumpByInputMotion : AbstractEchoMotion<Rigidbody>
    {
        public const float DefaultForce = 10f;
        public JumpByInputMotion(Rigidbody item, float jumpForce = DefaultForce) : base(item)
        {
            this.jumpForce = -jumpForce;
        }

        private readonly float jumpForce;

        public override void OnUpdate(float deltaTime)
        {
            if (Input.GetButtonDown("Jump") && item.velocity.y == 0f)
            {
                item.AddForce(Vector3.up * jumpForce * Physics.gravity.y, ForceMode.Force);
            }
        }
    }
}