using UnityEngine;

namespace FlowEnt.Motions.TransformMotions
{
    public class MoveToSplineMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveToSplineMotion(TTransform item, ISpline spline) : base(item)
        {
            Spline = spline;
        }

        public ISpline Spline { get; }

        public override void OnUpdate(float t)
        {
            Item.position = Spline.GetPoint(t);
        }
    }
}
