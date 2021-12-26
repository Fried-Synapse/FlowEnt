using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localPosition" /> value using a spline.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class MoveLocalSplineMotion<TTransform> : AbstractSplineMotion<TTransform>
        where TTransform : Transform
    {
        public MoveLocalSplineMotion(TTransform item, ISpline spline) : base(item, spline)
        {
        }

        public override void OnUpdate(float t)
        {
            item.localPosition = spline.GetPoint(t);
        }
    }
}
