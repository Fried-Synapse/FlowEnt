using System;
using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Values
{
    /// <summary>
    /// Lerps a <see cref="Vector3" /> value.
    /// </summary>
    public class Vector3ValueMotion : AbstractValueMotion<Vector3>
    {
        public Vector3ValueMotion(Vector3 from, Vector3 to, Action<Vector3> onUpdated) : base(from, to, onUpdated)
        {
        }

        protected override Func<Vector3, Vector3, float, Vector3> LerpFunction => Vector3.LerpUnclamped;
    }
}