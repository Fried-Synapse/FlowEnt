using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="TRigidbody.position" /> value.
    /// </summary>
    public class MoveVectorMotion<TRigidbody> : AbstractVector3Motion<TRigidbody>
        where TRigidbody : Rigidbody
    {
        public MoveVectorMotion(TRigidbody item, Vector3 value) : base(item, value)
        {
        }

        public MoveVectorMotion(TRigidbody item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.position;
        protected override void SetValue(Vector3 value) => item.position = value;
    }
}
