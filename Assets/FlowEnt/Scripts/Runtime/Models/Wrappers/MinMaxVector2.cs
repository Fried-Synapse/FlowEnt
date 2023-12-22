using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public struct MinMaxVector2
    {
        public MinMaxVector2(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }

        [SerializeField]
        private Vector2 min;

        public Vector2 Min { get => min; set => min = value; }

        [SerializeField]
        private Vector2 max;

        public Vector2 Max { get => max; set => max = value; }

        public static MinMaxVector2 LerpUnclamped(MinMaxVector2 a, MinMaxVector2 b, float t)
            => new(Vector2.LerpUnclamped(a.min, b.min, t), Vector2.LerpUnclamped(a.max, b.max, t));

        public static MinMaxVector2 operator +(MinMaxVector2 a, MinMaxVector2 b)
            => new(a.min + b.min, a.max + b.max);
    }
}