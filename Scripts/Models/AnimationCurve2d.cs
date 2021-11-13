using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class AnimationCurve2d
    {
        [SerializeField]
        internal AnimationCurve x;
#pragma warning disable RCS1085 
        public AnimationCurve X { get => x; set => x = value; }
#pragma warning restore RCS1085

        [SerializeField]
        internal AnimationCurve y;
#pragma warning disable RCS1085
        public AnimationCurve Y { get => y; set => y = value; }
#pragma warning restore RCS1085
    }
}
