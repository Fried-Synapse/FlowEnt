using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localPosition" /> value by axis.
    /// </summary>
    public class MoveLocalAxisMotion<TTransform> : AbstractAxisMotion<TTransform>
        where TTransform : Transform
    {
        public MoveLocalAxisMotion(TTransform item, Axis axis, float value) : base(item, axis, value)
        {
        }

        public MoveLocalAxisMotion(TTransform item, Axis axis, float? from, float to) : base(item, axis, from, to)
        {
        }

        protected override Vector3 Target { get => item.localPosition; set => item.localPosition = value; }
    }
}