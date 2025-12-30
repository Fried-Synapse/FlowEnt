using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractVector2Motion<TItem> : AbstractStructValueMotion<TItem, Vector2>
    {
        protected AbstractVector2Motion(TItem item, Vector2 value) : base(item, value)
        {
        }

        protected AbstractVector2Motion(TItem item, Vector2? from, Vector2 to) : base(item, from, to)
        {
        }

        protected override Func<Vector2, Vector2, float, Vector2> LerpFunction => Vector2.LerpUnclamped;
        protected override Vector2 GetTo(Vector2 from, Vector2 value) => from + value;
    }
}