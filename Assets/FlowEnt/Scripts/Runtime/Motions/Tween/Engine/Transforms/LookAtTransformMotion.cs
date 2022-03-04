using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Looks at a transform using the <see cref="Transform.LookAt(Transform)" /> method.
    /// </summary>
    public class LookAtTransformMotion : AbstractTweenMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private Transform target;
#pragma warning restore IDE0044, RCS1169
            public override ITweenMotion Build()
                => new LookAtTransformMotion(item, target);
        }

        public LookAtTransformMotion(Transform item, Transform target) : base(item)
        {
            this.target = target;
        }

        private readonly Transform target;

        public override void OnUpdate(float t)
        {
            item.LookAt(target, Vector3.up);
        }
    }
}