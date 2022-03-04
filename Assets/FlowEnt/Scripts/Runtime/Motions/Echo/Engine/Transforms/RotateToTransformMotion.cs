using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Rotates the transform towards the target using the deltaTime as a step. If target is rotated it'll continue to rotate towards.
    /// </summary>
    public class RotateToTransformMotion : RotateToQuaternionMotion
    {
        [Serializable]
        public new class Builder : AbstractFloatSpeedTypeBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private Transform target;
#pragma warning restore IDE0044, RCS1169

            public override IEchoMotion Build()
                => new RotateToTransformMotion(item, target, speed, speedType);
        }

        public RotateToTransformMotion(Transform item, Transform target, float speed = DefaultSpeed, SpeedType speedType = DefaultSpeedType) : base(item, target.rotation, speed, speedType)
        {
            this.target = target;
        }

        protected new readonly Transform target;

        public override void OnUpdate(float deltaTime)
        {
            base.target = target.rotation;
            base.OnUpdate(deltaTime);
        }
    }
}