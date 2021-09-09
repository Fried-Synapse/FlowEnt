using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public class MoveToAxisMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveToAxisMotion(TTransform item, Axis axis, float to) : base(item)
        {
            this.axis = axis;
            this.to = to;
        }

        public MoveToAxisMotion(TTransform item, Axis axis, float from, float to) : this(item, axis, to)
        {
            hasFrom = true;
            this.from = from;
        }

        private readonly Axis axis;
        private readonly bool hasFrom;
        private float from;
        private readonly float to;

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
                        from = item.position.x;
                        break;
                    case Axis.Y:
                    case Axis.YZ:
                        from = item.position.y;
                        break;
                    case Axis.Z:
                        from = item.position.z;
                        break;
                }
            }
        }

        private float valueCache;
        private Vector3 positionCache;

        public override void OnUpdate(float t)
        {
            positionCache = item.position;
            valueCache = Mathf.LerpUnclamped(from, to, t);

            if (axis.HasFlag(Axis.X))
            {
                positionCache.x = valueCache;
            }
            if (axis.HasFlag(Axis.Y))
            {
                positionCache.y = valueCache;
            }
            if (axis.HasFlag(Axis.Z))
            {
                positionCache.z = valueCache;
            }

            item.position = positionCache;
        }
    }
}