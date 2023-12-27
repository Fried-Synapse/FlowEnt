using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Tween.Transforms;

namespace FriedSynapse.FlowEnt
{
    public static partial class TransformMotionExtensions
    {
        #region Move

        #region Move Vector

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> Move<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Vector3 value)
            where TTransform : Transform
            => proxy.Apply(new MoveVectorMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Vector3 to)
            where TTransform : Transform
            => proxy.Apply(new MoveVectorMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Vector3 from, Vector3 to)
            where TTransform : Transform
            => proxy.Apply(new MoveVectorMotion(proxy.Item, from, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocal<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Vector3 value)
            where TTransform : Transform
            => proxy.Apply(new MoveLocalVectorMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="MoveLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Vector3 to)
            where TTransform : Transform
            => proxy.Apply(new MoveLocalVectorMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Vector3 from, Vector3 to)
            where TTransform : Transform
            => proxy.Apply(new MoveLocalVectorMotion(proxy.Item, from, to));

        #endregion

        #region MoveTo AnimationCurve3d

        /// <summary>
        /// Applies a <see cref="MoveAnimationCurve3dMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="animationCurve"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            AnimationCurve3d animationCurve)
            where TTransform : Transform
            => proxy.Apply(new MoveAnimationCurve3dMotion(proxy.Item, animationCurve));

        /// <summary>
        /// Applies a <see cref="MoveLocalAnimationCurve3dMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="animationCurve"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            AnimationCurve3d animationCurve)
            where TTransform : Transform
            => proxy.Apply(new MoveLocalAnimationCurve3dMotion(proxy.Item, animationCurve));

        #endregion

        #region Move Axis

        #region Move

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="axis"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> Move<TTransform>(this TweenMotionProxy<TTransform> proxy, Axis axis,
            float value)
            where TTransform : Transform
            => proxy.Apply(new MoveAxisMotion(proxy.Item, axis, value));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="axis"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Axis axis, float to)
            where TTransform : Transform
            => proxy.Apply(new MoveAxisMotion(proxy.Item, axis, default, to));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="axis"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Axis axis, float from, float to)
            where TTransform : Transform
            => proxy.Apply(new MoveAxisMotion(proxy.Item, axis, from, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="axis"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocal<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Axis axis, float value)
            where TTransform : Transform
            => proxy.Apply(new MoveLocalAxisMotion(proxy.Item, axis, value));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="axis"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Axis axis, float to)
            where TTransform : Transform
            => proxy.Apply(new MoveLocalAxisMotion(proxy.Item, axis, default, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="axis"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Axis axis, float from, float to)
            where TTransform : Transform
            => proxy.Apply(new MoveLocalAxisMotion(proxy.Item, axis, from, to));

        #endregion

        #region Move Axis X

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveX<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float value)
            where TTransform : Transform
            => proxy.Apply(new MoveAxisMotion(proxy.Item, Axis.X, value));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveXTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float to)
            where TTransform : Transform
            => proxy.Apply(new MoveAxisMotion(proxy.Item, Axis.X, default, to));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveXTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float from, float to)
            where TTransform : Transform
            => proxy.Apply(new MoveAxisMotion(proxy.Item, Axis.X, from, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalX<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float value)
            where TTransform : Transform
            => proxy.Apply(new MoveLocalAxisMotion(proxy.Item, Axis.X, value));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalXTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float to)
            where TTransform : Transform
            => proxy.Apply(new MoveLocalAxisMotion(proxy.Item, Axis.X, default, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalXTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float from, float to)
            where TTransform : Transform
            => proxy.Apply(new MoveLocalAxisMotion(proxy.Item, Axis.X, from, to));

        #endregion

        #region Move Axis Y

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveY<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float value)
            where TTransform : Transform
            => proxy.Apply(new MoveAxisMotion(proxy.Item, Axis.Y, value));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveYTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float to)
            where TTransform : Transform
            => proxy.Apply(new MoveAxisMotion(proxy.Item, Axis.Y, default, to));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveYTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float from, float to)
            where TTransform : Transform
            => proxy.Apply(new MoveAxisMotion(proxy.Item, Axis.Y, from, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalY<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float value)
            where TTransform : Transform
            => proxy.Apply(new MoveLocalAxisMotion(proxy.Item, Axis.Y, value));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalYTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float to)
            where TTransform : Transform
            => proxy.Apply(new MoveLocalAxisMotion(proxy.Item, Axis.Y, default, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalYTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float from, float to)
            where TTransform : Transform
            => proxy.Apply(new MoveLocalAxisMotion(proxy.Item, Axis.Y, from, to));

        #endregion

        #region Move Axis Z

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveZ<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float value)
            where TTransform : Transform
            => proxy.Apply(new MoveAxisMotion(proxy.Item, Axis.Z, value));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveZTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float to)
            where TTransform : Transform
            => proxy.Apply(new MoveAxisMotion(proxy.Item, Axis.Z, default, to));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveZTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float from, float to)
            where TTransform : Transform
            => proxy.Apply(new MoveAxisMotion(proxy.Item, Axis.Z, from, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalZ<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float value)
            where TTransform : Transform
            => proxy.Apply(new MoveLocalAxisMotion(proxy.Item, Axis.Z, value));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalZTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float to)
            where TTransform : Transform
            => proxy.Apply(new MoveLocalAxisMotion(proxy.Item, Axis.Z, default, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalZTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float from, float to)
            where TTransform : Transform
            => proxy.Apply(new MoveLocalAxisMotion(proxy.Item, Axis.Z, from, to));

        #endregion

        #endregion

        #region MoveTo Spline

        /// <summary>
        /// Applies a <see cref="MoveCurveMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="curve"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            ICurve curve)
            where TTransform : Transform
            => proxy.Apply(new MoveCurveMotion(proxy.Item, curve));

        /// <summary>
        /// Applies a <see cref="MoveLocalCurveMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="curve"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            ICurve curve)
            where TTransform : Transform
            => proxy.Apply(new MoveLocalCurveMotion(proxy.Item, curve));

        #endregion

        #endregion

        #region Rotate

        #region Rotate Quaternion

        /// <summary>
        /// Applies a <see cref="RotateQuaternionMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> Rotate<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Quaternion value)
            where TTransform : Transform
            => proxy.Apply(new RotateQuaternionMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="RotateQuaternionMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Quaternion to)
            where TTransform : Transform
            => proxy.Apply(new RotateQuaternionMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="RotateQuaternionMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Quaternion from, Quaternion to)
            where TTransform : Transform
            => proxy.Apply(new RotateQuaternionMotion(proxy.Item, from, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalQuaternionMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocal<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Quaternion value)
            where TTransform : Transform
            => proxy.Apply(new RotateLocalQuaternionMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="RotateLocalQuaternionMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Quaternion to)
            where TTransform : Transform
            => proxy.Apply(new RotateLocalQuaternionMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalQuaternionMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Quaternion from, Quaternion to)
            where TTransform : Transform
            => proxy.Apply(new RotateLocalQuaternionMotion(proxy.Item, from, to));

        #endregion

        #region Rotate Vector

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> Rotate<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Vector3 value)
            where TTransform : Transform
            => proxy.Apply(new RotateVectorMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Vector3 to)
            where TTransform : Transform
            => proxy.Apply(new RotateVectorMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Vector3 from, Vector3 to)
            where TTransform : Transform
            => proxy.Apply(new RotateVectorMotion(proxy.Item, from, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocal<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Vector3 value)
            where TTransform : Transform
            => proxy.Apply(new RotateLocalVectorMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="RotateLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Vector3 to)
            where TTransform : Transform
            => proxy.Apply(new RotateLocalVectorMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Vector3 from, Vector3 to)
            where TTransform : Transform
            => proxy.Apply(new RotateLocalVectorMotion(proxy.Item, from, to));

        #endregion

        #region Rotate Axis

        #region Rotate Axis X

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateX<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float value)
            where TTransform : Transform
            => proxy.Apply(new RotateVectorMotion(proxy.Item, new Vector3(value, 0f, 0f)));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateXTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float to)
            where TTransform : Transform
            => proxy.Apply(new RotateAxisMotion(proxy.Item, Axis.X, default, to));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateXTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float from, float to)
            where TTransform : Transform
            => proxy.Apply(new RotateAxisMotion(proxy.Item, Axis.X, from, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalX<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float value)
            where TTransform : Transform
            => proxy.Apply(new RotateLocalVectorMotion(proxy.Item, new Vector3(value, 0f, 0f)));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalXTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float to)
            where TTransform : Transform
            => proxy.Apply(new RotateLocalAxisMotion(proxy.Item, Axis.X, default, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalXTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float from, float to)
            where TTransform : Transform
            => proxy.Apply(new RotateLocalAxisMotion(proxy.Item, Axis.X, from, to));

        #endregion

        #region Rotate Axis Y

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateY<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float value)
            where TTransform : Transform
            => proxy.Apply(new RotateVectorMotion(proxy.Item, new Vector3(0f, value, 0f)));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateYTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float to)
            where TTransform : Transform
            => proxy.Apply(new RotateAxisMotion(proxy.Item, Axis.Y, default, to));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateYTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float from, float to)
            where TTransform : Transform
            => proxy.Apply(new RotateAxisMotion(proxy.Item, Axis.Y, from, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalY<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float value)
            where TTransform : Transform
            => proxy.Apply(new RotateLocalVectorMotion(proxy.Item, new Vector3(0f, value, 0f)));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalYTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float to)
            where TTransform : Transform
            => proxy.Apply(new RotateLocalAxisMotion(proxy.Item, Axis.Y, default, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalYTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float from, float to)
            where TTransform : Transform
            => proxy.Apply(new RotateLocalAxisMotion(proxy.Item, Axis.Y, from, to));

        #endregion

        #region Rotate Axis Z

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateZ<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float value)
            where TTransform : Transform
            => proxy.Apply(new RotateVectorMotion(proxy.Item, new Vector3(0f, 0f, value)));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateZTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float to)
            where TTransform : Transform
            => proxy.Apply(new RotateAxisMotion(proxy.Item, Axis.Z, default, to));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateZTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float from, float to)
            where TTransform : Transform
            => proxy.Apply(new RotateAxisMotion(proxy.Item, Axis.Z, from, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalZ<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float value)
            where TTransform : Transform
            => proxy.Apply(new RotateLocalVectorMotion(proxy.Item, new Vector3(0f, 0f, value)));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalZTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float to)
            where TTransform : Transform
            => proxy.Apply(new RotateLocalAxisMotion(proxy.Item, Axis.Z, default, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalZTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float from, float to)
            where TTransform : Transform
            => proxy.Apply(new RotateLocalAxisMotion(proxy.Item, Axis.Z, from, to));

        #endregion

        #endregion

        #region RotateAround

        /// <summary>
        /// Applies a <see cref="RotateAroundTransformMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="point"></param>
        /// <param name="axis"></param>
        /// <param name="toAngle"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateAround<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Transform point, Vector3 axis, float toAngle)
            where TTransform : Transform
            => proxy.Apply(new RotateAroundTransformMotion(proxy.Item, point, axis, toAngle));

        /// <summary>
        /// Applies a <see cref="RotateAroundVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="point"></param>
        /// <param name="axis"></param>
        /// <param name="toAngle"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateAround<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Vector3 point, Vector3 axis, float toAngle)
            where TTransform : Transform
            => proxy.Apply(new RotateAroundVectorMotion(proxy.Item, point, axis, toAngle));

        #endregion

        #region LookAt

        /// <summary>
        /// Applies a <see cref="LookAtTransformMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> LookAt<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Transform target)
            where TTransform : Transform
            => proxy.Apply(new LookAtTransformMotion(proxy.Item, target));

        /// <summary>
        /// Applies a <see cref="LookAtVector3Motion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="target"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> LookAt<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Vector3 target)
            where TTransform : Transform
            => proxy.Apply(new LookAtVector3Motion(proxy.Item, target));

        #endregion

        #region OrientToPath

        /// <summary>
        /// Applies a <see cref="OrientToPathMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> OrientToPath<TTransform>(this TweenMotionProxy<TTransform> proxy)
            where TTransform : Transform
            => proxy.Apply(new OrientToPathMotion(proxy.Item));

        #endregion

        #endregion

        #region Scale

        #region Scale

        /// <summary>
        /// Applies a <see cref="ScaleLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleLocal<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Vector3 value)
            where TTransform : Transform
            => proxy.Apply(new ScaleLocalVectorMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="ScaleLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleLocalTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Vector3 to)
            where TTransform : Transform
            => proxy.Apply(new ScaleLocalVectorMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleLocalTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Vector3 from, Vector3 to)
            where TTransform : Transform
            => proxy.Apply(new ScaleLocalVectorMotion(proxy.Item, from, to));

        #endregion

        #region ScaleTo AnimationCurve3d

        /// <summary>
        /// Applies a <see cref="ScaleLocalAnimationCurve3dMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="animationCurve"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleLocalTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            AnimationCurve3d animationCurve)
            where TTransform : Transform
            => proxy.Apply(new ScaleLocalAnimationCurve3dMotion(proxy.Item, animationCurve));

        #endregion

        #region Scale Axis

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="axis"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleLocal<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Axis axis, float value)
            where TTransform : Transform
            => proxy.Apply(new ScaleLocalAxisMotion(proxy.Item, axis, value));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="axis"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleLocalTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Axis axis, float to)
            where TTransform : Transform
            => proxy.Apply(new ScaleLocalAxisMotion(proxy.Item, axis, default, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="axis"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleLocalTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            Axis axis, float from, float to)
            where TTransform : Transform
            => proxy.Apply(new ScaleLocalAxisMotion(proxy.Item, axis, from, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleLocalX<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float value)
            where TTransform : Transform
            => proxy.Apply(new ScaleLocalAxisMotion(proxy.Item, Axis.X, value));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleLocalXTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float to)
            where TTransform : Transform
            => proxy.Apply(new ScaleLocalAxisMotion(proxy.Item, Axis.X, default, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleLocalXTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float from, float to)
            where TTransform : Transform
            => proxy.Apply(new ScaleLocalAxisMotion(proxy.Item, Axis.X, from, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleLocalY<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float value)
            where TTransform : Transform
            => proxy.Apply(new ScaleLocalAxisMotion(proxy.Item, Axis.Y, value));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleLocalYTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float to)
            where TTransform : Transform
            => proxy.Apply(new ScaleLocalAxisMotion(proxy.Item, Axis.Y, default, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleLocalYTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float from, float to)
            where TTransform : Transform
            => proxy.Apply(new ScaleLocalAxisMotion(proxy.Item, Axis.Y, from, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleLocalZ<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float value)
            where TTransform : Transform
            => proxy.Apply(new ScaleLocalAxisMotion(proxy.Item, Axis.Z, value));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleLocalZTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float to)
            where TTransform : Transform
            => proxy.Apply(new ScaleLocalAxisMotion(proxy.Item, Axis.Z, default, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleLocalZTo<TTransform>(this TweenMotionProxy<TTransform> proxy,
            float from, float to)
            where TTransform : Transform
            => proxy.Apply(new ScaleLocalAxisMotion(proxy.Item, Axis.Z, from, to));

        #endregion

        #endregion
    }
}