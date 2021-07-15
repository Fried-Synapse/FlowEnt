using UnityEngine;

namespace FlowEnt.Motions.TransformMotions
{
    public class MoveLocalToSplineMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveLocalToSplineMotion(TTransform item, ISpline spline) : base(item)
        {
            Spline = spline;
        }

        public ISpline Spline { get; }

        public override void OnUpdate(float t)
        {
            Item.localPosition = Spline.GetPoint(t);
        }
    }
}
