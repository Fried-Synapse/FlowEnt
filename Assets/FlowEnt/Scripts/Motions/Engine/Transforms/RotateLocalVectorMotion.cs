using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localEulerAngles" /> value.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class RotateLocalVectorMotion<TTransform> : AbstractVector3Motion<TTransform>
        where TTransform : Transform
    {
        public RotateLocalVectorMotion(TTransform item, Vector3 value) : base(item, value)
        {
        }

        public RotateLocalVectorMotion(TTransform item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.localEulerAngles;
        protected override void SetValue(Vector3 value) => item.localEulerAngles = value;
    }
}