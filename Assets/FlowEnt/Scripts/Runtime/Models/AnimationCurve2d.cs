using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public struct AnimationCurve2d
    {
        public AnimationCurve2d(AnimationCurve x, AnimationCurve y)
        {
            this.x = x;
            this.y = y;
            cache = Vector3.zero;
        }

        [SerializeField]
        private AnimationCurve x;
        public AnimationCurve X { get => x; set => x = value; }

        [SerializeField]
        private AnimationCurve y;
        public AnimationCurve Y { get => y; set => y = value; }

        private Vector2 cache;
        public Vector2 Evaluate(float t)
        {
            cache.x = x.Evaluate(t);
            cache.x = y.Evaluate(t);
            return cache;
        }

        public static implicit operator AnimationCurve3d(AnimationCurve2d curve) => new AnimationCurve3d(curve.x, curve.y, new AnimationCurve());
    }
}
