using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class LookAtVector3Motion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public LookAtVector3Motion(TTransform item, Vector3 target) : base(item)
        {
            this.target = target;
        }

        private readonly Vector3 target;

        public override void OnUpdate(float t)
        {
            item.LookAt(target, Vector3.up);
        }
    }
}