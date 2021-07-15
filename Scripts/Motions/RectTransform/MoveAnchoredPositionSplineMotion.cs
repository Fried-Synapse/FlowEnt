using UnityEngine;

namespace FlowEnt.Motions.RectTransformMotions
{
    public class MoveAnchoredPositionSplineMotion : AbstractMotion<RectTransform>
    {
        public MoveAnchoredPositionSplineMotion(RectTransform item, ISpline spline) : base(item)
        {
            Spline = spline;
        }

        public ISpline Spline { get; }

        public override void OnUpdate(float t)
        {
            Item.anchoredPosition = Spline.GetPoint(t);
        }

    }
}