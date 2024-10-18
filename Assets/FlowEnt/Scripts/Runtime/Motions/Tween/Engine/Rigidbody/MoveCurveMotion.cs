using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="Rigidbody.position" /> value using a curve.
    /// </summary>
    public class MoveCurveMotion : AbstractCurveMotion<Rigidbody>
    {
        [Serializable]
        public class Builder : AbstractBuilder
        {
            public override AbstractTweenMotion Build()
                => new MoveCurveMotion(item, curve.Build());
        }

        public MoveCurveMotion(Rigidbody item, ICurve curve) : base(item, curve)
        {
        }

        public override void OnUpdate(float t)
        {
            item.MovePosition(curve.GetPoint(t));
        }
    }
}