using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractGradientMotion<TItem> : AbstractTweenMotion<TItem>
        where TItem : class
    {
        protected AbstractGradientMotion(TItem item, Gradient gradient) : base(item)
        {
            this.gradient = gradient;
        }

        protected readonly Gradient gradient;
    }
}
