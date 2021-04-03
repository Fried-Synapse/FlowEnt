using FlowEnt;
using UnityEngine;

public static class TransformMotionExtensions
{
    public static MotionWrapper<Transform> MoveAndRotateTo(this MotionWrapper<Transform> motion, Vector3 moveTo, Vector3 rotationTo)
        => motion.MoveTo(moveTo).RotateTo(rotationTo);

    public static MotionWrapper<Transform> MoveAndRotateTo(this MotionWrapper<Transform> motion, Vector3 moveTo, Quaternion rotationTo)
        => motion.MoveTo(moveTo).RotateTo(rotationTo);
}
