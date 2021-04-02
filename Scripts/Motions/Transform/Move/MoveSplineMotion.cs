using UnityEngine;

namespace FlowEnt
{
    public class MoveSplineMotion : AbstractMotion<Transform>
    {
        public MoveSplineMotion(Transform item, ISpline spline) : base(item)
        {
            Spline = spline;
        }

        public ISpline Spline { get; }

        public override void OnStart()
        {
        }

        public override void OnUpdate(float t)
        {
            Item.position = Spline.GetPoint(t);
        }

        public override void OnComplete()
        {
        }
    }
}
