using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.position" /> value using a spline.
    /// </summary>
    public class MoveCurveMotion : AbstractCurveMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractBuilder
        {
            public override AbstractTweenMotion Build()
                => new MoveCurveMotion(item, curve.Build());
        }

        public MoveCurveMotion(Transform item, ICurve curve) : base(item, curve)
        {
        }

        public override void OnUpdate(float t)
        {
            item.position = curve.GetPoint(t);
        }
    }
}
