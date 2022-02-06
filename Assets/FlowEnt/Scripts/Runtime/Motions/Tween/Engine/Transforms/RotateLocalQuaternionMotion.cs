using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localRotation" /> value.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class RotateLocalQuaternionMotion<TTransform> : AbstractQuaternionMotion<TTransform>
        where TTransform : Transform
    {
        public RotateLocalQuaternionMotion(TTransform item, Quaternion value) : base(item, value)
        {
        }

        public RotateLocalQuaternionMotion(TTransform item, Quaternion? from, Quaternion to) : base(item, from, to)
        {
        }

        protected override Quaternion GetFrom() => item.localRotation;
        protected override void SetValue(Quaternion value) => item.localRotation = value;
    }
}