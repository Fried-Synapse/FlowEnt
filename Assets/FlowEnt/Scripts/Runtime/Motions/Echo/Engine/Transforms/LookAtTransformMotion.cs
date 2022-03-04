using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Looks at a transform using the <see cref="Transform.LookAt(Transform)" /> method.
    /// </summary>
    public class LookAtTransformMotion : AbstractEchoMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private Transform target;
#pragma warning restore IDE0044, RCS1169

            public override IEchoMotion Build()
                => new LookAtTransformMotion(item, target);
        }

        public LookAtTransformMotion(Transform item, Transform target) : base(item)
        {
            this.target = target;
        }

        private readonly Transform target;

        public override void OnUpdate(float deltaTime)
        {
            item.LookAt(target, Vector3.up);
        }
    }
}