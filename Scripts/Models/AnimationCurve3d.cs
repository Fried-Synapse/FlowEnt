using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class AnimationCurve3d : AnimationCurve2d
    {
        [SerializeField]
        internal AnimationCurve z;
#pragma warning disable RCS1085
        public AnimationCurve Z { get => z; set => z = value; }
#pragma warning restore RCS1085
    }
}
