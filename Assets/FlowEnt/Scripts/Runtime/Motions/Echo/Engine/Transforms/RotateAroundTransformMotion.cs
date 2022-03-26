using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Lerps the given value and applies it to <see cref="Transform.RotateAround(Vector3, Vector3, float)" /> where the point is the current position of the transform.
    /// </summary>
    public class RotateAroundTransformMotion : RotateAroundVectorMotion
    {
        [Serializable]
        public new class Builder : AbstractRotateAroundBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private Transform point;
#pragma warning restore IDE0044, RCS1169

            public override IEchoMotion Build()
            {
                return new RotateAroundTransformMotion(item, point, axis, speed);
            }
        }

        public RotateAroundTransformMotion(Transform item, Transform point, Vector3 axis, float speed) : base(item, point.position, axis, speed)
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