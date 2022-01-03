using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Scaled the transform using the provided speed.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class ScaleVectorMotion<TTransform> : AbstractEchoMotion<TTransform>
        where TTransform : Transform
    {
        public ScaleVectorMotion(TTransform item, Vector3 speed) : base(item)
        {
            this.speed = speed;
        }

        private readonly Vector3 speed;

        public override void OnUpdate(float deltaTime)
        {
            item.localScale += speed * deltaTime;
        }
    }
}