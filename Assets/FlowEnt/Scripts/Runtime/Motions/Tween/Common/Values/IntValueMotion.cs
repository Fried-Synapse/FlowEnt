using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    /// <summary>
    /// Lerps an <see cref="int" /> value.
    /// </summary>
    public class IntValueMotion : AbstractTweenMotion
    {
        [Serializable]
        public class Builder : AbstractValueMotion<int>.AbstractBuilder
        {
            public override ITweenMotion Build()
                => new IntValueMotion(from, to, GetCallback());
        }

        public IntValueMotion(int from, int to, Action<int> callback)
        {
            this.from = from;
            this.to = to;
            this.callback = callback;
        }

        private readonly int from;
        private readonly int to;
        private readonly Action<int> callback;

        public override void OnUpdate(float t)
        {
            callback((int)Mathf.LerpUnclamped(from, to, t));
        }
    }
}