using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public abstract class AbstractTweenMotionBuilder : AbstractMotionBuilder<AbstractTweenMotion>
    {
    }

    [Serializable]
    public abstract class AbstractTweenMotionBuilder<TItem> : AbstractTweenMotionBuilder
    {
        [SerializeField, AutoAssignButtonMotionField]
        protected TItem item;

        public TItem Item { get => item; set => item = value; }
    }

    [Serializable]
    public class TweenMotionsBuilder : AbstractListBuilder<AbstractTweenMotionBuilder, AbstractTweenMotion>
    {
        public override List<AbstractTweenMotion> Build()
            => Items.FindAll(m => m.IsEnabled).ConvertAll(m => m.Build());
    }
}