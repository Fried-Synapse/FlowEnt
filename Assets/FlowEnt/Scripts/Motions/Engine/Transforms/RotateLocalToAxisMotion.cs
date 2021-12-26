using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public class RotateLocalToAxisMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public RotateLocalToAxisMotion(TTransform item, Axis axis, float to) : base(item)
        {
            this.to = to;
            this.axis = axis;
        }

        public RotateLocalToAxisMotion(TTransform item, Axis axis, float from, float to) : this(item, axis, to)
        {
            hasFrom = true;
            this.from = from;
        }

        private readonly Axis axis;
        private readonly bool hasFrom;
        private float from;
        private readonly float to;

        private float valueCache;
        private Vector3 rotationCache;

        public override void OnStart()
        {
            if (!hasFrom)
            {
                switch (axis)
                {
                    case Axis.X:
                    case Axis.XY:
                    case Axis.XZ:
                    case Axis.XYZ:
                        from = item.localEulerAngles.x;
                        break;
                    case Axis.Y:
                    case Axis.YZ:
                        from = item.localEulerAngles.y;
                        break;
                    case Axis.Z:
                        from = item.localEulerAngles.z;
                        break;
                }
            }
        }

        public override void OnUpdate(float t)
        {
            rotationCache = item.localEulerAngles;
            valueCache = Mathf.LerpUnclamped(from, to, t);

            if ((axis & Axis.X) != 0)
            {
                rotationCache.x = valueCache;
            }
            if ((axis & Axis.Y) != 0)
            {
                rotationCache.y = valueCache;
            }
            if ((axis & Axis.Z) != 0)
            {
                rotationCache.z = valueCache;
            }

            item.localEulerAngles = rotationCache;
        }
    }
}