using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractColorMotion<TItem> : AbstractStructValueMotion<TItem, Color>
    {
        [Serializable]
        public new abstract class AbstractValueBuilder : AbstractStructValueMotion<TItem, Color>.AbstractValueBuilder
        {
            protected AbstractValueBuilder()
            {
                value = Color.white;
            }
        }

        [Serializable]
        public new abstract class AbstractFromToBuilder : AbstractStructValueMotion<TItem, Color>.AbstractFromToBuilder
        {
            protected AbstractFromToBuilder()
            {
                from = Color.white;
                to = Color.white;
            }
        }

        protected AbstractColorMotion(TItem item, Color value) : base(item, value)
        {
        }

        protected AbstractColorMotion(TItem item, Color? from, Color to) :
            base(item, from, to)
        {
        }

        protected override Func<Color, Color, float, Color> LerpFunction => Color.LerpUnclamped;
        protected override Color GetTo(Color from, Color value) => from + value;
    }
}