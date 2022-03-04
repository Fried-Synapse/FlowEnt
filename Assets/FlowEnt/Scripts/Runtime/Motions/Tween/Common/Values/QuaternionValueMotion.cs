using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    public class QuaternionValueMotion : AbstractValueMotion<Quaternion>
    {
        [Serializable]
        public class Builder : AbstractBuilder
        {
            public override ITweenMotion Build()
                => new QuaternionValueMotion(from, to, GetCallback());
        }

        /// <summary>
        /// Lerps a <see cref="Quaternion" /> value.
        /// </summary>
        public QuaternionValueMotion(Quaternion from, Quaternion to, Action<Quaternion> onUpdated) : base(from, to, onUpdated)
        {
        }

        protected override Func<Quaternion, Quaternion, float, Quaternion> LerpFunction => Quaternion.LerpUnclamped;
    }
}