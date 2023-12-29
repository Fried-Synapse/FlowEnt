using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    /// <summary>
    /// Lerps a <see cref="Vector2" /> value.
    /// </summary>
    public class Vector2ValueMotion : AbstractValueMotion<Vector2>
    {
        [Serializable]
        public class Builder : AbstractValueMotionBuilder
        {
            public override AbstractTweenMotion Build()
                => new Vector2ValueMotion(from, to, GetCallback());
        }

        public Vector2ValueMotion(Vector2 from, Vector2 to, Action<Vector2> onUpdated) : base(from, to, onUpdated)
        {
        }

        protected override Func<Vector2, Vector2, float, Vector2> LerpFunction => Vector2.LerpUnclamped;
    }
}