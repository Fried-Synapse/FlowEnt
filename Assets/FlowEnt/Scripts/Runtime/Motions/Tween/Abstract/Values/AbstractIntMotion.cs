using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractIntMotion<TItem> : AbstractStructValueMotion<TItem, int>
    {
        protected AbstractIntMotion(TItem item, int value) : base(item, value)
        {
        }

        protected AbstractIntMotion(TItem item, int? from, int to) : base(item, from, to)
        {
        }

        protected override Func<int, int, float, int> LerpFunction => (a, b, t) => (int)Mathf.LerpUnclamped(a, b, t);
        protected override int GetTo(int from, int value) => from + value;
    }
}