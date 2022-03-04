using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.ParticleSystems
{
    public class ConvergeToTransformMotion : ConvergeToVectorMotion
    {
        [Serializable]
        public new class Builder : AbstractFloatSpeedTypeBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private Transform target;
#pragma warning restore IDE0044, RCS1169

            public override IEchoMotion Build()
                => new ConvergeToTransformMotion(item, target, speed, speedType);
        }

        public ConvergeToTransformMotion(ParticleSystem item, Transform target, float speed = DefaultSpeed, SpeedType speedType = DefaultSpeedType) : base(item, target.position, speed, speedType)
        {
            this.target = target;
        }

        private new readonly Transform target;

        public override void OnUpdate(float deltaTime)
        {
            base.target = target.position;
            base.OnUpdate(deltaTime);
        }
    }
}