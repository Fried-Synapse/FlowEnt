using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Transforms;

namespace FriedSynapse.FlowEnt
{
    public static class TransformMotionExtensions
    {
        #region Move

        #region Move

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> Move<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, value));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, default, to));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 from, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, from, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveLocal<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalVectorMotion<TTransform>(motionWrapper.Item, value));

        /// <summary>
        /// Applies a <see cref="MoveLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveLocalTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalVectorMotion<TTransform>(motionWrapper.Item, default, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveLocalTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 from, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalVectorMotion<TTransform>(motionWrapper.Item, from, to));

        #endregion

        #region MoveTo AnimationCurve3d

        /// <summary>
        /// Applies a <see cref="MoveAnimationCurve3dMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="animationCurve"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveTo<TTransform>(this TweenMotion<TTransform> motionWrapper, AnimationCurve3d animationCurve)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveAnimationCurve3dMotion<TTransform>(motionWrapper.Item, animationCurve));

        /// <summary>
        /// Applies a <see cref="MoveLocalAnimationCurve3dMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="animationCurve"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveLocalTo<TTransform>(this TweenMotion<TTransform> motionWrapper, AnimationCurve3d animationCurve)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalAnimationCurve3dMotion<TTransform>(motionWrapper.Item, animationCurve));

        #endregion

        #region Move Axis

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveX<TTransform>(this TweenMotion<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, new Vector3(value, 0f, 0f)));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveXTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveAxisMotion<TTransform>(motionWrapper.Item, Axis.X, default, to));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveXTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveAxisMotion<TTransform>(motionWrapper.Item, Axis.X, from, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveLocalX<TTransform>(this TweenMotion<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalVectorMotion<TTransform>(motionWrapper.Item, new Vector3(value, 0f, 0f)));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveLocalXTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.X, default, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveLocalXTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.X, from, to));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveY<TTransform>(this TweenMotion<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, value, 0f)));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveYTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, default, to));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveYTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, from, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveLocalY<TTransform>(this TweenMotion<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, value, 0f)));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveLocalYTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, default, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveLocalYTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, from, to));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveZ<TTransform>(this TweenMotion<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, 0f, value)));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveZTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, default, to));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveZTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, from, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveLocalZ<TTransform>(this TweenMotion<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, 0f, value)));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveLocalZTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, default, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveLocalZTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, from, to));

        #endregion

        #region MoveTo Spline

        /// <summary>
        /// Applies a <see cref="MoveSplineMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="spline"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveTo<TTransform>(this TweenMotion<TTransform> motionWrapper, ISpline spline)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveSplineMotion<TTransform>(motionWrapper.Item, spline));

        /// <summary>
        /// Applies a <see cref="MoveLocalSplineMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="spline"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> MoveLocalTo<TTransform>(this TweenMotion<TTransform> motionWrapper, ISpline spline)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalSplineMotion<TTransform>(motionWrapper.Item, spline));

        #endregion

        #endregion

        #region Rotate

        #region Rotate

        /// <summary>
        /// Applies a <see cref="RotateQuaternionMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> Rotate<TTransform>(this TweenMotion<TTransform> motionWrapper, Quaternion value)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateQuaternionMotion<TTransform>(motionWrapper.Item, value));

        /// <summary>
        /// Applies a <see cref="RotateQuaternionMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Quaternion to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateQuaternionMotion<TTransform>(motionWrapper.Item, default, to));

        /// <summary>
        /// Applies a <see cref="RotateQuaternionMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Quaternion from, Quaternion to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateQuaternionMotion<TTransform>(motionWrapper.Item, from, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalQuaternionMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateLocalTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Quaternion to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalQuaternionMotion<TTransform>(motionWrapper.Item, default, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalQuaternionMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateLocalTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Quaternion from, Quaternion to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalQuaternionMotion<TTransform>(motionWrapper.Item, from, to));

        #endregion

        #region Rotate Vector

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> Rotate<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 value)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateVectorMotion<TTransform>(motionWrapper.Item, value));

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateVectorMotion<TTransform>(motionWrapper.Item, default, to));

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 from, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateVectorMotion<TTransform>(motionWrapper.Item, from, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateLocalTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalVectorMotion<TTransform>(motionWrapper.Item, default, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateLocalTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 from, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalVectorMotion<TTransform>(motionWrapper.Item, from, to));

        #endregion

        #region Rotate Axis

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateX<TTransform>(this TweenMotion<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateVectorMotion<TTransform>(motionWrapper.Item, new Vector3(value, 0f, 0f)));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateXTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateAxisMotion<TTransform>(motionWrapper.Item, Axis.X, default, to));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateXTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateAxisMotion<TTransform>(motionWrapper.Item, Axis.X, from, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateLocalXTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.X, default, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateLocalXTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.X, from, to));

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateY<TTransform>(this TweenMotion<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, value, 0f)));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateYTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, default, to));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateYTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, from, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateLocalYTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, default, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateLocalYTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, from, to));

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateZ<TTransform>(this TweenMotion<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, 0f, value)));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateZTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, default, to));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateZTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, from, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateLocalZTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, default, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> RotateLocalZTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, from, to));

        #endregion

        #region LookAt

        /// <summary>
        /// Applies a <see cref="LookAtTransformMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="target"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> LookAt<TTransform>(this TweenMotion<TTransform> motionWrapper, Transform target)
            where TTransform : Transform
            => motionWrapper.Apply(new LookAtTransformMotion<TTransform>(motionWrapper.Item, target));

        /// <summary>
        /// Applies a <see cref="LookAtVector3Motion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="target"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> LookAt<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 target)
            where TTransform : Transform
            => motionWrapper.Apply(new LookAtVector3Motion<TTransform>(motionWrapper.Item, target));

        #endregion

        #region OrientToPath

        /// <summary>
        /// Applies a <see cref="OrientToPathMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> OrientToPath<TTransform>(this TweenMotion<TTransform> motionWrapper)
            where TTransform : Transform
            => motionWrapper.Apply(new OrientToPathMotion<TTransform>(motionWrapper.Item));

        #endregion

        #endregion

        #region Scale

        #region Scale

        /// <summary>
        /// Applies a <see cref="ScaleLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> Scale<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 value)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalVectorMotion<TTransform>(motionWrapper.Item, value));

        /// <summary>
        /// Applies a <see cref="ScaleLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> ScaleTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalVectorMotion<TTransform>(motionWrapper.Item, default, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> ScaleTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 from, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalVectorMotion<TTransform>(motionWrapper.Item, from, to));

        #endregion

        #region ScaleTo AnimationCurve3d

        /// <summary>
        /// Applies a <see cref="ScaleLocalAnimationCurve3dMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="animationCurve"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> ScaleTo<TTransform>(this TweenMotion<TTransform> motionWrapper, AnimationCurve3d animationCurve)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalAnimationCurve3dMotion<TTransform>(motionWrapper.Item, animationCurve));

        #endregion

        #region Scale Axis

        /// <summary>
        /// Applies a <see cref="ScaleLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> ScaleX<TTransform>(this TweenMotion<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalVectorMotion<TTransform>(motionWrapper.Item, new Vector3(value, 1f, 1f)));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> ScaleXTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.X, default, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> ScaleXTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.X, from, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> ScaleY<TTransform>(this TweenMotion<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalVectorMotion<TTransform>(motionWrapper.Item, new Vector3(1f, value, 1f)));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> ScaleYTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, default, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> ScaleYTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, from, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> ScaleZ<TTransform>(this TweenMotion<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalVectorMotion<TTransform>(motionWrapper.Item, new Vector3(1f, 1f, value)));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> ScaleZTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, default, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotion<TTransform> ScaleZTo<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, from, to));

        #endregion

        #endregion
    }
}