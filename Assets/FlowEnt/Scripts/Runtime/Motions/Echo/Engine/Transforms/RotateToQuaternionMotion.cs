using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Rotates the transform towards the target using the deltaTime as a step.
    /// </summary>
    public class RotateToQuaternionMotion : AbstractFloatSpeedTypeMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractFloatSpeedTypeBuilder
        {
            [SerializeField]
            private Vector3 target;

            public override IEchoMotion Build()
                => new RotateToQuaternionMotion(item, Quaternion.Euler(target), speed, speedType);
        }

        public RotateToQuaternionMotion(Transform item, Quaternion target, float speed = DefaultSpeed, SpeedType speedType = DefaultSpeedType) : base(item, speed, speedType)
        {
            this.target = target;
        }

        protected Quaternion target;

        public override void OnUpdate(float deltaTime)
        {
            item.rotation = Quaternion.RotateTowards(item.rotation, target, GetSpeed(Quaternion.Angle(item.rotation, target)) * deltaTime);
        }
    }
}