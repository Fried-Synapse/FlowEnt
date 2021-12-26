using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Abstract
{
    public abstract class AbstractSplineMotion<TItem> : AbstractMotion<TItem>
    {
        protected AbstractSplineMotion(TItem item, ISpline spline) : base(item)
        {
            this.spline = spline;
        }

        protected readonly ISpline spline;
    }
}
