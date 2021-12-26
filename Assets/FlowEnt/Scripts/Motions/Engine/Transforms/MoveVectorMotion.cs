using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.position" /> value.
    /// </summary>
    public class MoveVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveVectorMotion(TTransform item, Vector3 value) : base(item)
        {
            this.value = value;
        }

        private readonly Vector3 value;
        private Vector3 from;
        private Vector3 to;

        public override void OnStart()
        {
            from = item.position;
            to = from + value;
        }

        public override void OnUpdate(float t)
        {
            item.position = Vector3.LerpUnclamped(from, to, t);
        }
    }
}
