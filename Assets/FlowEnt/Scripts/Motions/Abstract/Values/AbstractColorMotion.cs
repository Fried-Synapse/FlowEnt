using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Abstract
{
    public abstract class AbstractColorMotion<TItem> : AbstractMotion<TItem, Color>
    {
        protected AbstractColorMotion(TItem item, Color value) : base(item, value)
        {
        }

        protected AbstractColorMotion(TItem item, Color? from, Color to) : base(item, from, to)
        {
        }

        protected override Func<Color, Color, float, Color> LerpFunction => Color.LerpUnclamped;
        protected override Color GetTo(Color from, Color value) => from + value;
    }
}