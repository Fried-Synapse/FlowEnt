using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localEulerAngles" /> value by axis.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class RotateLocalAxisMotion<TTransform> : AbstractAxisMotion<TTransform>
        where TTransform : Transform
    {
        public RotateLocalAxisMotion(TTransform item, Axis axis, float value) : base(item, axis, value)
        {
        }

        public RotateLocalAxisMotion(TTransform item, Axis axis, float? from, float to) : base(item, axis, from, to)
        {
        }

        protected override Vector3 Target { get => item.localEulerAngles; set => item.localEulerAngles = value; }
    }
}