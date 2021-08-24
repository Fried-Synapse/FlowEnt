using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Values
{
    public class FloatValueMotion : AbstractValueMotion<float>
    {
        public FloatValueMotion(float from, float to, Action<float> onUpdated) : base(from, to, onUpdated)
        {
        }

        protected override Func<float, float, float, float> LerpFunction => Mathf.LerpUnclamped;
    }
}