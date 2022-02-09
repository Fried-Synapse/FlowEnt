using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="Rigidbody.position" /> value using a spline.
    /// </summary>
    /// <typeparam name="TRigidbody"></typeparam>
    public class MoveSplineMotion<TRigidbody> : AbstractSplineMotion<TRigidbody>
        where TRigidbody : Rigidbody
    {
        public MoveSplineMotion(TRigidbody item, ISpline spline) : base(item, spline)
        {
        }

        public override void OnUpdate(float t)
        {
            item.position = spline.GetPoint(t);
        }
    }
}
