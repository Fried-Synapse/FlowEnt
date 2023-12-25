using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
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

            public override ITweenMotion Build()
            {
                return new RotateAroundTransformMotion(item, point, axis, toAngle);
            }
        }

        public RotateAroundTransformMotion(Transform item, Transform point, Vector3 axis, float toAngle) 
            : base(item, point != null ? point.position : Vector3.zero, axis, toAngle)
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