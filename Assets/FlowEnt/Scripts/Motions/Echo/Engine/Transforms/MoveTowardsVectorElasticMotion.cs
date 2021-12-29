using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    public class MoveTowardsVectorElasticMotion<TTransform> : MoveTowardsVectorMotion<TTransform>
        where TTransform : Transform
    {
        public MoveTowardsVectorElasticMotion(TTransform item, Vector3 target, float speed = DefaultSpeed) : base(item, target, speed)
        {
            this.speed = speed;
        }

        private new readonly float speed;

        public override void OnUpdate(float t)
        {
            base.speed = speed * Vector3.Distance(item.position, target);
            base.OnUpdate(t);
        }
    }
}