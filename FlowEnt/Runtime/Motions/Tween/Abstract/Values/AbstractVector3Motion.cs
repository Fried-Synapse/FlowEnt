using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractVector3Motion<TItem> : AbstractStructValueMotion<TItem, Vector3>
    {
        protected AbstractVector3Motion(TItem item, Vector3 value) : base(item, value)
        {
        }

        protected AbstractVector3Motion(TItem item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Func<Vector3, Vector3, float, Vector3> LerpFunction => Vector3.LerpUnclamped;
        protected override Vector3 GetTo(Vector3 from, Vector3 value) => from + value;
    }
}