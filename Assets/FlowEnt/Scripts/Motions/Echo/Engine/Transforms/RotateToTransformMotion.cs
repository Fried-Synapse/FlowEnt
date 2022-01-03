using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Rotates the transform towards the target using the deltaTime as a step. If target is rotated it'll continue to rotate towards.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class RotateToTransformMotion<TTransform> : RotateToQuaternionMotion<TTransform>
        where TTransform : Transform
    {
        public RotateToTransformMotion(TTransform item, Transform target, float speed = DefaultSpeed, SpeedType speedType = DefaultSpeedType) : base(item, target.rotation, speed, speedType)
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