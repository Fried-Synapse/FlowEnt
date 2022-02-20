using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public abstract class AbstractTweenMotionBuilder : MonoBehaviour, IBuilder<ITweenMotion>
    {
        public abstract ITweenMotion Build();
    }

    [Serializable]
    public abstract class AbstractTweenMotionBuilder<TItem> : AbstractTweenMotionBuilder
        where TItem : class
    {
        [SerializeField]
        protected TItem item;
    }

    [Serializable]
    public class TweenMotionsBuilder : AbstractBuilder<ITweenMotion[]>
    {
#pragma warning disable IDE0044, RCS1169
        [SerializeField]
        private AbstractTweenMotionBuilder[] motions;
        public AbstractTweenMotionBuilder[] Motions => motions;
#pragma warning restore IDE0044, RCS1169

        public override ITweenMotion[] Build()
        {
            return null;
        }
    }
}
