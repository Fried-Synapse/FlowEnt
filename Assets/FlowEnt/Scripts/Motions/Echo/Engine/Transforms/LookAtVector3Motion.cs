using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Looks at a transform using the <see cref="Transform.LookAt(Vector3)" /> method.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class LookAtVector3Motion<TTransform> : AbstractEchoMotion<TTransform>
        where TTransform : Transform
    {
        public LookAtVector3Motion(TTransform item, Vector3 target) : base(item)
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