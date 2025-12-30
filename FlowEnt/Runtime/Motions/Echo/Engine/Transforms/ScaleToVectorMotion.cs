using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Scales the transform towards the target using the deltaTime as a step.
    /// </summary>
    public class ScaleToVectorMotion : AbstractFloatSpeedTypeMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractFloatSpeedTypeBuilder
        {
            [SerializeField]
            private Vector3 target;

            public override AbstractEchoMotion Build()
                => new ScaleToVectorMotion(item, target, speed, speedType);
        }

        public ScaleToVectorMotion(Transform item, Vector3 target, float speed = DefaultSpeed, SpeedType speedType = DefaultSpeedType) : base(item, speed, speedType)
        {
            this.target = target;
        }

        protected Vector3 target;

        public override void OnUpdate(float deltaTime)
        {
            item.localScale = Vector3.MoveTowards(item.localScale, target, GetSpeed(item.localScale.magnitude / target.magnitude) * deltaTime);
        }
    }
}