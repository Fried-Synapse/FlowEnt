using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractAnimationCurve3dMotion<TItem> : AbstractTweenMotion<TItem>
    {
        [Serializable]
        public abstract class AbstractAnimationCurve3dBuilder : AbstractBuilder
        {
            [SerializeField]
            protected AnimationCurve3d animationCurve;
        }

        protected AbstractAnimationCurve3dMotion(TItem item, AnimationCurve3d animationCurve) : base(item)
        {
            this.animationCurve = animationCurve;
        }

        protected readonly AnimationCurve3d animationCurve;
    }
}
