using System;
using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Values
{
    public class Vector4ValueMotion : AbstractValueMotion<Vector4>
    {
        /// <summary>
        /// Lerps a <see cref="Vector4" /> value.
        /// </summary>
        public Vector4ValueMotion(Vector4 from, Vector4 to, Action<Vector4> onUpdated) : base(from, to, onUpdated)
        {
        }

        protected override Func<Vector4, Vector4, float, Vector4> LerpFunction => Vector4.LerpUnclamped;
    }
}