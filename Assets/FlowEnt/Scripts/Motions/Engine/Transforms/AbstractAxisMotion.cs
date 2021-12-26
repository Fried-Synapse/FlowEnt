using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public abstract class AbstractAxisMotion<TItem> : AbstractFloatMotion<TItem>
    {
        protected AbstractAxisMotion(TItem item, Axis axis, float value) : base(item, value)
        {
            this.axis = axis;
        }

        protected AbstractAxisMotion(TItem item, Axis axis, float? from, float to) : base(item, from, to)
        {
            this.axis = axis;
        }

        private readonly Axis axis;
        private Vector3 cache;
        protected abstract Vector3 Target { get; set; }
        protected override float GetFrom()
        {
            Vector3 target = Target;
            switch (axis)
            {
                case Axis.X:
                case Axis.XY:
                case Axis.XZ:
                case Axis.XYZ:
                    return target.x;
                case Axis.Y:
                case Axis.YZ:
                    return target.y;
                case Axis.Z:
                    return target.z;
                default:
                    return 0;
            }
        }

        protected override void SetValue(float value)
        {
            cache = Target;

            if ((axis & Axis.X) != 0)
            {
                cache.x = value;
            }
            if ((axis & Axis.Y) != 0)
            {
                cache.y = value;
            }
            if ((axis & Axis.Z) != 0)
            {
                cache.z = value;
            }

            Target = cache;
        }
    }
}