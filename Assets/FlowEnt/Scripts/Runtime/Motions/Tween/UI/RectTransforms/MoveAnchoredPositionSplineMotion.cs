using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.UI.RectTransforms
{
    /// <summary>
    /// Lerps the <see cref="RectTransform.anchoredPosition" /> value using a spline.
    /// </summary>
    public class MoveAnchoredPositionSplineMotion : AbstractSplineMotion<RectTransform>
    {
        [Serializable]
        public class Builder : AbstractSplineBuilder
        {
            public override ITweenMotion Build()
                => new MoveAnchoredPositionSplineMotion(item, GetSpline());
        }
        
        public MoveAnchoredPositionSplineMotion(RectTransform item, ISpline spline) : base(item, spline)
        {
        }

        public override void OnUpdate(float t)
        {
            item.anchoredPosition = spline.GetPoint(t);
        }
    }
}