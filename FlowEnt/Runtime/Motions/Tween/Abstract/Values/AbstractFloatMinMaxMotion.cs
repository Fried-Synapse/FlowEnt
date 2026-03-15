using System;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractFloatMinMaxMotion<TItem> : AbstractStructValueMotion<TItem, MinMaxVector2>
    {
        protected AbstractFloatMinMaxMotion(TItem item, MinMaxVector2 value) : base(item, value)
        {
        }

        protected AbstractFloatMinMaxMotion(TItem item, MinMaxVector2? from, MinMaxVector2 to) : base(item, from, to)
        {
        }

        protected override Func<MinMaxVector2, MinMaxVector2, float, MinMaxVector2> LerpFunction => MinMaxVector2.LerpUnclamped;
        protected override MinMaxVector2 GetTo(MinMaxVector2 from, MinMaxVector2 value) => from + value;
    }
}