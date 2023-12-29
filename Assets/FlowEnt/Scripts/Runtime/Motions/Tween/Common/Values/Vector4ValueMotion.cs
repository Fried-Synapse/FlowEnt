using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    public class Vector4ValueMotion : AbstractValueMotion<Vector4>
    {
        [Serializable]
        public class Builder : AbstractValueMotionBuilder
        {
            public override AbstractTweenMotion Build()
                => new Vector4ValueMotion(from, to, GetCallback());
        }

        /// <summary>
        /// Lerps a <see cref="Vector4" /> value.
        /// </summary>
        public Vector4ValueMotion(Vector4 from, Vector4 to, Action<Vector4> onUpdated) : base(from, to, onUpdated)
        {
        }

        protected override Func<Vector4, Vector4, float, Vector4> LerpFunction => Vector4.LerpUnclamped;
    }
}