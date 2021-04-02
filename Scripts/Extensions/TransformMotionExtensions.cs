using UnityEngine;

namespace FlowEnt
{
    public static class TransformMotionExtensions
    {
        #region Move

        public static MotionWrapper<Transform> MoveTo(this MotionWrapper<Transform> motion, Vector3 to)
            => motion.Apply(new MoveToMotion(motion.Item, to));

        public static MotionWrapper<Transform> MoveLocalTo(this MotionWrapper<Transform> motion, Vector3 to)
            => motion.Apply(new MoveLocalToMotion(motion.Item, to));

        public static MotionWrapper<Transform> Move(this MotionWrapper<Transform> motion, Vector3 value)
            => motion.Apply(new MoveMotion(motion.Item, value));

        public static MotionWrapper<Transform> Move(this MotionWrapper<Transform> motion, ISpline spline)
            => motion.Apply(new MoveSplineMotion(motion.Item, spline));

        public static MotionWrapper<Transform> MoveLocal(this MotionWrapper<Transform> motion, ISpline spline)
            => motion.Apply(new MoveLocalSplineMotion(motion.Item, spline));

        public static MotionWrapper<Transform> MoveX(this MotionWrapper<Transform> motion, float x)
            => motion.Apply(new MoveXMotion(motion.Item, x));

        public static MotionWrapper<Transform> MoveY(this MotionWrapper<Transform> motion, float y)
            => motion.Apply(new MoveYMotion(motion.Item, y));

        public static MotionWrapper<Transform> MoveZ(this MotionWrapper<Transform> motion, float z)
            => motion.Apply(new MoveZMotion(motion.Item, z));

        #endregion

        #region Rotate

        public static MotionWrapper<Transform> RotateTo(this MotionWrapper<Transform> motion, Quaternion to)
        {
            Quaternion from = Quaternion.identity;
            motion
                .OnStart(() =>
                {
                    from = motion.Item.rotation;
                })
                .OnUpdate(t =>
                {
                    motion.Item.rotation = Quaternion.LerpUnclamped(from, to, t);
                });

            return motion;
        }

        public static MotionWrapper<Transform> RotateTo(this MotionWrapper<Transform> motion, Vector3 to)
            => motion.RotateTo(Quaternion.Euler(to));

        #endregion

        public static MotionWrapper<Transform> OrientToPath(this MotionWrapper<Transform> motion)
        {
            Vector3? oldPosition = null;
            motion
                .OnStart(() =>
                {
                    oldPosition = motion.Item.position;
                })
                .OnUpdate(t =>
                {
                    Vector3 relativePosition = motion.Item.position - oldPosition.Value;
                    motion.Item.rotation = Quaternion.LookRotation(relativePosition);
                    oldPosition = motion.Item.position;
                });

            return motion;
        }

    }
}