using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.eulerAngles" /> value by axis.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class RotateAxisMotion<TTransform> : AbstractAxisMotion<TTransform>
        where TTransform : Transform
    {
        public RotateAxisMotion(TTransform item, Axis axis, float value) : base(item, axis, value)
        {
        }

        public RotateAxisMotion(TTransform item, Axis axis, float? from, float to) : base(item, axis, from, to)
        {
        }

        protected override Vector3 Target { get => item.eulerAngles; set => item.eulerAngles = value; }
    }
}