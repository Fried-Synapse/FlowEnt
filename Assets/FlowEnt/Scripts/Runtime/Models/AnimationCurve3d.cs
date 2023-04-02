using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public struct AnimationCurve3d
    {
        public AnimationCurve3d(AnimationCurve x, AnimationCurve y, AnimationCurve z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            cache = Vector3.zero;
        }

        [SerializeField]
        private AnimationCurve x;
        public AnimationCurve X { get => x; set => x = value; }

        [SerializeField]
        private AnimationCurve y;
        public AnimationCurve Y { get => y; set => y = value; }

        [SerializeField]
        private AnimationCurve z;
        public AnimationCurve Z { get => z; set => z = value; }

        private Vector3 cache;
        public Vector3 Evaluate(float t)
        {
            cache.x = x.Evaluate(t);
            cache.y = y.Evaluate(t);
            cache.z = z.Evaluate(t);
            return cache;
        }

        public static implicit operator AnimationCurve(AnimationCurve3d curve) => curve.x;
        public static implicit operator AnimationCurve2d(AnimationCurve3d curve) => new AnimationCurve2d(curve.x, curve.y);
    }
}
