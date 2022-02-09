using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="TRigidbody.velocity" /> value.
    /// </summary>
    public class VelocityMotion<TRigidbody> : AbstractVector3Motion<TRigidbody>
        where TRigidbody : Rigidbody
    {
        public VelocityMotion(TRigidbody item, Vector3 value) : base(item, value)
        {
        }

        public VelocityMotion(TRigidbody item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.velocity;
        protected override void SetValue(Vector3 value) => item.velocity = value;
    }
}
