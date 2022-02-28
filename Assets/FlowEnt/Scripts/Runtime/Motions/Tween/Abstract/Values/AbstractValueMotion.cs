using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractValueMotion<TItem, TValue> : AbstractTweenMotion<TItem>
        where TItem : class
        where TValue : struct
    {
        protected AbstractValueMotion(TItem item, TValue value) : base(item)
        {
            this.value = value;
            lerpFunction = LerpFunction;
        }

        protected AbstractValueMotion(TItem item, TValue? from, TValue to) : base(item)
        {
            hasFrom = from != null;
            if (hasFrom)
            {
                this.from = from.Value;
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

    [Serializable]
    public abstract class AbstractValueValueMotionBuilder<TItem, TValue> : AbstractTweenMotionBuilder<TItem>
        where TItem : class
        where TValue : struct
    {
        [SerializeField]
        protected TValue value;
    }

    [Serializable]
    public abstract class AbstractValueFromToMotionBuilder<TItem, TValue> : AbstractTweenMotionBuilder<TItem>
        where TItem : class
        where TValue : struct
    {
        [SerializeField]
        protected TValue from;
        [SerializeField]
        protected TValue to;
    }
}