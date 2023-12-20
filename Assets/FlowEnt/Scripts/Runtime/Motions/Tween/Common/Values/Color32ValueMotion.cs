using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    public class Color32ValueMotion : AbstractValueMotion<Color32>
    {
        [Serializable]
        public class Builder : AbstractValueMotionBuilder
        {
            public Builder()
            {
                from = Color.white;
                to = Color.white;
            }
            
            public override ITweenMotion Build()
                => new Color32ValueMotion(from, to, GetCallback());
        }

        /// <summary>
        /// Lerps a <see cref="Color32" /> value.
        /// </summary>
        public Color32ValueMotion(Color32 from, Color32 to, Action<Color32> callback) : base(from, to, callback)
        {
        }

        protected override Func<Color32, Color32, float, Color32> LerpFunction => Color32.LerpUnclamped;
    }
}