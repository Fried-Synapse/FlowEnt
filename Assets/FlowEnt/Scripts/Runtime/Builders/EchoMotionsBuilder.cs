using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public abstract class AbstractEchoMotionBuilder : AbstractBuilder<IEchoMotion>
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
    public class EchoMotionsBuilder : AbstractBuilder<List<IEchoMotion>>
    {
#pragma warning disable IDE0044, RCS1169
        [SerializeReference]
        private List<AbstractEchoMotionBuilder> motions = new List<AbstractEchoMotionBuilder>();
#pragma warning restore IDE0044, RCS1169

        public override List<IEchoMotion> Build()
            => motions.ConvertAll(m => m.Build());
    }
}
