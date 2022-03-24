using System;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractColorLinearMotion<TItem> : AbstractClassValueMotion<TItem, LinearColor>
        where TItem : class
    {
        protected AbstractColorLinearMotion(TItem item, LinearColor value) : base(item, value)
        {
        }

        protected AbstractColorLinearMotion(TItem item, LinearColor from, LinearColor to) : base(item, from, to)
        {
        }

        protected override Func<LinearColor, LinearColor, float, LinearColor> LerpFunction => LinearColor.LerpUnclamped;
        protected override LinearColor GetTo(LinearColor from, LinearColor value) => from + value;
    }
}