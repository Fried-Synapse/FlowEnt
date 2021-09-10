using FriedSynapse.FlowEnt;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public class ScaleLocalToAxisMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public ScaleLocalToAxisMotion(TTransform item, Axis axis, float to) : base(item)
        {
            this.axis = axis;
            this.to = to;
        }

        public ScaleLocalToAxisMotion(TTransform item, Axis axis, float from, float to) : this(item, axis, to)
        {
            hasFrom = true;
            this.from = from;
        }

        private readonly Axis axis;
        private readonly bool hasFrom;
        private float from;
        private readonly float to;

        private float valueCache;
        private Vector3 scaleCache;

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
                        from = item.localScale.x;
                        break;
                    case Axis.Y:
                    case Axis.YZ:
                        from = item.localScale.y;
                        break;
                    case Axis.Z:
                        from = item.localScale.z;
                        break;
                }
            }
        }

        public override void OnUpdate(float t)
        {
            scaleCache = item.localScale;
            valueCache = Mathf.LerpUnclamped(from, to, t);

            if (axis.HasFlag(Axis.X))
            {
                scaleCache.x = valueCache;
            }
            if (axis.HasFlag(Axis.Y))
            {
                scaleCache.y = valueCache;
            }
            if (axis.HasFlag(Axis.Z))
            {
                scaleCache.z = valueCache;
            }

            item.localScale = scaleCache;
        }
    }
}