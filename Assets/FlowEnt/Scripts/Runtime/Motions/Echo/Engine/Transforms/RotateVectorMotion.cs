using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Rotates the transform using the provided speed.
    /// </summary>
    public class RotateVectorMotion : AbstractVector3SpeedMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractVector3SpeedBuilder
        {
            public override AbstractEchoMotion Build()
                => new RotateVectorMotion(item, speed);
        }

        public RotateVectorMotion(Transform item, Vector3 speed) : base(item, speed)
        {
        }

        public override void OnUpdate(float deltaTime)
        {
            item.Rotate(speed * deltaTime);
        }
    }
}