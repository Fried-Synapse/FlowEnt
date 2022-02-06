using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localPosition" /> value.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class MoveLocalVectorMotion<TTransform> : AbstractVector3Motion<TTransform>
        where TTransform : Transform
    {
        public MoveLocalVectorMotion(TTransform item, Vector3 value) : base(item, value)
        {
        }

        public MoveLocalVectorMotion(TTransform item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.localPosition;
        protected override void SetValue(Vector3 value) => item.localPosition = value;
    }
}