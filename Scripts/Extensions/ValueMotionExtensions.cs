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

        public static Tween Value(this Tween motionWrapper, Vector2 from, Vector2 to, Action<Vector2> onUpdated)
            => motionWrapper.Apply(new Vector2ValueMotion(from, to, onUpdated));

        public static Tween Value(this Tween motionWrapper, Vector3 from, Vector3 to, Action<Vector3> onUpdated)
            => motionWrapper.Apply(new Vector3ValueMotion(from, to, onUpdated));

        public static Tween Value(this Tween motionWrapper, Vector4 from, Vector4 to, Action<Vector4> onUpdated)
            => motionWrapper.Apply(new Vector4ValueMotion(from, to, onUpdated));

        public static Tween Value(this Tween motionWrapper, ISpline spline, Action<Vector3> onUpdated)
            => motionWrapper.Apply(new SplineValueMotion(spline, onUpdated));

        public static Tween Value(this Tween motionWrapper, Quaternion from, Quaternion to, Action<Quaternion> onUpdated)
            => motionWrapper.Apply(new QuaternionValueMotion(from, to, onUpdated));

        public static Tween Value(this Tween motionWrapper, Color from, Color to, Action<Color> onUpdated)
            => motionWrapper.Apply(new ColorValueMotion(from, to, onUpdated));

        public static Tween Value(this Tween motionWrapper, Color32 from, Color32 to, Action<Color32> onUpdated)
            => motionWrapper.Apply(new Color32ValueMotion(from, to, onUpdated));
    }
}
