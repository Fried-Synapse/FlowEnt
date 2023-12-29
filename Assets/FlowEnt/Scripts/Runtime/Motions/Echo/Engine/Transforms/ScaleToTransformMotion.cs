using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Scales the transform towards the target using the deltaTime as a step. If target is scaled it'll continue to move towards.
    /// </summary>
    public class ScaleToTransformMotion : ScaleToVectorMotion
    {
        [Serializable]
        public new class Builder : AbstractFloatSpeedTypeBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private Transform target;
#pragma warning restore IDE0044, RCS1169

            public override AbstractEchoMotion Build()
                => new ScaleToTransformMotion(item, target, speed, speedType);
        }

        public ScaleToTransformMotion(Transform item, Transform target, float speed = DefaultSpeed, SpeedType speedType = DefaultSpeedType) : base(item, target.position, speed, speedType)
        {
            this.target = target;
        }

        protected new readonly Transform target;

        public override void OnUpdate(float deltaTime)
        {
            base.target = target.localScale;
            base.OnUpdate(deltaTime);
        }
    }
}