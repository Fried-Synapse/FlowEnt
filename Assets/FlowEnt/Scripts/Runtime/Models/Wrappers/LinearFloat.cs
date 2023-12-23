using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public struct LinearFloat
    {
        public LinearFloat(float start, float end)
        {
            this.start = start;
            this.end = end;
        }

        [SerializeField]
        private float start;

        public float Start { get => start; set => start = value; }

        [SerializeField]
        private float end;

        public float End { get => end; set => end = value; }

        public static LinearFloat LerpUnclamped(LinearFloat a, LinearFloat b, float t)
            => new(Mathf.LerpUnclamped(a.start, b.start, t), Mathf.LerpUnclamped(a.end, b.end, t));

        public static LinearFloat operator +(LinearFloat a, LinearFloat b)
            => new(a.start + b.start, a.end + b.end);
    }
}