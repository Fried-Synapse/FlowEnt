using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public struct LinearColor
    {
        public LinearColor(Color start, Color end)
        {
            this.start = start;
            this.end = end;
        }

        [SerializeField]
        private Color start;
        public Color Start { get => start; set => start = value; }
        [SerializeField]
        private Color end;
        public Color End { get => end; set => end = value; }

        public static LinearColor LerpUnclamped(LinearColor a, LinearColor b, float t)
            => new LinearColor(Color.LerpUnclamped(a.start, b.start, t), Color.LerpUnclamped(a.end, b.end, t));

        public static LinearColor operator +(LinearColor a, LinearColor b)
            => new LinearColor(a.start + b.start, a.end + b.end);
    }
}
