using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class AnimationCurve3d : AnimationCurve2d
    {
        [SerializeField]
        protected AnimationCurve z;
#pragma warning disable RCS1085
        public AnimationCurve Z { get => z; set => z = value; }
#pragma warning restore RCS1085

        private Vector3 cache;
        public new Vector3 Evaluate(float t)
        {
            cache.x = x.Evaluate(t);
            cache.x = y.Evaluate(t);
            cache.z = z.Evaluate(t);
            return cache;
        }
    }
}
