using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Moves the transform towards the target using the deltaTime as a step. If target is moved it'll continue to move towards.
    /// </summary>
    public class MoveToTransformMotion : MoveToVectorMotion
    {
        [Serializable]
        public new class Builder : AbstractFloatSpeedTypeBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private Transform target;
#pragma warning restore IDE0044, RCS1169

            public override IEchoMotion Build()
                => new MoveToTransformMotion(item, target, speed, speedType);
        }

        public MoveToTransformMotion(Transform item, Transform target, float speed = DefaultSpeed, SpeedType speedType = DefaultSpeedType) : base(item, target.position, speed, speedType)
        {
            this.target = target;
        }

        protected new readonly Transform target;

        public override void OnUpdate(float deltaTime)
        {
            base.target = target.position;
            base.OnUpdate(deltaTime);
        }
    }
}