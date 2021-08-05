using System;
using FlowEnt.Motions.Values;
using UnityEngine;

namespace FlowEnt
{
    public static class ValueMotionExtensions
    {
        public static Tween Value(this Tween motionWrapper, float from, float to, Action<float> onUpdated)
            => motionWrapper.Apply(new FloatValueMotion(from, to, onUpdated));

        public static Tween Value(this Tween motionWrapper, int from, int to, Action<int> onUpdated)
            => motionWrapper.Apply(new IntValueMotion(from, to, onUpdated));

        public static Tween Value(this Tween motionWrapper, Color from, Color to, Action<Color> onUpdated)
            => motionWrapper.Apply(new ColorValueMotion(from, to, onUpdated));
    }
}
