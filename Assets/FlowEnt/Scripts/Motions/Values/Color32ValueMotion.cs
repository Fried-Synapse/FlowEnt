using System;
using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Values
{
    public class Color32ValueMotion : AbstractValueMotion<Color32>
    {
        /// <summary>
        /// Lerps a <see cref="Color32" /> value.
        /// </summary>
        public Color32ValueMotion(Color32 from, Color32 to, Action<Color32> callback) : base(from, to, callback)
        {
        }

        protected override Func<Color32, Color32, float, Color32> LerpFunction => Color32.LerpUnclamped;
    }
}