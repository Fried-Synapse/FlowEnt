using FlowEnt;
using UnityEngine;

public static class TransformMotionExtensions
{
    public static Motion<Transform> MoveAndRotateTo(this Motion<Transform> motion, Vector3 moveTo, Vector3 rotationTo)
        => motion.MoveTo(moveTo).RotateTo(rotationTo);

    public static Motion<Transform> MoveAndRotateTo(this Motion<Transform> motion, Vector3 moveTo, Quaternion rotationTo)
        => motion.MoveTo(moveTo).RotateTo(rotationTo);
}
