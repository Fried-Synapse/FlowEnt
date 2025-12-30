using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Moves the transform towards the target using the deltaTime as a step.
    /// </summary>
    public class MoveToVectorMotion : AbstractFloatSpeedTypeMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractFloatSpeedTypeBuilder
        {
            [SerializeField]
            private Vector3 target;

            public override AbstractEchoMotion Build()
                => new MoveToVectorMotion(item, target, speed, speedType);
        }

        public MoveToVectorMotion(Transform item, Vector3 target, float speed = DefaultSpeed, SpeedType speedType = DefaultSpeedType) : base(item, speed, speedType)
        {
            this.target = target;
        }

        protected Vector3 target;

        public override void OnUpdate(float deltaTime)
        {
            item.position = Vector3.MoveTowards(item.position, target, GetSpeed(Vector3.Distance(item.position, target)) * deltaTime);
        }
    }
}