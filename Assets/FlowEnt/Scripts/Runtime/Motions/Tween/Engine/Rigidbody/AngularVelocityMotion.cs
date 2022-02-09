using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="TRigidbody.angularVelocity" /> value.
    /// </summary>
    public class AngularVelocityMotion<TRigidbody> : AbstractVector3Motion<TRigidbody>
        where TRigidbody : Rigidbody
    {
        public AngularVelocityMotion(TRigidbody item, Vector3 value) : base(item, value)
        {
        }

        public AngularVelocityMotion(TRigidbody item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.angularVelocity;
        protected override void SetValue(Vector3 value) => item.angularVelocity = value;
    }
}
