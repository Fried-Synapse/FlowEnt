using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Looks at a transform using the <see cref="Transform.LookAt(Transform)" /> method.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class LookAtTransformMotion<TTransform> : AbstractTweenMotion<TTransform>
        where TTransform : Transform
    {
        public LookAtTransformMotion(TTransform item, Transform target) : base(item)
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