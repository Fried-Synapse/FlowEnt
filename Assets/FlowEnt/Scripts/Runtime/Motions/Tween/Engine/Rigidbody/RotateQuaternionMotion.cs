using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="Rigidbody.rotation" /> value.
    /// </summary>
    /// <typeparam name="TRigidbody"></typeparam>
    public class RotateQuaternionMotion<TRigidbody> : AbstractQuaternionMotion<TRigidbody>
        where TRigidbody : Rigidbody
    {
        public RotateQuaternionMotion(TRigidbody item, Quaternion value) : base(item, value)
        {
        }

        public RotateQuaternionMotion(TRigidbody item, Quaternion? from, Quaternion to) : base(item, from, to)
        {
        }

        protected override Quaternion GetFrom() => item.rotation;
        protected override void SetValue(Quaternion value) => item.rotation = value;
    }
}