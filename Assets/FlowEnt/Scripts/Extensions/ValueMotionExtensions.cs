using System;
using FriedSynapse.FlowEnt.Motions.Tween.Values;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class ValueMotionExtensions
    {
        /// <summary>
        /// Applies a <see cref="FloatValueMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween proxy, float from, float to, Action<float> onUpdated)
            => proxy.Apply(new FloatValueMotion(from, to, onUpdated));

        /// <summary>
        /// Applies an <see cref="IntValueMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween proxy, int from, int to, Action<int> onUpdated)
            => proxy.Apply(new IntValueMotion(from, to, onUpdated));

        /// <summary>
        /// Applies a <see cref="Vector2ValueMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween proxy, Vector2 from, Vector2 to, Action<Vector2> onUpdated)
            => proxy.Apply(new Vector2ValueMotion(from, to, onUpdated));

        /// <summary>
        /// Applies a <see cref="Vector3ValueMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween proxy, Vector3 from, Vector3 to, Action<Vector3> onUpdated)
            => proxy.Apply(new Vector3ValueMotion(from, to, onUpdated));

        /// <summary>
        /// Applies a <see cref="Vector4ValueMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween proxy, Vector4 from, Vector4 to, Action<Vector4> onUpdated)
            => proxy.Apply(new Vector4ValueMotion(from, to, onUpdated));

        /// <summary>
        /// Applies a <see cref="SplineValueMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="spline"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween proxy, ISpline spline, Action<Vector3> onUpdated)
            => proxy.Apply(new SplineValueMotion(spline, onUpdated));

        /// <summary>
        /// Applies a <see cref="QuaternionValueMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween proxy, Quaternion from, Quaternion to, Action<Quaternion> onUpdated)
            => proxy.Apply(new QuaternionValueMotion(from, to, onUpdated));

        /// <summary>
        /// Applies a <see cref="ColorValueMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween proxy, Color from, Color to, Action<Color> onUpdated)
            => proxy.Apply(new ColorValueMotion(from, to, onUpdated));

        /// <summary>
        /// Applies a <see cref="Color32ValueMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween proxy, Color32 from, Color32 to, Action<Color32> onUpdated)
            => proxy.Apply(new Color32ValueMotion(from, to, onUpdated));
    }
}
