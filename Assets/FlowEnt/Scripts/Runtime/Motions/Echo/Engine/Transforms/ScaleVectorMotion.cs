using System;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Scaled the transform using the provided speed.
    /// </summary>
    public class ScaleVectorMotion : AbstractVector3SpeedMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractVector3SpeedBuilder
        {
            public override IEchoMotion Build()
                => new ScaleVectorMotion(item, speed);
        }

        public ScaleVectorMotion(Transform item, Vector3 speed) : base(item, speed)
        {
        }

        public override void OnUpdate(float deltaTime)
        {
            item.localScale += speed * deltaTime;
        }
    }
}