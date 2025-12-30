using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Abstract
{
    public abstract class AbstractFloatSpeedTypeMotion<TItem> : AbstractFloatSpeedMotion<TItem>
    {
#pragma warning disable RCS1158
        public const SpeedType DefaultSpeedType = SpeedType.Linear;
#pragma warning restore RCS1158

        [Serializable]
        public abstract class AbstractFloatSpeedTypeBuilder : AbstractFloatSpeedBuilder
        {
            [SerializeField]
            protected SpeedType speedType = DefaultSpeedType;
        }
        protected AbstractFloatSpeedTypeMotion(TItem item, float speed, SpeedType speedType) : base(item, speed)
        {
            this.speedType = speedType;
        }

        protected readonly SpeedType speedType;

        protected float GetSpeed(float distance)
            => speedType switch
            {
                SpeedType.Linear => speed,
                SpeedType.Elastic => speed * distance,
                SpeedType.Gravity => speed / distance,
                _ => 0
            };
    }
}
