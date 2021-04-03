using UnityEngine;

namespace FlowEnt
{
    public static class TransformExtensions 
    {
        #region Move

        public static MotionWrapper<TTransform> Move<TTransform>(this TTransform transform, Vector3 value, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).Move(value);

        public static MotionWrapper<TTransform> MoveX<TTransform>(this TTransform transform, float x, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).MoveX(x);

        public static MotionWrapper<TTransform> MoveY<TTransform>(this TTransform transform, float y, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).MoveY(y);

        public static MotionWrapper<TTransform> MoveZ<TTransform>(this TTransform transform, float z, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).MoveZ(z);

        public static MotionWrapper<TTransform> MoveTo<TTransform>(this TTransform transform, Vector3 to, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).MoveTo(to);

        public static MotionWrapper<TTransform> MoveLocalTo<TTransform>(this TTransform transform, Vector3 to, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).MoveLocalTo(to);

        public static MotionWrapper<TTransform> MoveTo<TTransform>(this TTransform transform, ISpline spline, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).MoveTo(spline);

        public static MotionWrapper<TTransform> MoveLocalTo<TTransform>(this TTransform transform, ISpline spline, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).MoveLocalTo(spline);

        #endregion

        #region Rotate

        public static MotionWrapper<TTransform> RotateMotion<TTransform>(this TTransform transform, Quaternion value, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).Rotate(value);

        public static MotionWrapper<TTransform> RotateMotion<TTransform>(this TTransform transform, Vector3 value, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).Rotate(value);

        public static MotionWrapper<TTransform> RotateX<TTransform>(this TTransform transform, float x, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).RotateX(x);

        public static MotionWrapper<TTransform> RotateY<TTransform>(this TTransform transform, float y, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).RotateY(y);

        public static MotionWrapper<TTransform> RotateZ<TTransform>(this TTransform transform, float z, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).RotateZ(z);

        public static MotionWrapper<TTransform> RotateTo<TTransform>(this TTransform transform, Quaternion to, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).RotateTo(to);

        public static MotionWrapper<TTransform> RotateTo<TTransform>(this TTransform transform, Vector3 to, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).RotateTo(to);

        public static MotionWrapper<TTransform> RotateLocalTo<TTransform>(this TTransform transform, Quaternion to, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).RotateLocalTo(to);

        public static MotionWrapper<TTransform> RotateLocalTo<TTransform>(this TTransform transform, Vector3 to, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).RotateLocalTo(to);

        #endregion

        #region Scale

        public static MotionWrapper<TTransform> Scale<TTransform>(this TTransform transform, Vector3 value, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).Scale(value);

        public static MotionWrapper<TTransform> ScaleX<TTransform>(this TTransform transform, float x, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).ScaleX(x);

        public static MotionWrapper<TTransform> ScaleY<TTransform>(this TTransform transform, float y, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).ScaleY(y);

        public static MotionWrapper<TTransform> ScaleZ<TTransform>(this TTransform transform, float z, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).ScaleZ(z);

        public static MotionWrapper<TTransform> ScaleLocalTo<TTransform>(this TTransform transform, Vector3 to, float time)
            where TTransform : Transform
            => ExtensionsHelper.PrepareMotion(transform, time).ScaleLocalTo(to);

        #endregion
    }
}
