using System.Runtime.CompilerServices;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractAlphaMotion<TItem> : AbstractFloatMotion<TItem>
        where TItem : class
    {
        protected AbstractAlphaMotion(TItem item, float value) : base(item, value)
        {
        }

        protected AbstractAlphaMotion(TItem item, float? from, float to) : base(item, from, to)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected Color SetAlpha(Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }
    }
}