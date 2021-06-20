using UnityEngine;
using FlowEnt.Motions.TransformMotions;

namespace FlowEnt
{
    public static class TransformMotionExtensions
    {
        #region Move

        public static MotionWrapper<TTransform> Move<TTransform>(this MotionWrapper<TTransform> motionWrapper, Vector3 value)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, value));

        public static MotionWrapper<TTransform> MoveX<TTransform>(this MotionWrapper<TTransform> motionWrapper, float x)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, new Vector3(x, 0, 0)));

        public static MotionWrapper<TTransform> MoveY<TTransform>(this MotionWrapper<TTransform> motionWrapper, float y)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0, y, 0)));

        public static MotionWrapper<TTransform> MoveZ<TTransform>(this MotionWrapper<TTransform> motionWrapper, float z)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveVectorMotion<TTransform>(motionWrapper.Item, new Vector3(0, 0, z)));

        public static MotionWrapper<TTransform> MoveTo<TTransform>(this MotionWrapper<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveToVectorMotion<TTransform>(motionWrapper.Item, to));

        public static MotionWrapper<TTransform> MoveTo<TTransform>(this MotionWrapper<TTransform> motionWrapper, Vector3 from, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveToVectorMotion<TTransform>(motionWrapper.Item, from, to));

        public static MotionWrapper<TTransform> MoveLocalTo<TTransform>(this MotionWrapper<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalToVectorMotion<TTransform>(motionWrapper.Item, to));

        public static MotionWrapper<TTransform> MoveTo<TTransform>(this MotionWrapper<TTransform> motionWrapper, ISpline spline)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveToSplineMotion<TTransform>(motionWrapper.Item, spline));

        public static MotionWrapper<TTransform> MoveLocalTo<TTransform>(this MotionWrapper<TTransform> motionWrapper, ISpline spline)
            where TTransform : Transform
            => motionWrapper.Apply(new MoveLocalToSplineMotion<TTransform>(motionWrapper.Item, spline));

        #endregion

        #region Rotate

        public static MotionWrapper<TTransform> Rotate<TTransform>(this MotionWrapper<TTransform> motionWrapper, Quaternion value)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateQuaternionMotion<TTransform>(motionWrapper.Item, value));

        public static MotionWrapper<TTransform> Rotate<TTransform>(this MotionWrapper<TTransform> motionWrapper, Vector3 value)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateQuaternionMotion<TTransform>(motionWrapper.Item, Quaternion.Euler(value)));

        public static MotionWrapper<TTransform> RotateX<TTransform>(this MotionWrapper<TTransform> motionWrapper, float x)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateQuaternionMotion<TTransform>(motionWrapper.Item, Quaternion.Euler(new Vector3(x, 0, 0))));

        public static MotionWrapper<TTransform> RotateY<TTransform>(this MotionWrapper<TTransform> motionWrapper, float y)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateQuaternionMotion<TTransform>(motionWrapper.Item, Quaternion.Euler(new Vector3(0, y, 0))));

        public static MotionWrapper<TTransform> RotateZ<TTransform>(this MotionWrapper<TTransform> motionWrapper, float z)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateQuaternionMotion<TTransform>(motionWrapper.Item, Quaternion.Euler(new Vector3(0, 0, z))));

        public static MotionWrapper<TTransform> RotateTo<TTransform>(this MotionWrapper<TTransform> motionWrapper, Quaternion to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateToQuaternionMotion<TTransform>(motionWrapper.Item, to));

        public static MotionWrapper<TTransform> RotateTo<TTransform>(this MotionWrapper<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateToQuaternionMotion<TTransform>(motionWrapper.Item, Quaternion.Euler(to)));

        public static MotionWrapper<TTransform> RotateLocalTo<TTransform>(this MotionWrapper<TTransform> motionWrapper, Quaternion to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalToQuaternionMotion<TTransform>(motionWrapper.Item, to));

        public static MotionWrapper<TTransform> RotateLocalTo<TTransform>(this MotionWrapper<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new RotateLocalToQuaternionMotion<TTransform>(motionWrapper.Item, Quaternion.Euler(to)));

        public static MotionWrapper<TTransform> OrientToPath<TTransform>(this MotionWrapper<TTransform> motionWrapper)
            where TTransform : Transform
            => motionWrapper.Apply(new OrientToPathMotion<TTransform>(motionWrapper.Item));

        #endregion

        #region Scale

        public static MotionWrapper<TTransform> Scale<TTransform>(this MotionWrapper<TTransform> motionWrapper, Vector3 value)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleVectorMotion<TTransform>(motionWrapper.Item, value));

        public static MotionWrapper<TTransform> ScaleX<TTransform>(this MotionWrapper<TTransform> motionWrapper, float x)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleVectorMotion<TTransform>(motionWrapper.Item, new Vector3(x, 1, 1)));

        public static MotionWrapper<TTransform> ScaleY<TTransform>(this MotionWrapper<TTransform> motionWrapper, float y)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleVectorMotion<TTransform>(motionWrapper.Item, new Vector3(1, y, 1)));

        public static MotionWrapper<TTransform> ScaleZ<TTransform>(this MotionWrapper<TTransform> motionWrapper, float z)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleVectorMotion<TTransform>(motionWrapper.Item, new Vector3(1, 1, z)));

        public static MotionWrapper<TTransform> ScaleLocalTo<TTransform>(this MotionWrapper<TTransform> motionWrapper, Vector3 to)
            where TTransform : Transform
            => motionWrapper.Apply(new ScaleLocalToVectorMotion<TTransform>(motionWrapper.Item, to));

        #endregion
    }
}