using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    public class MoveTowardsTransformMotion<TTransform> : AbstractEchoMotion<TTransform>
        where TTransform : Transform
    {
#pragma warning disable RCS1158
        public const float DefaultSpeed = 1f;
#pragma warning restore RCS1158

        public MoveTowardsTransformMotion(TTransform item, Transform target, float speed = DefaultSpeed) : base(item)
        {
            this.target = target;
            this.speed = speed;
        }

        protected readonly Transform target;
        protected float speed;

        public override void OnUpdate(float t)
        {
            item.position = Vector3.MoveTowards(item.position, target.position, speed * t);
        }
    }
}