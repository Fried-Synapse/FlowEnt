using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Lerps the given value and applies it to <see cref="Transform.RotateAround(Vector3, Vector3, float)" />.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class RotateAroundVectorMotion<TTransform> : AbstractEchoMotion<TTransform>
        where TTransform : Transform
    {
        public RotateAroundVectorMotion(TTransform item, Vector3 point, Vector3 axis, float speed) : base(item)
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