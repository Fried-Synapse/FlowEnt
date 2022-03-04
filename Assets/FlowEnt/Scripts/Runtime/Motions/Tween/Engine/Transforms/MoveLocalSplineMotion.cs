using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localPosition" /> value using a spline.
    /// </summary>
    public class MoveLocalSplineMotion : AbstractSplineMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractSplineBuilder
        {
            public override ITweenMotion Build()
                => new MoveLocalSplineMotion(item, GetSpline());
        }

        public MoveLocalSplineMotion(Transform item, ISpline spline) : base(item, spline)
        {
        }

        public override void OnUpdate(float t)
        {
            item.localPosition = spline.GetPoint(t);
        }
    }
}
