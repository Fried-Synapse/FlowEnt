using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractRectMotion<TItem> : AbstractValueMotion<TItem, Rect>
        where TItem : class
    {
        protected AbstractRectMotion(TItem item, Rect value) : base(item, value)
        {
        }

        protected AbstractRectMotion(TItem item, Rect? from, Rect to) : base(item, from, to)
        {
        }

        protected override Func<Rect, Rect, float, Rect> LerpFunction => throw new NotImplementedException();
        protected override Rect GetTo(Rect from, Rect value) => throw new NotImplementedException();
    }

    [Serializable]
    public abstract class AbstractRectValueMotionBuilder<TItem> : AbstractValueValueMotionBuilder<TItem, Rect>
        where TItem : class
    {
    }

    [Serializable]
    public abstract class AbstractRectFromToMotionBuilder<TItem> : AbstractValueFromToMotionBuilder<TItem, Rect>
        where TItem : class
    {
    }
}