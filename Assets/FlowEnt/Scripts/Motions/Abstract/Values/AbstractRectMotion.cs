using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Abstract
{
    public abstract class AbstractRectMotion<TItem> : AbstractMotion<TItem, Rect>
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
}