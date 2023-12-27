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
        /// <param name="tween"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween tween, float from, float to, Action<float> onUpdated)
            => tween.Apply(new FloatValueMotion(from, to, onUpdated));

        /// <summary>
        /// Applies an <see cref="IntValueMotion" /> to the tween.
        /// </summary>
        /// <param name="tween"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween tween, int from, int to, Action<int> onUpdated)
            => tween.Apply(new IntValueMotion(from, to, onUpdated));

        /// <summary>
        /// Applies a <see cref="Vector2ValueMotion" /> to the tween.
        /// </summary>
        /// <param name="tween"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween tween, Vector2 from, Vector2 to, Action<Vector2> onUpdated)
            => tween.Apply(new Vector2ValueMotion(from, to, onUpdated));

        /// <summary>
        /// Applies a <see cref="Vector3ValueMotion" /> to the tween.
        /// </summary>
        /// <param name="tween"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween tween, Vector3 from, Vector3 to, Action<Vector3> onUpdated)
            => tween.Apply(new Vector3ValueMotion(from, to, onUpdated));

        /// <summary>
        /// Applies a <see cref="Vector4ValueMotion" /> to the tween.
        /// </summary>
        /// <param name="tween"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween tween, Vector4 from, Vector4 to, Action<Vector4> onUpdated)
            => tween.Apply(new Vector4ValueMotion(from, to, onUpdated));

        /// <summary>
        /// Applies a <see cref="AnimationCurveValueMotion" /> to the tween.
        /// </summary>
        /// <param name="tween"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween tween, AnimationCurve animationCurve, Action<float> onUpdated)
            => tween.Apply(new AnimationCurveValueMotion(animationCurve, onUpdated));

        /// <summary>
        /// Applies a <see cref="AnimationCurve2dValueMotion" /> to the tween.
        /// </summary>
        /// <param name="tween"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween tween, AnimationCurve2d animationCurve, Action<Vector2> onUpdated)
            => tween.Apply(new AnimationCurve2dValueMotion(animationCurve, onUpdated));

        /// <summary>
        /// Applies a <see cref="AnimationCurve3dValueMotion" /> to the tween.
        /// </summary>
        /// <param name="tween"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween tween, AnimationCurve3d animationCurve, Action<Vector3> onUpdated)
            => tween.Apply(new AnimationCurve3dValueMotion(animationCurve, onUpdated));

        /// <summary>
        /// Applies a <see cref="CurveValueMotion" /> to the tween.
        /// </summary>
        /// <param name="tween"></param>
        /// <param name="curve"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween tween, ICurve curve, Action<Vector3> onUpdated)
            => tween.Apply(new CurveValueMotion(curve, onUpdated));

        /// <summary>
        /// Applies a <see cref="QuaternionValueMotion" /> to the tween.
        /// </summary>
        /// <param name="tween"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween tween, Quaternion from, Quaternion to, Action<Quaternion> onUpdated)
            => tween.Apply(new QuaternionValueMotion(from, to, onUpdated));

        /// <summary>
        /// Applies a <see cref="ColorValueMotion" /> to the tween.
        /// </summary>
        /// <param name="tween"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween tween, Color from, Color to, Action<Color> onUpdated)
            => tween.Apply(new ColorValueMotion(from, to, onUpdated));

        /// <summary>
        /// Applies a <see cref="Color32ValueMotion" /> to the tween.
        /// </summary>
        /// <param name="tween"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="onUpdated"></param>
        public static Tween Value(this Tween tween, Color32 from, Color32 to, Action<Color32> onUpdated)
            => tween.Apply(new Color32ValueMotion(from, to, onUpdated));
    }
}
