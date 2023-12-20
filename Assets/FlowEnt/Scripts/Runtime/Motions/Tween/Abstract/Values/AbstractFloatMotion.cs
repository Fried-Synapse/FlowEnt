using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractFloatMotion<TItem> : AbstractStructValueMotion<TItem, float>
    {
        protected AbstractFloatMotion(TItem item, float value) : base(item, value)
        {
        }

        protected AbstractFloatMotion(TItem item, float? from, float to) : base(item, from, to)
        {
        }

        protected override Func<float, float, float, float> LerpFunction => Mathf.LerpUnclamped;
        protected override float GetTo(float from, float value) => from + value;
    }
}