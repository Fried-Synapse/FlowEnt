using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    /// <summary>
    /// Lerps a <see cref="Vector3" /> value.
    /// </summary>
    public class Vector3ValueMotion : AbstractValueMotion<Vector3>
    {
        [Serializable]
        public class Builder : AbstractValueMotionBuilder
        {
            public override AbstractTweenMotion Build()
                => new Vector3ValueMotion(from, to, GetCallback());
        }

        public Vector3ValueMotion(Vector3 from, Vector3 to, Action<Vector3> onUpdated) : base(from, to, onUpdated)
        {
        }

        protected override Func<Vector3, Vector3, float, Vector3> LerpFunction => Vector3.LerpUnclamped;
    }
}