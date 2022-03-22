using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractGradientMotion<TItem> : AbstractClassValueMotion<TItem, Gradient>
        where TItem : class
    {
        protected AbstractGradientMotion(TItem item, Gradient value) : base(item, value)
        {
        }

        protected AbstractGradientMotion(TItem item, Gradient from, Gradient to) : base(item, from, to)
        {
        }

        private readonly GradientOperation lerper = new GradientOperation();

        protected override Func<Gradient, Gradient, float, Gradient> LerpFunction => lerper.LerpUnclamped;
        protected override Gradient GetTo(Gradient from, Gradient value) => new GradientOperation().Addition(from, value);
    }
}