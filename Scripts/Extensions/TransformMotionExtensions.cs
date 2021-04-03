using UnityEngine;
using FlowEnt.Motions.TransformMotions;

namespace FlowEnt
{
    public static class TransformMotionExtensions
    {
        #region Move

        public static MotionWrapper<TTransform> Move<TTransform>(this MotionWrapper<TTransform> motion, Vector3 value)
            where TTransform : Transform
            => motion.Apply(new MoveVectorMotion<TTransform>(motion.Item, value));

        public static MotionWrapper<TTransform> MoveX<TTransform>(this MotionWrapper<TTransform> motion, float x)
            where TTransform : Transform
            => motion.Apply(new MoveVectorMotion<TTransform>(motion.Item, new Vector3(x, 0, 0)));

        public static MotionWrapper<TTransform> MoveY<TTransform>(this MotionWrapper<TTransform> motion, float y)
            where TTransform : Transform
            => motion.Apply(new MoveVectorMotion<TTransform>(motion.Item, new Vector3(0, y, 0)));

        public static MotionWrapper<TTransform> MoveZ<TTransform>(this MotionWrapper<TTransform> motion, float z)
            where TTransform : Transform
            => motion.Apply(new MoveVectorMotion<TTransform>(motion.Item, new Vector3(0, 0, z)));

        public static MotionWrapper<TTransform> MoveTo<TTransform>(this MotionWrapper<TTransform> motion, Vector3 to)
            where TTransform : Transform
            => motion.Apply(new MoveToVectorMotion<TTransform>(motion.Item, to));

        public static MotionWrapper<TTransform> MoveLocalTo<TTransform>(this MotionWrapper<TTransform> motion, Vector3 to)
            where TTransform : Transform
            => motion.Apply(new MoveLocalToVectorMotion<TTransform>(motion.Item, to));

        public static MotionWrapper<TTransform> MoveTo<TTransform>(this MotionWrapper<TTransform> motion, ISpline spline)
            where TTransform : Transform
            => motion.Apply(new MoveToSplineMotion<TTransform>(motion.Item, spline));

        public static MotionWrapper<TTransform> MoveLocalTo<TTransform>(this MotionWrapper<TTransform> motion, ISpline spline)
            where TTransform : Transform
            => motion.Apply(new MoveLocalToSplineMotion<TTransform>(motion.Item, spline));

        #endregion

        #region Rotate

        public static MotionWrapper<TTransform> Rotate<TTransform>(this MotionWrapper<TTransform> motion, Quaternion value)
            where TTransform : Transform
            => motion.Apply(new RotateQuaternionMotion<TTransform>(motion.Item, value));

        public static MotionWrapper<TTransform> Rotate<TTransform>(this MotionWrapper<TTransform> motion, Vector3 value)
            where TTransform : Transform
            => motion.Apply(new RotateQuaternionMotion<TTransform>(motion.Item, Quaternion.Euler(value)));

        public static MotionWrapper<TTransform> RotateX<TTransform>(this MotionWrapper<TTransform> motion, float x)
            where TTransform : Transform
            => motion.Apply(new RotateQuaternionMotion<TTransform>(motion.Item, Quaternion.Euler(new Vector3(x, 0, 0))));

        public static MotionWrapper<TTransform> RotateY<TTransform>(this MotionWrapper<TTransform> motion, float y)
            where TTransform : Transform
            => motion.Apply(new RotateQuaternionMotion<TTransform>(motion.Item, Quaternion.Euler(new Vector3(0, y, 0))));

        public static MotionWrapper<TTransform> RotateZ<TTransform>(this MotionWrapper<TTransform> motion, float z)
            where TTransform : Transform
            => motion.Apply(new RotateQuaternionMotion<TTransform>(motion.Item, Quaternion.Euler(new Vector3(0, 0, z))));

        public static MotionWrapper<TTransform> RotateTo<TTransform>(this MotionWrapper<TTransform> motion, Quaternion to)
            where TTransform : Transform
            => motion.Apply(new RotateToQuaternionMotion<TTransform>(motion.Item, to));

        public static MotionWrapper<TTransform> RotateTo<TTransform>(this MotionWrapper<TTransform> motion, Vector3 to)
            where TTransform : Transform
            => motion.Apply(new RotateToQuaternionMotion<TTransform>(motion.Item, Quaternion.Euler(to)));

        public static MotionWrapper<TTransform> RotateLocalTo<TTransform>(this MotionWrapper<TTransform> motion, Quaternion to)
            where TTransform : Transform
            => motion.Apply(new RotateLocalToQuaternionMotion<TTransform>(motion.Item, to));

        public static MotionWrapper<TTransform> RotateLocalTo<TTransform>(this MotionWrapper<TTransform> motion, Vector3 to)
            where TTransform : Transform
            => motion.Apply(new RotateLocalToQuaternionMotion<TTransform>(motion.Item, Quaternion.Euler(to)));

        public static MotionWrapper<TTransform> OrientToPath<TTransform>(this MotionWrapper<TTransform> motion)
            where TTransform : Transform
            => motion.Apply(new OrientToPathMotion<TTransform>(motion.Item));

        #endregion

        #region Scale

        public static MotionWrapper<TTransform> Scale<TTransform>(this MotionWrapper<TTransform> motion, Vector3 value)
            where TTransform : Transform
            => motion.Apply(new ScaleVectorMotion<TTransform>(motion.Item, value));

        public static MotionWrapper<TTransform> ScaleX<TTransform>(this MotionWrapper<TTransform> motion, float x)
            where TTransform : Transform
            => motion.Apply(new ScaleVectorMotion<TTransform>(motion.Item, new Vector3(x, 1, 1)));

        public static MotionWrapper<TTransform> ScaleY<TTransform>(this MotionWrapper<TTransform> motion, float y)
            where TTransform : Transform
            => motion.Apply(new ScaleVectorMotion<TTransform>(motion.Item, new Vector3(1, y, 1)));

        public static MotionWrapper<TTransform> ScaleZ<TTransform>(this MotionWrapper<TTransform> motion, float z)
            where TTransform : Transform
            => motion.Apply(new ScaleVectorMotion<TTransform>(motion.Item, new Vector3(1, 1, z)));

        public static MotionWrapper<TTransform> ScaleLocalTo<TTransform>(this MotionWrapper<TTransform> motion, Vector3 to)
            where TTransform : Transform
            => motion.Apply(new ScaleLocalToVectorMotion<TTransform>(motion.Item, to));

        #endregion
    }
}