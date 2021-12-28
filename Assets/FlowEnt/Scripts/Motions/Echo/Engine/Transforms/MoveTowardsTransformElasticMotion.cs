using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    public class MoveTowardsTransformElasticMotion<TTransform> : MoveTowardsTransformMotion<TTransform>
        where TTransform : Transform
    {
        public MoveTowardsTransformElasticMotion(TTransform item, Transform target, float speed = DefaultSpeed) : base(item, target, speed)
        {
            this.speed = speed;
        }

        private new readonly float speed;

        public override void OnUpdate(float t)
        {
            base.speed = speed * Vector3.Distance(item.position, target.position);
            base.OnUpdate(t);
        }
    }
}