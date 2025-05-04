using System;
using FriedSynapse.FlowEnt.Motions.Abstract;
using Object = UnityEngine.Object;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    /// <summary>
    /// Abstract Motion that can be used to create a simple motion.
    /// </summary>
    public abstract class AbstractTweenMotion : ITweenMotion
    {
        public virtual void OnStart()
        {
        }

        public abstract void OnUpdate(float t);

        public virtual void OnLoopStart()
        {
        }

        public virtual void OnLoopComplete()
        {
        }

        public virtual void OnComplete()
        {
        }
    }

    /// <summary>
    /// Generic Abstract Motion
    /// </summary>
    /// <typeparam name="TItem">Generic Type for the motion. There is a read only property of type &lt;TItem&gt; called item that can be used and it's required on the constructor.</typeparam>
    public abstract class AbstractTweenMotion<TItem> : AbstractTweenMotion, IObjectMotion
    {
        [Serializable]
        public abstract class AbstractBuilder : AbstractTweenMotionBuilder<TItem>
        {
        }

        protected AbstractTweenMotion(TItem item)
        {
            this.item = item;
        }

        protected readonly TItem item;
        public TItem Item => item;
        Object IObjectMotion.Object => item as Object;
    }
}