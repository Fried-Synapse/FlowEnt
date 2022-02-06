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

        private Vector3 cache3d;
        public new Vector3 Evaluate(float t)
        {
            cache3d.x = x.Evaluate(t);
            cache3d.x = y.Evaluate(t);
            cache3d.z = z.Evaluate(t);
            return cache3d;
        }
    }
}
