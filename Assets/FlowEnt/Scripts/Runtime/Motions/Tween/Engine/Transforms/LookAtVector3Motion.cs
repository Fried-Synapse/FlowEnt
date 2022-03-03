using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Looks at a transform using the <see cref="Transform.LookAt(Vector3)" /> method.
    /// </summary>
    public class LookAtVector3Motion : AbstractTweenMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractTweenMotionBuilder<Transform>
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private Vector3 target;
#pragma warning restore IDE0044, RCS1169
            public override ITweenMotion Build()
                => new LookAtVector3Motion(item, target);
        }

        public LookAtVector3Motion(Transform item, Vector3 target) : base(item)
        {
            this.target = target;
        }

        private readonly Vector3 target;

        public override void OnUpdate(float t)
        {
            item.LookAt(target, Vector3.up);
        }
    }
}