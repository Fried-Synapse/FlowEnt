using System;
using UnityEngine;

namespace FlowEnt.Motions.Values
{
    public class Vector4ValueMotion : AbstractValueMotion<Vector4>
    {
        public Vector4ValueMotion(Vector4 from, Vector4 to, Action<Vector4> onUpdated) : base(from, to, onUpdated)
        {
        }

        protected override Func<Vector4, Vector4, float, Vector4> LerpFunction => Vector4.Lerp;
    }
}