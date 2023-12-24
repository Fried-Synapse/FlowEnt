using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractColorLinearMotion<TItem> : AbstractStructValueMotion<TItem, LinearColor>
    {
        [Serializable]
        public new abstract class
            AbstractValueBuilder : AbstractStructValueMotion<TItem, LinearColor>.AbstractValueBuilder
        {
            protected AbstractValueBuilder()
            {
                value = new(Color.white, Color.white);
            }
        }

        [Serializable]
        public new abstract class
            AbstractFromToBuilder : AbstractStructValueMotion<TItem, LinearColor>.AbstractFromToBuilder
        {
            protected AbstractFromToBuilder()
            {
                from = new(Color.white, Color.white);
                to = new(Color.white, Color.white);
            }
        }

        protected AbstractColorLinearMotion(TItem item, LinearColor value) : base(item, value)
        {
        }

        protected AbstractColorLinearMotion(TItem item, LinearColor? from, LinearColor to) : base(item, from, to)
        {
        }

        protected override Func<LinearColor, LinearColor, float, LinearColor> LerpFunction => LinearColor.LerpUnclamped;
        protected override LinearColor GetTo(LinearColor from, LinearColor value) => from + value;
    }
}