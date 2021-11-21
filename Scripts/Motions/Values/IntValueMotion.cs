using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Values
{
    /// <summary>
    /// Lerps an <see cref="int" /> value.
    /// </summary>
    public class IntValueMotion : AbstractMotion
    {
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