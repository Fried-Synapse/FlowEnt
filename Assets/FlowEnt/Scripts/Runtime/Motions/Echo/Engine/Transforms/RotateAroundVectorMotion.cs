using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Lerps the given value and applies it to <see cref="Transform.RotateAround(Vector3, Vector3, float)" />.
    /// </summary>
    public class RotateAroundVectorMotion : AbstractEchoMotion<Transform>
    {
        [Serializable]
        public abstract class AbstractRotateAroundBuilder : AbstractBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            protected Vector3 axis;
            [SerializeField]
            protected float speed;
#pragma warning restore IDE0044, RCS1169
        }

        [Serializable]
        public class Builder : AbstractRotateAroundBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private Vector3 point;
#pragma warning restore IDE0044, RCS1169

            public override IEchoMotion Build()
                => new RotateAroundVectorMotion(item, point, axis, speed);
        }

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