using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    public abstract class AbstractValueMotion<T> : AbstractTweenMotion
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