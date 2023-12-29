using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Moves the transform using the provided speed.
    /// </summary>
    public class MoveVectorMotion : AbstractVector3SpeedMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractVector3SpeedBuilder
        {
            public override AbstractEchoMotion Build()
                => new MoveVectorMotion(item, speed);
        }

        public MoveVectorMotion(Transform item, Vector3 speed) : base(item, speed)
        {
        }

        public override void OnUpdate(float deltaTime)
        {
            item.position += speed * deltaTime;
        }
    }
}