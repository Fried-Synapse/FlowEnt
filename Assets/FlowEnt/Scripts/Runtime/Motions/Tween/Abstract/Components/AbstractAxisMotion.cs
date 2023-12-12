using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractAxisMotion<TItem> : AbstractVector3Motion<TItem>
        where TItem : class
    {
        [Serializable]
        public abstract class AbstractAxisValueBuilder : AbstractBuilder
        {
            [SerializeField]
            protected Axis axis;

            [SerializeField]
            protected float value;
        }

        [Serializable]
        public abstract class AbstractAxisFromToBuilder : AbstractBuilder
        {
            [SerializeField]
            protected Axis axis;
#if FlowEnt_3_Nullables
            [SerializeField]
            protected SerializableNullable<float> from;
#else
            [SerializeField]
            protected float from;
#endif

            [SerializeField]
            protected float to;
        }

        protected AbstractAxisMotion(TItem item, Axis axis, float value) : base(item, GetValue(value, axis))
        {
            this.axis = axis;
        }

        protected AbstractAxisMotion(TItem item, Axis axis, float? from, float to, Vector3? baseVector) : base(item,
            from == null ? null : GetValue(from.Value, axis, baseVector), GetValue(to, axis, baseVector))
        {
            this.axis = axis;
        }

        private readonly Axis axis;
        private Vector3 cache;
        protected abstract Vector3 Target { get; set; }

        private static Vector3 GetValue(float value, Axis axis, Vector3? baseVector = null)
        {
            Vector3 valueVector = baseVector ?? Vector3.zero;

            if ((axis & Axis.X) != 0)
            {
                valueVector.x = value;
            }

            if ((axis & Axis.Y) != 0)
            {
                valueVector.y = value;
            }

            if ((axis & Axis.Z) != 0)
            {
                valueVector.z = value;
            }

            return valueVector;
        }

        protected override void SetValue(Vector3 value)
        {
            cache = Target;

            if ((axis & Axis.X) != 0)
            {
                cache.x = value.x;
            }

            if ((axis & Axis.Y) != 0)
            {
                cache.y = value.y;
            }

            if ((axis & Axis.Z) != 0)
            {
                cache.z = value.z;
            }

            Target = cache;
        }
    }
}