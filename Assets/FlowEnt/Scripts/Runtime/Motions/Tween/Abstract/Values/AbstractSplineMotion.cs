using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractSplineMotion<TItem> : AbstractTweenMotion<TItem>
        where TItem : class
    {
        protected AbstractSplineMotion(TItem item, ISpline spline) : base(item)
        {
            this.spline = spline;
        }

        protected readonly ISpline spline;
    }
}
