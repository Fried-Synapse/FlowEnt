using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public abstract class AbstractTweenMotionBuilder : AbstractBuilder<ITweenMotion>
    {
#pragma warning disable IDE0044, RCS1169, RCS1213, IDE0051
        [SerializeField]
        private string name;
        [SerializeField]
        private bool isEnabled;
#pragma warning restore IDE0044, RCS1169, RCS1213, IDE0051
    }

    [Serializable]
    public abstract class AbstractTweenMotionBuilder<TItem> : AbstractTweenMotionBuilder
        where TItem : class
    {
        [SerializeField]
        protected TItem item;
    }

    [Serializable]
    public class TweenMotionsBuilder : AbstractBuilder<List<ITweenMotion>>
    {
#pragma warning disable IDE0044, RCS1169
        [SerializeReference]
        private List<AbstractTweenMotionBuilder> motions = new List<AbstractTweenMotionBuilder>();
#pragma warning restore IDE0044, RCS1169

        public override List<ITweenMotion> Build()
            => motions.ConvertAll(m => m.Build());
    }
}
