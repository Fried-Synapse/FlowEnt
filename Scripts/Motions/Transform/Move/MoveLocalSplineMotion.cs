using UnityEngine;

namespace FlowEnt
{
    public class MoveLocalSplineMotion : AbstractMotion<Transform>
    {
        public MoveLocalSplineMotion(Transform item, ISpline spline) : base(item)
        {
            Spline = spline;
        }

        public ISpline Spline { get; }

        public override void OnStart()
        {
        }

        public override void OnUpdate(float t)
        {
            Item.localPosition = Spline.GetPoint(t);
        }

        public override void OnComplete()
        {
        }
    }
}
