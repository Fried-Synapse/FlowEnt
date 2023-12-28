using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public abstract class AbstractTweenMotionBuilder : AbstractMotionBuilder<ITweenMotion>
    {
    }

    [Serializable]
    public abstract class AbstractTweenMotionBuilder<TItem> : AbstractTweenMotionBuilder
    {
        [SerializeField, AutoAssignButtonMotionField]
        protected TItem item;

        public TItem Item => item;
    }

    [Serializable]
    public class TweenMotionsBuilder : AbstractListBuilder<AbstractTweenMotionBuilder, ITweenMotion>
    {
        public override List<ITweenMotion> Build()
            => Items.FindAll(m => m.IsEnabled).ConvertAll(m => m.Build());
    }
}