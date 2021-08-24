using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Values
{
    public class QuaternionValueMotion : AbstractValueMotion<Quaternion>
    {
        public QuaternionValueMotion(Quaternion from, Quaternion to, Action<Quaternion> onUpdated) : base(from, to, onUpdated)
        {
        }

        protected override Func<Quaternion, Quaternion, float, Quaternion> LerpFunction => Quaternion.LerpUnclamped;
    }
}