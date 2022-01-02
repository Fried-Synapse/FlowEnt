using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Moves the transform towards the target using the deltaTime as a step. If target is moved it'll continue to move towards.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class MoveToTransformMotion<TTransform> : MoveToVectorMotion<TTransform>
        where TTransform : Transform
    {
        public MoveToTransformMotion(TTransform item, Transform target, float speed = DefaultSpeed, SpeedType speedType = DefaultSpeedType) : base(item, target.position, speed, speedType)
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