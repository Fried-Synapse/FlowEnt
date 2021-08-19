using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public class MoveToSplineMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveToSplineMotion(TTransform item, ISpline spline) : base(item)
        {
            this.spline = spline;
        }

        private ISpline spline;

        public override void OnUpdate(float t)
        {
            item.position = spline.GetPoint(t);
        }
    }
}
