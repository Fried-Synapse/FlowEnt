using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.UI.RectTransforms
{
    /// <summary>
    /// Lerps the <see cref="RectTransform.anchoredPosition" /> value using a spline.
    /// </summary>
    public class MoveAnchoredPositionCurveMotion : AbstractCurveMotion<RectTransform>
    {
        [Serializable]
        public class Builder : AbstractBuilder
        {
            public override ITweenMotion Build()
                => new MoveAnchoredPositionCurveMotion(item, curve.Build());
        }
        
        public MoveAnchoredPositionCurveMotion(RectTransform item, ICurve curve) : base(item, curve)
        {
        }

        public override void OnUpdate(float t)
        {
            item.anchoredPosition = curve.GetPoint(t);
        }
    }
}