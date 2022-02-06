using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Scales the transform towards the target using the deltaTime as a step. If target is scaled it'll continue to move towards.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class ScaleToTransformMotion<TTransform> : ScaleToVectorMotion<TTransform>
        where TTransform : Transform
    {
        public ScaleToTransformMotion(TTransform item, Transform target, float speed = DefaultSpeed, SpeedType speedType = DefaultSpeedType) : base(item, target.position, speed, speedType)
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