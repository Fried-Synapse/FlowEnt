using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="Rigidbody.position" /> value by axis.
    /// </summary>
    /// <typeparam name="TRigidbody"></typeparam>
    public class MoveAxisMotion<TRigidbody> : AbstractAxisMotion<TRigidbody>
        where TRigidbody : Rigidbody
    {
        public MoveAxisMotion(TRigidbody item, Axis axis, float value) : base(item, axis, value)
        {
        }

        public MoveAxisMotion(TRigidbody item, Axis axis, float? from, float to) : base(item, axis, from, to)
        {
        }

        protected override Vector3 Target { get => item.position; set => item.position = value; }
    }
}