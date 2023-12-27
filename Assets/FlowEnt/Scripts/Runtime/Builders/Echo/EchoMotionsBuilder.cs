using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public abstract class AbstractEchoMotionBuilder : AbstractMotionBuilder<IEchoMotion>
    {
    }

    [Serializable]
    public abstract class AbstractEchoMotionBuilder<TItem> : AbstractEchoMotionBuilder
    {
        [SerializeField, AutoAssignButtonMotionField]
        protected TItem item;

        public TItem Item => item;
    }

    [Serializable]
    public class EchoMotionsBuilder : AbstractListBuilder<AbstractEchoMotionBuilder, IEchoMotion>
    {
        public override List<IEchoMotion> Build()
            => Items.FindAll(m => m.IsEnabled).ConvertAll(m => m.Build());
    }
}