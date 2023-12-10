using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;
using UnityEngine.Events;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    public abstract class AbstractEventMotion<T> : AbstractTweenMotion
    {
        [Serializable]
        public abstract class AbstractEventMotionBuilder : AbstractTweenMotionBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private UnityEvent<T> callback;
#pragma warning restore IDE0044, RCS1169

            protected Action<T> GetCallback()
                => t => callback.Invoke(t);
        }

        protected AbstractEventMotion(Action<T> onUpdated)
        {
            this.onUpdated = onUpdated;
        }

        protected readonly Action<T> onUpdated;
    }

    public abstract class AbstractValueMotion<T> : AbstractEventMotion<T>
        where T : struct
    {
        [Serializable]
        public abstract class AbstractValueMotionBuilder : AbstractEventMotionBuilder
        {
            [SerializeField]
            protected T from;

            [SerializeField]
            protected T to;
        }

        protected AbstractValueMotion(T from, T to, Action<T> onUpdated) : base(onUpdated)
        {
            this.from = from;
            this.to = to;
            lerpFunction = LerpFunction;
        }

        private readonly T from;
        private readonly T to;
        private readonly Func<T, T, float, T> lerpFunction;
        protected abstract Func<T, T, float, T> LerpFunction { get; }

        public override void OnUpdate(float t)
        {
            onUpdated(lerpFunction(from, to, t));
        }
    }
}