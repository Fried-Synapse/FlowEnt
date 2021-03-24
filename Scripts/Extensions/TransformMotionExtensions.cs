using UnityEngine;

namespace FlowEnt
{
    public static class TransformMotionExtensions
    {
        #region Move

        public static MotionSet<Transform> MoveTo(this MotionSet<Transform> motion, Vector3 to)
        {
            Vector3? from = null;
            motion
                .OnStart(() =>
                {
                    from = motion.Element.position;
                })
                .OnUpdate(t =>
                {
                    motion.Element.position = Vector3.LerpUnclamped(from.Value, to, t);
                });

            return motion;
        }

        public static MotionSet<Transform> MoveLocalTo(this MotionSet<Transform> motion, Vector3 to)
        {
            Vector3? from = null;
            motion
                .OnStart(() =>
                {
                    from = motion.Element.localPosition;
                })
                .OnUpdate(t =>
                {
                    motion.Element.localPosition = Vector3.LerpUnclamped(from.Value, to, t);
                });

            return motion;
        }

        public static MotionSet<Transform> Move(this MotionSet<Transform> motion, ISpline spline)
        {
            motion
                .OnUpdate(t =>
                {
                    motion.Element.position = spline.GetPoint(t);
                });

            return motion;
        }

        public static MotionSet<Transform> OrientToPath(this MotionSet<Transform> motion)
        {
            Vector3? oldPosition = null;
            motion
                .OnStart(() =>
                {
                    oldPosition = motion.Element.position;
                })
                .OnUpdate(t =>
                {
                    Vector3 relativePosition = motion.Element.position - oldPosition.Value;
                    motion.Element.rotation = Quaternion.LookRotation(relativePosition);
                    oldPosition = motion.Element.position;
                });

            return motion;
        }

        public static MotionSet<Transform> Move(this MotionSet<Transform> motion, Vector3 value)
        {
            Vector3? from = null;
            Vector3? to = null;
            motion
                .OnStart(() =>
                {
                    from = motion.Element.position;
                    to = from + value;
                })
                .OnUpdate(t =>
                {
                    motion.Element.position = Vector3.LerpUnclamped(from.Value, to.Value, t);
                });

            return motion;
        }

        public static MotionSet<Transform> MoveX(this MotionSet<Transform> motion, float x)
        {
            Vector3? from = null;
            Vector3? to = null;
            motion
                .OnStart(() =>
                {
                    from = motion.Element.position;
                    to = from + new Vector3(x, 0, 0);
                })
                .OnUpdate(t =>
                {
                    motion.Element.position = Vector3.LerpUnclamped(from.Value, to.Value, t);
                });

            return motion;
        }

        public static MotionSet<Transform> MoveY(this MotionSet<Transform> motion, float y)
        {
            Vector3? from = null;
            Vector3? to = null;
            motion
                .OnStart(() =>
                {
                    from = motion.Element.position;
                    to = from + new Vector3(0, y, 0);
                })
                .OnUpdate(t =>
                {
                    motion.Element.position = Vector3.LerpUnclamped(from.Value, to.Value, t);
                });

            return motion;
        }

        public static MotionSet<Transform> MoveZ(this MotionSet<Transform> motion, float z)
        {
            Vector3? from = null;
            Vector3? to = null;
            motion
                .OnStart(() =>
                {
                    from = motion.Element.position;
                    to = from + new Vector3(0, 0, z);
                })
                .OnUpdate(t =>
                {
                    motion.Element.position = Vector3.LerpUnclamped(from.Value, to.Value, t);
                });

            return motion;
        }

        #endregion

        #region Rotate

        public static MotionSet<Transform> RotateTo(this MotionSet<Transform> motion, Quaternion to)
        {
            Quaternion from = Quaternion.identity;
            motion
                .OnStart(() =>
                {
                    from = motion.Element.rotation;
                })
                .OnUpdate(t =>
                {
                    motion.Element.rotation = Quaternion.LerpUnclamped(from, to, t);
                });

            return motion;
        }

        public static MotionSet<Transform> RotateTo(this MotionSet<Transform> motion, Vector3 to)
            => motion.RotateTo(Quaternion.Euler(to));

        #endregion

    }
}