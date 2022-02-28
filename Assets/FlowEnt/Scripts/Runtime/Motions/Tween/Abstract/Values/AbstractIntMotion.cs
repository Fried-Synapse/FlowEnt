using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractIntMotion<TItem> : AbstractValueMotion<TItem, int>
        where TItem : class
    {
        protected AbstractIntMotion(TItem item, int value) : base(item, value)
        {
        }

        protected AbstractIntMotion(TItem item, int? from, int to) : base(item, from, to)
        {
        }

        protected override Func<int, int, float, int> LerpFunction => (int a, int b, float t) => (int)Mathf.LerpUnclamped(a, b, t);
        protected override int GetTo(int from, int value) => from + value;
    }

    [Serializable]
    public abstract class AbstractIntValueMotionBuilder<TItem> : AbstractValueValueMotionBuilder<TItem, int>
        where TItem : class
    {
    }

    [Serializable]
    public abstract class AbstractIntFromToMotionBuilder<TItem> : AbstractValueFromToMotionBuilder<TItem, int>
        where TItem : class
    {
    }
}