using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Abstract
{
    public abstract class AbstractFloatSpeedMotion<TItem> : AbstractEchoMotion<TItem>
    {
#pragma warning disable RCS1158
        public const float DefaultSpeed = 1f;
#pragma warning restore RCS1158

        [Serializable]
        public abstract class AbstractFloatSpeedBuilder : AbstractBuilder
        {
            [SerializeField]
            protected float speed = DefaultSpeed;
        }
        protected AbstractFloatSpeedMotion(TItem item, float speed) : base(item)
        {
            this.speed = speed;
        }

        protected float speed;
    }
}
