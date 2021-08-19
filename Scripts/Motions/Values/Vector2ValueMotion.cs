using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Values
{
    public class Vector2ValueMotion : AbstractValueMotion<Vector2>
    {
        public Vector2ValueMotion(Vector2 from, Vector2 to, Action<Vector2> onUpdated) : base(from, to, onUpdated)
        {
        }

        protected override Func<Vector2, Vector2, float, Vector2> LerpFunction => Vector2.Lerp;
    }
}