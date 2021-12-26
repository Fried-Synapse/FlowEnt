using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.rotation" /> value.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class RotateQuaternionMotion<TTransform> : AbstractQuaternionMotion<TTransform>
        where TTransform : Transform
    {
        public RotateQuaternionMotion(TTransform item, Quaternion value) : base(item, value)
        {
        }

        public RotateQuaternionMotion(TTransform item, Quaternion? from, Quaternion to) : base(item, from, to)
        {
        }

        protected override Quaternion GetFrom() => item.rotation;
        protected override void SetValue(Quaternion value) => item.rotation = value;
    }
}