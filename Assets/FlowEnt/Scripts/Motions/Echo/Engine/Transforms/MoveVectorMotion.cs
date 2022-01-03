using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Moves the transform using the provided speed.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class MoveVectorMotion<TTransform> : AbstractEchoMotion<TTransform>
        where TTransform : Transform
    {
        public MoveVectorMotion(TTransform item, Vector3 speed) : base(item)
        {
            this.speed = speed;
        }

        private readonly Vector3 speed;

        public override void OnUpdate(float deltaTime)
        {
            item.position += speed * deltaTime;
        }
    }
}