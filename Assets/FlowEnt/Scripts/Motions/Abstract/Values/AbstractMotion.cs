using System;

namespace FriedSynapse.FlowEnt.Motions.Abstract
{
    public abstract class AbstractMotion<TItem, TValue> : AbstractMotion<TItem>
           where TValue : struct
    {
        protected AbstractMotion(TItem item, TValue value) : base(item)
        {
            this.value = value;
            lerpFunction = LerpFunction;
        }

        protected AbstractMotion(TItem item, TValue? from, TValue to) : base(item)
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
}