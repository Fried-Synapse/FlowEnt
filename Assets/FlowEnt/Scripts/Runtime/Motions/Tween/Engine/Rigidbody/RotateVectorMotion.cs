using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="Rigidbody.rotation.eulerAngles" /> value.
    /// </summary>
    /// <typeparam name="TRigidbody"></typeparam>
    public class RotateVectorMotion<TRigidbody> : AbstractVector3Motion<TRigidbody>
        where TRigidbody : Rigidbody
    {
        public RotateVectorMotion(TRigidbody item, Vector3 value) : base(item, value)
        {
        }

        public RotateVectorMotion(TRigidbody item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.rotation.eulerAngles;
        protected override void SetValue(Vector3 value) => item.rotation = Quaternion.Euler(value);
    }
}
