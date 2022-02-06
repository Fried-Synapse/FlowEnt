using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.position" /> value using a spline.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class MoveSplineMotion<TTransform> : AbstractSplineMotion<TTransform>
        where TTransform : Transform
    {
        public MoveSplineMotion(TTransform item, ISpline spline) : base(item, spline)
        {
        }

        public override void OnUpdate(float t)
        {
            item.position = spline.GetPoint(t);
        }
    }
}
