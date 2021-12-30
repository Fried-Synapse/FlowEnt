using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    public class MoveTowardsTransformElasticMotion<TTransform> : MoveTowardsVectorElasticMotion<TTransform>
        where TTransform : Transform
    {
        public MoveTowardsTransformElasticMotion(TTransform item, Transform target, float speed = DefaultSpeed) : base(item, target.position, speed)
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