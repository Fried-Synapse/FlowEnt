using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractColorGradientMotion<TItem> : AbstractTweenMotion<TItem>
        where TItem : class
    {
        [Serializable]
        public abstract class AbstractGradientBuilder : AbstractBuilder
        {
            [SerializeField]
            protected Gradient gradient;
        }

        protected AbstractColorGradientMotion(TItem item, Gradient gradient) : base(item)
        {
            this.gradient = gradient;
        }

        protected readonly Gradient gradient;
    }
}
