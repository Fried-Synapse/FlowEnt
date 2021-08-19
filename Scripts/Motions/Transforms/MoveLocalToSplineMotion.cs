using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public class MoveLocalToSplineMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveLocalToSplineMotion(TTransform item, ISpline spline) : base(item)
        {
            this.spline = spline;
        }

        private ISpline spline;

        public override void OnUpdate(float t)
        {
            item.localPosition = spline.GetPoint(t);
        }
    }
}
