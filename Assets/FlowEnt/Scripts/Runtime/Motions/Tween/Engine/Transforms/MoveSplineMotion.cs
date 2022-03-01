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
        [Serializable]
        public class Builder : AbstractBuilder
        {
            public override ITweenMotion Build()
                => new MoveSplineMotion(item, GetSpline());
        }

        public MoveSplineMotion(Transform item, ISpline spline) : base(item, spline)
        {
        }

        public override void OnUpdate(float t)
        {
            item.position = spline.GetPoint(t);
        }
    }
}
