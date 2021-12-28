using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractVector4Motion<TItem> : AbstractValueMotion<TItem, Vector4>
        where TItem : class
    {
        protected AbstractVector4Motion(TItem item, Vector4 value) : base(item, value)
        {
        }

        protected AbstractVector4Motion(TItem item, Vector4? from, Vector4 to) : base(item, from, to)
        {
        }

        protected override Func<Vector4, Vector4, float, Vector4> LerpFunction => Vector4.LerpUnclamped;
        protected override Vector4 GetTo(Vector4 from, Vector4 value) => from + value;
    }
}