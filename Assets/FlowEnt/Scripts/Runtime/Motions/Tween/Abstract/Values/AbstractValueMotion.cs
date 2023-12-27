using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractStructValueMotion<TItem, TValue> : AbstractValueMotion<TItem, TValue>
        where TValue : struct
    {
        [Serializable]
        public new abstract class AbstractFromToBuilder<T> : AbstractValueMotion<TItem, TValue>.AbstractFromToBuilder<T>
            where T : struct
        {
            protected T? From => hasFromValue ? from : null;
        }

        [Serializable]
        public new abstract class AbstractFromToBuilder : AbstractFromToBuilder<TValue>
        {
        }

        protected AbstractStructValueMotion(TItem item, TValue value) : base(item, value)
        {
        }

        protected AbstractStructValueMotion(TItem item, TValue? from, TValue to) : base(item, from != null,
            from ?? default, to)
        {
        }
    }

    public abstract class AbstractClassValueMotion<TItem, TValue> : AbstractValueMotion<TItem, TValue>
        where TValue : class
    {
        [Serializable]
        public new abstract class AbstractFromToBuilder<T> : AbstractValueMotion<TItem, TValue>.AbstractFromToBuilder<T>
            where T : class
        {
            protected T From => hasFromValue ? from : null;
        }

        [Serializable]
        public new abstract class AbstractFromToBuilder : AbstractFromToBuilder<TValue>
        {
        }

        protected AbstractClassValueMotion(TItem item, TValue value) : base(item, value)
        {
        }

        protected AbstractClassValueMotion(TItem item, TValue from, TValue to) : base(item, from != null,
            from, to)
        {
        }
    }

    public static class TooltipConst
    {
        public const string Temp = "If this is disabled the motion will start from the current state";
    }

    public abstract class AbstractValueMotion<TItem, TValue> : AbstractTweenMotion<TItem>
    {
        [Serializable]
        public abstract class AbstractValueBuilder<T> : AbstractBuilder
        {
            [SerializeField]
            protected T value;
        }

        [Serializable]
        public abstract class AbstractFromToBuilder<T> : AbstractBuilder
        {
            [SerializeField]
            [Tooltip(TooltipConst.Temp)]
            protected bool hasFromValue = true;

            [SerializeField]
            [EnableIf(nameof(hasFromValue), true)]
            protected T from;

            [SerializeField]
            protected T to;
        }

        [Serializable]
        public abstract class AbstractValueBuilder : AbstractValueBuilder<TValue>
        {
        }

        [Serializable]
        public abstract class AbstractFromToBuilder : AbstractFromToBuilder<TValue>
        {
        }

        protected AbstractValueMotion(TItem item, TValue value) : base(item)
        {
            this.value = value;
            lerpFunction = LerpFunction;
        }

        protected AbstractValueMotion(TItem item, bool hasFrom, TValue from, TValue to) : base(item)
        {
            this.hasFrom = hasFrom;
            if (hasFrom)
            {
                this.from = from;
            }

            hasTo = true;
            this.to = to;
            lerpFunction = LerpFunction;
        }

        private readonly bool hasFrom;
        private readonly bool hasTo;
        private readonly TValue value;
        private TValue from;
        private TValue to;
        private readonly Func<TValue, TValue, float, TValue> lerpFunction;
        protected abstract Func<TValue, TValue, float, TValue> LerpFunction { get; }
        protected abstract TValue GetFrom();
        protected abstract TValue GetTo(TValue from, TValue value);
        protected abstract void SetValue(TValue value);

        public override void OnStart()
        {
            if (!hasFrom)
            {
                from = GetFrom();
            }

            if (!hasTo)
            {
                to = GetTo(from, value);
            }
        }

        public override void OnUpdate(float t)
        {
            SetValue(lerpFunction(from, to, t));
        }
    }
}