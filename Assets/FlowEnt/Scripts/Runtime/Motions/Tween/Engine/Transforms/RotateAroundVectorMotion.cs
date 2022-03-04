using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the given value and applies it to <see cref="Transform.RotateAround(Vector3, Vector3, float)" />.
    /// </summary>
    public class RotateAroundVectorMotion : AbstractTweenMotion<Transform>
    {
        //TODO add builders

        public RotateAroundVectorMotion(Transform item, Vector3 point, Vector3 axis, float toAngle) : base(item)
        {
            this.point = point;
            this.axis = axis;
            this.toAngle = toAngle;
        }

        protected Vector3 point;
        private readonly Vector3 axis;
        private readonly float toAngle;
        private float lastFrameAngle;

        public override void OnUpdate(float t)
        {
            float thisFrameAngle = Mathf.LerpUnclamped(0, toAngle, t);
            float angle = thisFrameAngle - lastFrameAngle;
            item.RotateAround(point, axis, angle);
            lastFrameAngle = thisFrameAngle;
        }
    }
}