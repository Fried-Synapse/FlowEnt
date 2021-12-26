using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.UI.RectTransforms
{
    /// <summary>
    /// Lerps the <see cref="RectTransform.anchoredPosition" /> value using a spline.
    /// </summary>
    public class MoveAnchoredPositionSplineMotion : AbstractSplineMotion<RectTransform>
    {
        public MoveAnchoredPositionSplineMotion(RectTransform item, ISpline spline) : base(item, spline)
        {
        }

        public override void OnUpdate(float t)
        {
            item.anchoredPosition = spline.GetPoint(t);
        }
    }
}