using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractQuaternionMotion<TItem> : AbstractStructValueMotion<TItem, Quaternion>
        where TItem : class
    {
        protected AbstractQuaternionMotion(TItem item, Quaternion value) : base(item, value)
        {
        }

        protected AbstractQuaternionMotion(TItem item, Quaternion? from, Quaternion to) : base(item, from, to)
        {
        }

        protected override Func<Quaternion, Quaternion, float, Quaternion> LerpFunction => Quaternion.LerpUnclamped;
        protected override Quaternion GetTo(Quaternion from, Quaternion value) => from * value;
    }
}