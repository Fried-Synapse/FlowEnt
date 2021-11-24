using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Values
{
    public class ColorValueMotion : AbstractValueMotion<Color>
    {
        /// <summary>
        /// Lerps a <see cref="Color" /> value.
        /// </summary>
        public ColorValueMotion(Color from, Color to, Action<Color> callback) : base(from, to, callback)
        {
        }

        protected override Func<Color, Color, float, Color> LerpFunction => Color.LerpUnclamped;
    }
}