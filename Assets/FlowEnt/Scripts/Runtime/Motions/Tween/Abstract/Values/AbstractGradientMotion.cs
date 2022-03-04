using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractGradientMotion<TItem> : AbstractTweenMotion<TItem>
        where TItem : class
    {
        [Serializable]
        public abstract class AbstractGradientBuilder : AbstractBuilder
        {
            [SerializeField]
            protected Gradient gradient;
        }

        protected AbstractGradientMotion(TItem item, Gradient gradient) : base(item)
        {
            this.gradient = gradient;
        }

        protected readonly Gradient gradient;
    }
}
