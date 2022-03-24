using System;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractFloatLinearMotion<TItem> : AbstractClassValueMotion<TItem, LinearFloat>
        where TItem : class
    {
        protected AbstractFloatLinearMotion(TItem item, LinearFloat value) : base(item, value)
        {
        }

        protected AbstractFloatLinearMotion(TItem item, LinearFloat from, LinearFloat to) : base(item, from, to)
        {
        }

        protected override Func<LinearFloat, LinearFloat, float, LinearFloat> LerpFunction => LinearFloat.LerpUnclamped;
        protected override LinearFloat GetTo(LinearFloat from, LinearFloat value) => from + value;
    }
}