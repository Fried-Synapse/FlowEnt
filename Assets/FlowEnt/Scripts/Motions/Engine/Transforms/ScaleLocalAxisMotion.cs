using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public class ScaleLocalAxisMotion<TTransform> : AbstractAxisMotion<TTransform>
        where TTransform : Transform
    {
        public ScaleLocalAxisMotion(TTransform item, Axis axis, float value) : base(item, axis, value)
        {
        }

        public ScaleLocalAxisMotion(TTransform item, Axis axis, float? from, float to) : base(item, axis, from, to)
        {
        }

        protected override Vector3 Target { get => item.localScale; set => item.localScale = value; }
    }
}