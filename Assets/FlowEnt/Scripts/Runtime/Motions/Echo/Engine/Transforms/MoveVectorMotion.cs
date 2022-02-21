using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Moves the transform using the provided speed.
    /// </summary>
    public class MoveVectorMotion : AbstractEchoMotion<Transform>
    {
        public MoveVectorMotion(Transform item, Vector3 speed) : base(item)
        {
            this.speed = speed;
        }

        private readonly Vector3 speed;

        public override void OnUpdate(float deltaTime)
        {
            item.position += speed * deltaTime;
        }
    }

    [Serializable]
    public class MoveVectorMotionBuilder : AbstractEchoMotionBuilder<Transform>
    {
        [SerializeField]
        private Vector3 speed;

        public override IEchoMotion Build()
            => new MoveVectorMotion(item, speed);
    }
}