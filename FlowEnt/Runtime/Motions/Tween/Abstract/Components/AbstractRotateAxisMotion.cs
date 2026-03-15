using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractRotateAxisMotion<TTransform> : AbstractFloatMotion<TTransform>
            where TTransform : Transform
    {
        [Serializable]
        public abstract class AbstractRotateAxisValueBuilder : AbstractValueBuilder
        {
            [SerializeField]
            protected Axis axis;
        }

        [Serializable]
        public abstract class AbstractRotateAxisFromToBuilder : AbstractFromToBuilder
        {
            [SerializeField]
            protected Axis axis;
        }

        protected AbstractRotateAxisMotion(TTransform item, Axis axis, float value) : base(item, value)
        {
            this.axis = axis;
        }

        protected AbstractRotateAxisMotion(TTransform item, Axis axis, float? from, float to) : base(item, from, to)
        {
            this.axis = axis;
        }

        protected abstract Vector3 EulerAngles { get; }
        private readonly Axis axis;
        private float lastValue;
        protected override float GetFrom()
        {
            Vector3 target = EulerAngles;

            switch (axis)
            {
                case Axis.X:
                case Axis.XY:
                case Axis.XZ:
                case Axis.XYZ:
                    lastValue = target.x;
                    break;
                case Axis.Y:
                case Axis.YZ:
                    lastValue = target.y;
                    break;
                case Axis.Z:
                    lastValue = target.z;
                    break;
                default:
                    lastValue = 0;
                    break;
            }
            return lastValue;
        }

        protected override void SetValue(float value)
        {
            float change = value - lastValue;
            Vector3 rotation = Vector3.zero;

            if ((axis & Axis.X) != 0)
            {
                rotation.x = change;
            }
            if ((axis & Axis.Y) != 0)
            {
                rotation.y = change;
            }
            if ((axis & Axis.Z) != 0)
            {
                rotation.z = change;
            }
            item.Rotate(rotation);

            lastValue = value;
        }
    }
}
