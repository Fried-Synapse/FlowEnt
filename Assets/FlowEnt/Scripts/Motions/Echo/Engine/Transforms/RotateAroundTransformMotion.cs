using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Lerps the given value and applies it to <see cref="Transform.RotateAround(Vector3, Vector3, float)" /> where the point is the current position of the transform.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class RotateAroundTransformMotion<TTransform> : RotateAroundVectorMotion<TTransform>
        where TTransform : Transform
    {
        public RotateAroundTransformMotion(TTransform item, Transform point, Vector3 axis, float speed) : base(item, point.position, axis, speed)
        {
            this.point = point;
        }

        private new readonly Transform point;
        public override void OnUpdate(float t)
        {
            base.point = point.position;
            base.OnUpdate(t);
        }
    }
}