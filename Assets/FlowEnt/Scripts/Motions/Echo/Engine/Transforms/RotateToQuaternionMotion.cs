using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Transforms
{
    /// <summary>
    /// Rotates the transform towards the target using the deltaTime as a step.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public class RotateToQuaternionMotion<TTransform> : AbstractEchoMotion<TTransform>
        where TTransform : Transform
    {
#pragma warning disable RCS1158
        public const float DefaultSpeed = 1f;
        public const SpeedType DefaultSpeedType = SpeedType.Linear;
#pragma warning restore RCS1158

        public RotateToQuaternionMotion(TTransform item, Quaternion target, float speed = DefaultSpeed, SpeedType speedType = DefaultSpeedType) : base(item)
        {
            this.target = target;
            this.speedType = speedType;
            this.speed = speed;
        }

        protected Quaternion target;
        private readonly SpeedType speedType;
        private readonly float speed;

        public override void OnUpdate(float deltaTime)
        {
            float speed = 0;
            switch (speedType)
            {
                case SpeedType.Linear:
                    speed = this.speed;
                    break;
                case SpeedType.Elastic:
                    speed = this.speed * Quaternion.Angle(item.rotation, target);
                    break;
                case SpeedType.Gravity:
                    speed = this.speed / Quaternion.Angle(item.rotation, target);
                    break;
            }

            item.rotation = Quaternion.RotateTowards(item.rotation, target, speed * deltaTime);
        }
    }
}