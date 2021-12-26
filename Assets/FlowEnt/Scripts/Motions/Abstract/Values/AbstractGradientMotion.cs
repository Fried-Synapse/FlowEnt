using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Abstract
{
    public abstract class AbstractGradientMotion<TItem> : AbstractMotion<TItem>
    {
        protected AbstractGradientMotion(TItem item, Gradient gradient) : base(item)
        {
            this.gradient = gradient;
        }

        protected readonly Gradient gradient;
    }
}
