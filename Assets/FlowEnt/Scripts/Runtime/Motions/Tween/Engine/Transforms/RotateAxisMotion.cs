using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.eulerAngles" /> value by axis.
    /// </summary>
    public class RotateAxisMotion : AbstractRotateAxisMotion<Transform>
    {
        //TODO add builders

        public RotateAxisMotion(Transform item, Axis axis, float value) : base(item, axis, value)
        {
        }

        public RotateAxisMotion(Transform item, Axis axis, float? from, float to) : base(item, axis, from, to)
        {
        }

        protected override Vector3 EulerAngles => item.eulerAngles;
    }
}