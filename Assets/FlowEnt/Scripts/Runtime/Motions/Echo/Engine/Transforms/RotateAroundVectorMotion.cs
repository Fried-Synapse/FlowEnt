using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Lerps the given value and applies it to <see cref="Transform.RotateAround(Vector3, Vector3, float)" />.
    /// </summary>
    public class RotateAroundVectorMotion : AbstractEchoMotion<Transform>
    {
        //TODO add builders

        public RotateAroundVectorMotion(Transform item, Vector3 point, Vector3 axis, float speed) : base(item)
        {
            this.point = point;
            this.axis = axis;
            this.speed = speed;
        }

        protected Vector3 point;
        private readonly Vector3 axis;
        private readonly float speed;

        public override void OnUpdate(float t)
        {
            item.RotateAround(point, axis, speed * t);
        }
    }
}