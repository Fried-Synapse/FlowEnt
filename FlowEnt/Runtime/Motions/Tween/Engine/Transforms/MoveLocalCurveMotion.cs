using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localPosition" /> value using a spline.
    /// </summary>
    public class MoveLocalCurveMotion : AbstractCurveMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractBuilder
        {
            public override AbstractTweenMotion Build()
                => new MoveLocalCurveMotion(item, curve.Build());

#if UNITY_EDITOR
            private protected override Transform GizmoTransform => item;
#endif
        }

        public MoveLocalCurveMotion(Transform item, ICurve curve) : base(item, curve)
        {
        }

        public override void OnUpdate(float t)
        {
            item.localPosition = curve.GetPoint(t);
        }
    }
}