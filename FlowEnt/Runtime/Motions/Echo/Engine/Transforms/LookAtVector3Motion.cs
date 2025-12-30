using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Looks at a transform using the <see cref="Transform.LookAt(Vector3)" /> method.
    /// </summary>
    public class LookAtVector3Motion : AbstractEchoMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractBuilder
        {
            [SerializeField]
            private Vector3 target;

            public override AbstractEchoMotion Build()
                => new LookAtVector3Motion(item, target);
        }

        public LookAtVector3Motion(Transform item, Vector3 target) : base(item)
        {
            this.target = target;
        }

        private readonly Vector3 target;

        public override void OnUpdate(float deltaTime)
        {
            item.LookAt(target, Vector3.up);
        }
    }
}