using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public class ScaleVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public ScaleVectorMotion(TTransform item, Vector3 value) : base(item)
        {
            this.value = value;
        }

        private readonly Vector3 value;
        private Vector3 from;
        private Vector3 to;

        public override void OnStart()
        {
            from = item.localScale;
            to = Vector3.Scale(from, value);
        }

        public override void OnUpdate(float t)
        {
            item.localScale = Vector3.LerpUnclamped(from, to, t);
        }
    }
}