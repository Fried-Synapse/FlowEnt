using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Abstract
{
    public abstract class AbstractVector3SpeedMotion<TItem> : AbstractEchoMotion<TItem>
        where TItem : class
    {
        [Serializable]
        public abstract class AbstractVector3SpeedBuilder : AbstractBuilder
        {
            [SerializeField]
            protected Vector3 speed;
        }
        protected AbstractVector3SpeedMotion(TItem item, Vector3 speed) : base(item)
        {
            this.speed = speed;
        }

        protected Vector3 speed;
    }
}
