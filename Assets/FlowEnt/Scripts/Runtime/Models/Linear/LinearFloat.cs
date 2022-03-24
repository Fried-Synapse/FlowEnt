using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class LinearFloat : AbstractLinear<float>
    {
        public LinearFloat(float start, float end) : base(start, end)
        {
        }

        public static LinearFloat LerpUnclamped(LinearFloat a, LinearFloat b, float t)
            => new LinearFloat(Mathf.LerpUnclamped(a.start, b.start, t), Mathf.LerpUnclamped(a.end, b.end, t));

        public static LinearFloat operator +(LinearFloat a, LinearFloat b)
            => new LinearFloat(a.start + b.start, a.end + b.end);
    }
}
