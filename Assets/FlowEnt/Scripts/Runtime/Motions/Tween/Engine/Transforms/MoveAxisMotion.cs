using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.position" /> value by axis.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class MoveAxisMotion<TTransform> : AbstractAxisMotion<TTransform>
        where TTransform : Transform
    {
        public MoveAxisMotion(TTransform item, Axis axis, float value) : base(item, axis, value)
        {
        }

        public MoveAxisMotion(TTransform item, Axis axis, float? from, float to) : base(item, axis, from, to)
        {
        }

        protected override Vector3 Target { get => item.position; set => item.position = value; }
    }
}