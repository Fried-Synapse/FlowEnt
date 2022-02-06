using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.eulerAngles" /> value.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class RotateVectorMotion<TTransform> : AbstractVector3Motion<TTransform>
        where TTransform : Transform
    {
        public RotateVectorMotion(TTransform item, Vector3 value) : base(item, value)
        {
        }

        public RotateVectorMotion(TTransform item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.eulerAngles;
        protected override void SetValue(Vector3 value) => item.eulerAngles = value;
    }
}
