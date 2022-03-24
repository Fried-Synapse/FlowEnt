using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class LinearColor : AbstractLinear<Color>
    {
        public LinearColor(Color start, Color end) : base(start, end)
        {
        }

        public static LinearColor LerpUnclamped(LinearColor a, LinearColor b, float t)
            => new LinearColor(Color.LerpUnclamped(a.start, b.start, t), Color.LerpUnclamped(a.end, b.end, t));

        public static LinearColor operator +(LinearColor a, LinearColor b)
            => new LinearColor(a.start + b.start, a.end + b.end);
    }
}
