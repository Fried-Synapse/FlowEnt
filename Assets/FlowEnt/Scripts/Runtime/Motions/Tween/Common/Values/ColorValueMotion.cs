using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    public class ColorValueMotion : AbstractValueMotion<Color>
    {
        [Serializable]
        public class Builder : AbstractBuilder
        {
            public override ITweenMotion Build()
                => new ColorValueMotion(from, to, GetCallback());
        }

        /// <summary>
        /// Lerps a <see cref="Color" /> value.
        /// </summary>
        public ColorValueMotion(Color from, Color to, Action<Color> callback) : base(from, to, callback)
        {
        }

        protected override Func<Color, Color, float, Color> LerpFunction => Color.LerpUnclamped;
    }
}