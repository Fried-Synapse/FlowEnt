using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    public class MoveTowardsTransformMotion<TTransform> : MoveTowardsVectorMotion<TTransform>
        where TTransform : Transform
    {
        public MoveTowardsTransformMotion(TTransform item, Transform target, float speed = DefaultSpeed) : base(item, target.position, speed)
        {
            this.target = target;
        }

        protected new readonly Transform target;

        public override void OnUpdate(float t)
        {
            base.target = target.position;
            base.OnUpdate(t);
        }
    }
}