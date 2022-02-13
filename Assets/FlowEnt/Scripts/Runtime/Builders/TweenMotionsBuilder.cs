using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using Newtonsoft.Json;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public abstract class AbstractTweenMotionBuilder : AbstractBuilder<ITweenMotion>
    {
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
#pragma warning disable RCS1169, RCS1085, IDE0044
        [SerializeField]
        private string motionsSerialised;
#pragma warning restore RCS1169, RCS1085, IDE0044

        public override List<ITweenMotion> Build()
        {
            List<AbstractTweenMotionBuilder> motions = JsonConvert.DeserializeObject<List<AbstractTweenMotionBuilder>>(motionsSerialised, JsonSettings.FullyTyped);
            return motions.ConvertAll(m => m.Build());
        }
    }
}
