using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class AnimationCurve2d
    {
        [SerializeField]
        protected AnimationCurve x;
#pragma warning disable RCS1085 
        public AnimationCurve X { get => x; set => x = value; }
#pragma warning restore RCS1085

        [SerializeField]
        protected AnimationCurve y;
#pragma warning disable RCS1085
        public AnimationCurve Y { get => y; set => y = value; }
#pragma warning restore RCS1085

        private Vector2 cache2d;
        public Vector2 Evaluate(float t)
        {
            cache2d.x = x.Evaluate(t);
            cache2d.x = y.Evaluate(t);
            return cache2d;
        }
    }
}
