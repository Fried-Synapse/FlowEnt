using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class RectHelper
    {
        public static Rect LerpUnclamped(Rect a, Rect b, float t)
            => new(Vector2.LerpUnclamped(a.position, b.position, t), Vector2.LerpUnclamped(a.size, b.size, t));

        public static Rect Add(Rect a, Rect b)
            => new(a.position + b.position, a.size + b.size);
    }
}