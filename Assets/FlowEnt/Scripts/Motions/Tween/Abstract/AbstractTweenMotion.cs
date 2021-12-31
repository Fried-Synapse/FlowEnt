using FriedSynapse.FlowEnt.Motions.Abstract;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    /// <summary>
    /// Abstract Motion that can be used to create a simple motion.
    /// </summary>
    public abstract class AbstractTweenMotion : AbstractMotion, ITweenMotion
    {
        public virtual void OnStart() { }
        public abstract void OnUpdate(float t);
        public virtual void OnLoopComplete() { }
        public virtual void OnComplete() { }
    }

    /// <summary>
    /// Generic Abstract Motion
    /// </summary>
    /// <typeparam name="TItem">Generic Type for the motion. There is a read only property of type <T> called item that can be used and it's required on the constructor.</typeparam>
    public abstract class AbstractTweenMotion<TItem> : AbstractTweenMotion
        where TItem : class
    {
        protected AbstractTweenMotion(TItem item)
        {
            this.item = item;
        }

        protected readonly TItem item;
        public TItem Item => item;
    }
}