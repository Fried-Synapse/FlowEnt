using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public class MoveLocalVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveLocalVectorMotion(TTransform item, Vector3 value) : base(item)
        {
            this.value = value;
        }

        private readonly Vector3 value;
        private Vector3 from;
        private Vector3 to;

        public override void OnStart()
        {
            from = item.localPosition;
            to = from + value;
        }

        public override void OnUpdate(float t)
        {
            item.localPosition = Vector3.LerpUnclamped(from, to, t);
        }
    }
}