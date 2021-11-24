using System;

namespace FriedSynapse.FlowEnt.Motions.Values
{
    public abstract class AbstractValueMotion<T> : AbstractMotion
    {
        protected AbstractValueMotion(T from, T to, Action<T> onUpdated)
        {
            this.from = from;
            this.to = to;
            this.onUpdated = onUpdated;
            lerpFunction = LerpFunction;
        }

        private readonly T from;
        private readonly T to;
        private readonly Action<T> onUpdated;
        private readonly Func<T, T, float, T> lerpFunction;
        protected abstract Func<T, T, float, T> LerpFunction { get; }

        public override void OnUpdate(float t)
        {
            onUpdated(lerpFunction(from, to, t));
        }
    }
}