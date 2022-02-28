using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.position" /> value using a spline.
    /// </summary>
    public class MoveSplineMotion : AbstractSplineMotion<Transform>
    {
        public MoveSplineMotion(Transform item, ISpline spline) : base(item, spline)
        {
        }

        public override void OnUpdate(float t)
        {
            item.position = spline.GetPoint(t);
        }
    }

    [Serializable]
    public class MoveSplineMotionBuilder : AbstractSplineMotionBuilder<Transform>
    {
        public override ITweenMotion Build()
            => new MoveSplineMotion(item, GetSpline());
    }
}
