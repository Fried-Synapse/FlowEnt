using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.RectTransforms
{
    public class MoveAnchoredPositionSplineMotion : AbstractMotion<RectTransform>
    {
        public MoveAnchoredPositionSplineMotion(RectTransform item, ISpline spline) : base(item)
        {
            this.spline = spline;
        }

        private readonly ISpline spline;

        public override void OnUpdate(float t)
        {
            item.anchoredPosition = spline.GetPoint(t);
        }
    }
}