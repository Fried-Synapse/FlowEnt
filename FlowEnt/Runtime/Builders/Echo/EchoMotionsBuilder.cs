using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public abstract class AbstractEchoMotionBuilder : AbstractMotionBuilder<AbstractEchoMotion>
    {
    }

    [Serializable]
    public abstract class AbstractEchoMotionBuilder<TItem> : AbstractEchoMotionBuilder
    {
        [SerializeField, AutoAssignButtonMotionField]
        protected TItem item;

        public TItem Item { get => item; set => item = value; }
    }

    [Serializable]
    public class EchoMotionsBuilder : AbstractListBuilder<AbstractEchoMotionBuilder, AbstractEchoMotion>
    {
        public override List<AbstractEchoMotion> Build()
            => Items.FindAll(m => m.IsEnabled).ConvertAll(m => m.Build());
    }
}