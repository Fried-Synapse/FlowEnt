using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    public class QuaternionValueMotion : AbstractValueMotion<Quaternion>
    {
        /// <summary>
        /// Lerps a <see cref="Quaternion" /> value.
        /// </summary>
        public QuaternionValueMotion(Quaternion from, Quaternion to, Action<Quaternion> onUpdated) : base(from, to, onUpdated)
        {
        }

        protected override Func<Quaternion, Quaternion, float, Quaternion> LerpFunction => Quaternion.LerpUnclamped;
    }
}