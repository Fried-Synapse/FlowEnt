using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    /// <summary>
    /// Lerps a <see cref="float" /> value.
    /// </summary>
    public class FloatValueMotion : AbstractValueMotion<float>
    {
        [Serializable]
        public class Builder : AbstractValueMotionBuilder
        {
            public override ITweenMotion Build()
                => new FloatValueMotion(from, to, GetCallback());
        }

        public FloatValueMotion(float from, float to, Action<float> onUpdated) : base(from, to, onUpdated)
        {
        }

        protected override Func<float, float, float, float> LerpFunction => Mathf.LerpUnclamped;
    }
}