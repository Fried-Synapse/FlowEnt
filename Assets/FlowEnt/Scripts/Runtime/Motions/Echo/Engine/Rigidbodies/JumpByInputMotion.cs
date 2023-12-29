using System;
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

        [Serializable]
        public class Builder : AbstractBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private float jumpForce = DefaultForce;
#pragma warning restore IDE0044, RCS1169
            public override AbstractEchoMotion Build()
                => new JumpByInputMotion(item, jumpForce);
        }

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