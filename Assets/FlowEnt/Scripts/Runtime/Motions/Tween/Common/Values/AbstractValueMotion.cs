using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;
using UnityEngine.Events;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    public abstract class AbstractValueMotion<T> : AbstractTweenMotion
    {
        [Serializable]
        public abstract class AbstractEventBuilder : AbstractTweenMotionBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private UnityEvent<T> callback;
#pragma warning restore IDE0044, RCS1169

            protected Action<T> GetCallback()
                => t => callback.Invoke(t);
        }

        [Serializable]
        public abstract class AbstractBuilder : AbstractEventBuilder
        {
            [SerializeField]
            protected T from;
            [SerializeField]
            protected T to;
        }

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