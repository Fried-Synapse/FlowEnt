using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Transforms;

namespace FriedSynapse.FlowEnt
{
    public static class TransformMotionExtensions
    {
        #region Move

        #region Move

        public static TweenMotion<TTransform> Move<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, value));

        public static TweenMotion<TTransform> MoveX<TTransform>(this TweenMotion<TTransform> motionWrapper, float x)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, new Vector3(x, 0f, 0f)));

        public static TweenMotion<TTransform> MoveY<TTransform>(this TweenMotion<TTransform> motionWrapper, float y)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, y, 0f)));

        public static TweenMotion<TTransform> MoveZ<TTransform>(this TweenMotion<TTransform> motionWrapper, float z)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, 0f, z)));

        public static TweenMotion<TTransform> MoveLocal<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalVectorMotion<TTransform>(motionWrapper.Item, value));

        public static TweenMotion<TTransform> MoveLocalX<TTransform>(this TweenMotion<TTransform> motionWrapper, float x)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalVectorMotion<TTransform>(motionWrapper.Item, new Vector3(x, 0f, 0f)));

        public static TweenMotion<TTransform> MoveLocalY<TTransform>(this TweenMotion<TTransform> motionWrapper, float y)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, y, 0f)));

        public static TweenMotion<TTransform> MoveLocalZ<TTransform>(this TweenMotion<TTransform> motionWrapper, float z)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, 0f, z)));

        #endregion

        #region MoveTo

        public static TweenMotion<TTransform> MoveTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveToVectorMotion<TTransform>(motionWrapper.Item, to));

        public static TweenMotion<TTransform> MoveTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 from, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveToVectorMotion<TTransform>(motionWrapper.Item, from, to));

        public static TweenMotion<TTransform> MoveToX<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveToAxisMotion<TTransform>(motionWrapper.Item, Axis.X, to));

        public static TweenMotion<TTransform> MoveToX<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveToAxisMotion<TTransform>(motionWrapper.Item, Axis.X, from, to));

        public static TweenMotion<TTransform> MoveToY<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveToAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, to));

        public static TweenMotion<TTransform> MoveToY<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveToAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, from, to));

        public static TweenMotion<TTransform> MoveToZ<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveToAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, to));

        public static TweenMotion<TTransform> MoveToZ<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveToAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, from, to));

        public static TweenMotion<TTransform> MoveLocalTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalToVectorMotion<TTransform>(motionWrapper.Item, to));

        public static TweenMotion<TTransform> MoveLocalTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 from, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalToVectorMotion<TTransform>(motionWrapper.Item, from, to));

        public static TweenMotion<TTransform> MoveLocalToX<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalToAxisMotion<TTransform>(motionWrapper.Item, Axis.X, to));

        public static TweenMotion<TTransform> MoveLocalToX<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalToAxisMotion<TTransform>(motionWrapper.Item, Axis.X, from, to));

        public static TweenMotion<TTransform> MoveLocalToY<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalToAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, to));

        public static TweenMotion<TTransform> MoveLocalToY<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalToAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, from, to));

        public static TweenMotion<TTransform> MoveLocalToZ<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalToAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, to));

        public static TweenMotion<TTransform> MoveLocalToZ<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalToAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, from, to));

        #endregion

        #region Move Spline

        public static TweenMotion<TTransform> MoveTo<TTransform>(this TweenMotion<TTransform> motionWrapper, ISpline spline)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveToSplineMotion<TTransform>(motionWrapper.Item, spline));

        public static TweenMotion<TTransform> MoveLocalTo<TTransform>(this TweenMotion<TTransform> motionWrapper, ISpline spline)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalToSplineMotion<TTransform>(motionWrapper.Item, spline));

        #endregion

        #endregion

        #region Rotate

        #region Rotate

        public static TweenMotion<TTransform> Rotate<TTransform>(this TweenMotion<TTransform> motionWrapper, Quaternion value)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateQuaternionMotion<TTransform>(motionWrapper.Item, value));

        public static TweenMotion<TTransform> Rotate<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 value)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateVectorMotion<TTransform>(motionWrapper.Item, value));

        public static TweenMotion<TTransform> RotateX<TTransform>(this TweenMotion<TTransform> motionWrapper, float x)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateVectorMotion<TTransform>(motionWrapper.Item, new Vector3(x, 0f, 0f)));

        public static TweenMotion<TTransform> RotateY<TTransform>(this TweenMotion<TTransform> motionWrapper, float y)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, y, 0f)));

        public static TweenMotion<TTransform> RotateZ<TTransform>(this TweenMotion<TTransform> motionWrapper, float z)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0f, 0f, z)));

        #endregion

        #region RotateTo

        public static TweenMotion<TTransform> RotateTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Quaternion to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateToQuaternionMotion<TTransform>(motionWrapper.Item, to));

        public static TweenMotion<TTransform> RotateTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Quaternion from, Quaternion to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateToQuaternionMotion<TTransform>(motionWrapper.Item, from, to));

        public static TweenMotion<TTransform> RotateTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateToVectorMotion<TTransform>(motionWrapper.Item, to));

        public static TweenMotion<TTransform> RotateTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 from, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateToVectorMotion<TTransform>(motionWrapper.Item, from, to));

        public static TweenMotion<TTransform> RotateLocalTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Quaternion to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalToQuaternionMotion<TTransform>(motionWrapper.Item, to));

        public static TweenMotion<TTransform> RotateLocalTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Quaternion from, Quaternion to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalToQuaternionMotion<TTransform>(motionWrapper.Item, from, to));

        public static TweenMotion<TTransform> RotateLocalTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalToVectorMotion<TTransform>(motionWrapper.Item, to));

        public static TweenMotion<TTransform> RotateLocalTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 from, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalToVectorMotion<TTransform>(motionWrapper.Item, from, to));

        #endregion

        #region OrientToPath

        public static TweenMotion<TTransform> OrientToPath<TTransform>(this TweenMotion<TTransform> motionWrapper)
            where TTransform : Transform
            => motionWrapper.Apply(new OrientToPathMotion<TTransform>(motionWrapper.Item));

        #endregion

        #endregion

        #region Scale

        #region Scale

        public static TweenMotion<TTransform> Scale<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 value)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleVectorMotion<TTransform>(motionWrapper.Item, value));

        public static TweenMotion<TTransform> ScaleX<TTransform>(this TweenMotion<TTransform> motionWrapper, float x)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleVectorMotion<TTransform>(motionWrapper.Item, new Vector3(x, 1f, 1f)));

        public static TweenMotion<TTransform> ScaleY<TTransform>(this TweenMotion<TTransform> motionWrapper, float y)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleVectorMotion<TTransform>(motionWrapper.Item, new Vector3(1f, y, 1f)));

        public static TweenMotion<TTransform> ScaleZ<TTransform>(this TweenMotion<TTransform> motionWrapper, float z)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleVectorMotion<TTransform>(motionWrapper.Item, new Vector3(1f, 1f, z)));

        #endregion

        #region ScaleTo

        public static TweenMotion<TTransform> ScaleLocalTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalToVectorMotion<TTransform>(motionWrapper.Item, to));

        public static TweenMotion<TTransform> ScaleLocalTo<TTransform>(this TweenMotion<TTransform> motionWrapper, Vector3 from, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalToVectorMotion<TTransform>(motionWrapper.Item, from, to));

        public static TweenMotion<TTransform> ScaleLocalToX<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalToAxisMotion<TTransform>(motionWrapper.Item, Axis.X, to));

        public static TweenMotion<TTransform> ScaleLocalToX<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalToAxisMotion<TTransform>(motionWrapper.Item, Axis.X, from, to));

        public static TweenMotion<TTransform> ScaleLocalToY<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalToAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, to));

        public static TweenMotion<TTransform> ScaleLocalToY<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalToAxisMotion<TTransform>(motionWrapper.Item, Axis.Y, from, to));

        public static TweenMotion<TTransform> ScaleLocalToZ<TTransform>(this TweenMotion<TTransform> motionWrapper, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalToAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, to));

        public static TweenMotion<TTransform> ScaleLocalToZ<TTransform>(this TweenMotion<TTransform> motionWrapper, float from, float to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalToAxisMotion<TTransform>(motionWrapper.Item, Axis.Z, from, to));

        #endregion

        #endregion
    }
}