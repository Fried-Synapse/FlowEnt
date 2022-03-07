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
        where TItem : class
    {
        [SerializeField]
        protected TItem item;
    }

    [Serializable]
    public class EchoMotionsBuilder : AbstractListBuilder<AbstractEchoMotionBuilder>, IBuilder<List<IEchoMotion>>
    {
        public List<IEchoMotion> Build()
            => Items.FindAll(m => m.IsEnabled).ConvertAll(m => m.Build());
    }
}
