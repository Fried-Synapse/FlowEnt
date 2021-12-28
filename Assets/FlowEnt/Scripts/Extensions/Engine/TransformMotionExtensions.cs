using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Tween.Transforms;
using FriedSynapse.FlowEnt.Motions.Echo.Transforms;

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
        public static TweenMotionProxy<TTransform> Move<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Vector3 value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, value));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, default, to));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Vector3 from, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, from, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocal<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Vector3 value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalVectorMotion<TTransform>(motionWrapper.Item, value));

        /// <summary>
        /// Applies a <see cref="MoveLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalVectorMotion<TTransform>(motionWrapper.Item, default, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Vector3 from, Vector3 to)
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
        public static TweenMotionProxy<TTransform> MoveTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, AnimationCurve3d animationCurve)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveAnimationCurve3dMotion<TTransform>(motionWrapper.Item, animationCurve));

        /// <summary>
        /// Applies a <see cref="MoveLocalAnimationCurve3dMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="animationCurve"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, AnimationCurve3d animationCurve)
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
        public static TweenMotionProxy<TTransform> MoveX<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, new Vector3(value, 0f, 0f)));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveXTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveAxisMotion<TTransform>(motionWrapper.Item, Axis.X, default, to));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveXTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveAxisMotion<TTransform>(motionWrapper.Item, Axis.X, from, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalX<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalVectorMotion<TTransform>(motionWrapper.Item, new Vector3(value, 0f, 0f)));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalXTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.X, default, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalXTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.X, from, to));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveY<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, value, 0f)));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveYTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, default, to));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveYTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, from, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalY<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, value, 0f)));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalYTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, default, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalYTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, from, to));

        /// <summary>
        /// Applies a <see cref="MoveVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveZ<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, 0f, value)));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveZTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, default, to));

        /// <summary>
        /// Applies a <see cref="MoveAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveZTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, from, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalZ<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, 0f, value)));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalZTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, default, to));

        /// <summary>
        /// Applies a <see cref="MoveLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalZTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float from, float to)
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
        public static TweenMotionProxy<TTransform> MoveTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, ISpline spline)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveSplineMotion<TTransform>(motionWrapper.Item, spline));

        /// <summary>
        /// Applies a <see cref="MoveLocalSplineMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="spline"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> MoveLocalTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, ISpline spline)
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
        public static TweenMotionProxy<TTransform> Rotate<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Quaternion value)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateQuaternionMotion<TTransform>(motionWrapper.Item, value));

        /// <summary>
        /// Applies a <see cref="RotateQuaternionMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Quaternion to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateQuaternionMotion<TTransform>(motionWrapper.Item, default, to));

        /// <summary>
        /// Applies a <see cref="RotateQuaternionMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Quaternion from, Quaternion to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateQuaternionMotion<TTransform>(motionWrapper.Item, from, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalQuaternionMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Quaternion to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalQuaternionMotion<TTransform>(motionWrapper.Item, default, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalQuaternionMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Quaternion from, Quaternion to)
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
        public static TweenMotionProxy<TTransform> Rotate<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Vector3 value)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateVectorMotion<TTransform>(motionWrapper.Item, value));

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateVectorMotion<TTransform>(motionWrapper.Item, default, to));

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Vector3 from, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateVectorMotion<TTransform>(motionWrapper.Item, from, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalVectorMotion<TTransform>(motionWrapper.Item, default, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Vector3 from, Vector3 to)
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
        public static TweenMotionProxy<TTransform> RotateX<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateVectorMotion<TTransform>(motionWrapper.Item, new Vector3(value, 0f, 0f)));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateXTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateAxisMotion<TTransform>(motionWrapper.Item, Axis.X, default, to));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateXTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateAxisMotion<TTransform>(motionWrapper.Item, Axis.X, from, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalXTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.X, default, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalXTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.X, from, to));

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateY<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, value, 0f)));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateYTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, default, to));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateYTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, from, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalYTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, default, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalYTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, from, to));

        /// <summary>
        /// Applies a <see cref="RotateVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateZ<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, 0f, value)));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateZTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, default, to));

        /// <summary>
        /// Applies a <see cref="RotateAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateZTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, from, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalZTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, default, to));

        /// <summary>
        /// Applies a <see cref="RotateLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> RotateLocalZTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float from, float to)
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
        public static TweenMotionProxy<TTransform> LookAt<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Transform target)
            where TTransform : Transform
            => motionWrapper.Apply(new LookAtTransformMotion<TTransform>(motionWrapper.Item, target));

        /// <summary>
        /// Applies a <see cref="LookAtVector3Motion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="target"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> LookAt<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Vector3 target)
            where TTransform : Transform
            => motionWrapper.Apply(new LookAtVector3Motion<TTransform>(motionWrapper.Item, target));

        #endregion

        #region OrientToPath

        /// <summary>
        /// Applies a <see cref="OrientToPathMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> OrientToPath<TTransform>(this TweenMotionProxy<TTransform> motionWrapper)
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
        public static TweenMotionProxy<TTransform> Scale<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Vector3 value)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalVectorMotion<TTransform>(motionWrapper.Item, value));

        /// <summary>
        /// Applies a <see cref="ScaleLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalVectorMotion<TTransform>(motionWrapper.Item, default, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, Vector3 from, Vector3 to)
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
        public static TweenMotionProxy<TTransform> ScaleTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, AnimationCurve3d animationCurve)
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
        public static TweenMotionProxy<TTransform> ScaleX<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalVectorMotion<TTransform>(motionWrapper.Item, new Vector3(value, 1f, 1f)));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleXTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.X, default, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleXTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.X, from, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleY<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalVectorMotion<TTransform>(motionWrapper.Item, new Vector3(1f, value, 1f)));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleYTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, default, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleYTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, from, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalVectorMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="value"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleZ<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float value)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalVectorMotion<TTransform>(motionWrapper.Item, new Vector3(1f, 1f, value)));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleZTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, default, to));

        /// <summary>
        /// Applies a <see cref="ScaleLocalAxisMotion{TTransform}" /> to the tween.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static TweenMotionProxy<TTransform> ScaleZTo<TTransform>(this TweenMotionProxy<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, from, to));

        #endregion

        #endregion

        /// <summary>
        /// Applies a <see cref="MoveTowardsTransformMotion{TTransform}" /> to the echo.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> MoveTowards<TTransform>(this EchoMotionProxy<TTransform> motionWrapper, Transform target, float speed = MoveTowardsTransformMotion<TTransform>.DefaultSpeed)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveTowardsTransformMotion<TTransform>(motionWrapper.Item, target, speed));

        /// <summary>
        /// Applies a <see cref="MoveTowardsTransformElasticMotion{TTransform}" /> to the echo.
        /// </summary>
        /// <param name="motionWrapper"></param>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        /// <typeparam name="TTransform"></typeparam>
        public static EchoMotionProxy<TTransform> MoveTowardsElastic<TTransform>(this EchoMotionProxy<TTransform> motionWrapper, Transform target, float speed = MoveTowardsTransformMotion<TTransform>.DefaultSpeed)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveTowardsTransformElasticMotion<TTransform>(motionWrapper.Item, target, speed));
    }
}