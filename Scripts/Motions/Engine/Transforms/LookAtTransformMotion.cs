using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class LookAtTransformMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public LookAtTransformMotion(TTransform item, Transform target) : base(item)
        {
            this.target = target;
        }

        private readonly Transform target;

        public override void OnUpdate(float t)
        {
            item.LookAt(target, Vector3.up);
        }
    }
}