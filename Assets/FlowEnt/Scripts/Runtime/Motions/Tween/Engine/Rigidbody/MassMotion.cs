using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="Rigidbody.mass" /> value.
    /// </summary>
    /// <typeparam name="TRigidbody"></typeparam>
    public class MassMotion<TRigidbody> : AbstractFloatMotion<TRigidbody>
        where TRigidbody : Rigidbody
    {
        public MassMotion(TRigidbody item, float value) : base(item, value)
        {
        }

        public MassMotion(TRigidbody item, float? from, float to) : base(item, from, to)
        {
        }

        protected override float GetFrom() => item.mass;
        protected override void SetValue(float value) => item.mass = value;
    }
}